#python送信側

import socket
import random
import time
import openai

openai.api_key = "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"

response = openai.ChatCompletion.create(
  model="gpt-3.5-turbo",
  messages=[
      {"role": "user", "content" : "OpenAI社の主要プロダクトについて教えてください"}
    ]
)

print(response["choices"][0]["message"]["content"])