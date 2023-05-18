# 0.ライブラリのインポートと変数定義
import socket

server_ip = "127.0.0.1"
server_port = 8080
listen_num = 5
buffer_size = 1024

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

openai.api_key = ""
os.environ['OPENAI_API_KEY'] = ""
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


# 1.ソケットオブジェクトの作成
tcp_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# 2.作成したソケットオブジェクトにIPアドレスとポートを紐づける
tcp_server.bind((server_ip, server_port))

# 3.作成したオブジェクトを接続可能状態にする
tcp_server.listen(listen_num)

# 4.ループして接続を待ち続ける
client,address = tcp_server.accept()
print("[*] Connected!! [ Source : {}]".format(address))
while True:
    # 5.クライアントと接続する
    data = client.recv(buffer_size)
    #接続が切られたら、終了
    if not data:
        print("receive data don't exist")
        break
    else:
        print("receive data : {} ".format(data.decode("utf-8")))
        #clientにOKを送信
        setup()
        initializeCharacter()
        client.sendall('OK\n'.encode())

    # 8.接続を終了させる
print("close client communication")
#clientとserverのsocketを閉じる
client.close()
tcp_server.close()