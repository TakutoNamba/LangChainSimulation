import random
import time
import openai
import os
import logging
import math
import faiss
import socket
from datetime import datetime, timedelta
from typing import List
from termcolor import colored
from langchain.chat_models import ChatOpenAI
from langchain.docstore import InMemoryDocstore
from langchain.embeddings import OpenAIEmbeddings
from langchain.retrievers import TimeWeightedVectorStoreRetriever
from langchain.vectorstores import FAISS
from langchain.experimental.generative_agents import GenerativeAgent, GenerativeAgentMemory
import Config

#OpenAI API key 必要
openai.api_key = ""
os.environ['OPENAI_API_KEY'] = ""

USER_NAME = "takuto" # The name you want to use when interviewing the agent.
LLM = ChatOpenAI(max_tokens=1500) # Can be any LLM you want.


def setup():
    logging.basicConfig(level=logging.ERROR)

def interview_agent(agent: GenerativeAgent, message: str) -> str:
    """Help the notebook user interact with the agent."""
    new_message = f"{USER_NAME} says {message}"
    return agent.generate_dialogue_response(new_message, datetime.now())[1]

def run_conversation(agents: List[GenerativeAgent], initial_observation: str) -> None:
    """Runs a conversation between agents."""
    _, observation = agents[1].generate_reaction(initial_observation, datetime.now())
    Config.send_data = "3|" + agents[1].name + "|" + observation + "/"
    print(observation)

    turns = 0
    while True:
        break_dialogue = False
        inner_turn = 0
        for agent in agents:
            stay_in_dialogue, observation = agent.generate_dialogue_response(observation, datetime.now())

            comment = observation.split('said ')[1]
            Config.conversation += (comment + "/")
            Config.send_data = "3|" + agent.name + "|" + comment + "/"
            
            print(Config.send_data)

            #一般的に話し終わったとされるような言い回しが入っている場合、その瞬間に会話を終了させる
            if 'Have a great' in observation or 'See you' in observation:
                break_dialogue = True
            
            # observation = f"{agent.name} said {reaction}"
            if not stay_in_dialogue:
                break_dialogue = True
            inner_turn += 1
        if break_dialogue:
            print("Conversation done")
            break
        turns += 1

def relevance_score_fn(score: float) -> float:
    """Return a similarity score on a scale [0, 1]."""
    # This will differ depending on a few things:
    # - the distance / similarity metric used by the VectorStore
    # - the scale of your embeddings (OpenAI's are unit norm. Many others are not!)
    # This function converts the euclidean norm of normalized embeddings
    # (0 is most similar, sqrt(2) most dissimilar)
    # to a similarity function (0 to 1)
    return 1.0 - score / math.sqrt(2)

def create_new_memory_retriever():
    """Create a new vector store retriever unique to the agent."""
    # Define your embedding model
    embeddings_model = OpenAIEmbeddings()
    # Initialize the vectorstore as empty
    embedding_size = 1536
    index = faiss.IndexFlatL2(embedding_size)
    vectorstore = FAISS(embeddings_model.embed_query, index, InMemoryDocstore({}), {}, relevance_score_fn=relevance_score_fn)
    return TimeWeightedVectorStoreRetriever(vectorstore=vectorstore, other_score_keys=["importance"], k=15)    

def initializeCharacter(_name, _age, _traits, _status):

    Config.agent_num += 1
    
    agt_memory = GenerativeAgentMemory(
    llm=LLM,
    memory_retriever=create_new_memory_retriever(),
    verbose=False,
    reflection_threshold=8 # we will give this a relatively low number to show how reflection works
    )

    Config.agent_memories.append(agt_memory)

    agent = GenerativeAgent(name= _name, 
              age= _age,
              traits= _traits, # You can add more persistent traits here 
              status= _status, # When connected to a virtual world, we can have the characters update their status
              memory_retriever=create_new_memory_retriever(),
              llm=LLM,
              memory=Config.agent_memories[Config.agent_num-1]
    )


    Config.agents.append(agent)
    print(Config.agents[len(Config.agents)-1].get_summary(force_refresh=True))

def inputMemories(_name, memories):
    # print(memories)
    singleMemory = memories.split('/')
    for m in singleMemory:
        # //print(m)
        checkTarget(_name).memory.add_memory(m, datetime.now())

    print(checkTarget(_name).get_summary(force_refresh=True))

def checkTarget(agentName) -> GenerativeAgent:
    for agt in Config.agents:
        if(agt.name == agentName):
            # print(agt.name)
            return agt

def implementSim(msgCode) -> str:
    msgType = msgCode.split('|')
    if msgType[0] == "0":
        initializeCharacter(msgType[1], msgType[2], msgType[3], msgType[4])
        output = "0|Character created!"
        return output
    elif msgType[0] == "1":
        inputMemories(msgType[1], msgType[2])
        output = "1|memory input completed!"
        return output
    elif msgType[0] == "2":
        content = interview_agent(checkTarget(msgType[1]), msgType[2])
        output = "2" + "|" + content
        return output
    elif msgType[0] == "3":
        agents = [checkTarget(msgType[1]), checkTarget(msgType[2])]
        run_conversation(agents, msgType[3])
        output = "3" + "|" + Config.conversation
        print(output)
        return None
    else:
        print("No simulation found")

