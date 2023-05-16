#python送信側

import socket
import random
import time
import openai
import os
import logging
import math
import faiss
from datetime import datetime, timedelta
from typing import List
from termcolor import colored
from langchain.chat_models import ChatOpenAI
from langchain.docstore import InMemoryDocstore
from langchain.embeddings import OpenAIEmbeddings
from langchain.retrievers import TimeWeightedVectorStoreRetriever
from langchain.vectorstores import FAISS
from langchain.experimental.generative_agents import GenerativeAgent, GenerativeAgentMemory

openai.api_key = "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"
os.environ['OPENAI_API_KEY'] = "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"
USER_NAME = "takuto" # The name you want to use when interviewing the agent.
LLM = ChatOpenAI(max_tokens=1500) # Can be any LLM you want.


def setup():
    logging.basicConfig(level=logging.ERROR)


# print(response["choices"][0]["message"]["content"])
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

def initializeCharacter():
    
    takashi_memory = GenerativeAgentMemory(
    llm=LLM,
    memory_retriever=create_new_memory_retriever(),
    verbose=False,
    reflection_threshold=8 # we will give this a relatively low number to show how reflection works
    )

    takashi = GenerativeAgent(name="Takashi", 
              age=25,
              traits="excited, likes coding, curious", # You can add more persistent traits here 
              status="looking for things to do on holidays, prefarably outdoor activities", # When connected to a virtual world, we can have the characters update their status
              memory_retriever=create_new_memory_retriever(),
              llm=LLM,
              memory=takashi_memory
    )

    print(takashi.get_summary())





def main():
    setup()
    initializeCharacter()


if __name__ == '__main__': main()

