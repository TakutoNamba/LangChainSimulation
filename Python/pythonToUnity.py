import socket
import random
import time

HOST = '192.168.0.76'
PORT = 8000


client = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
while True:
  a = random.randrange(3)
  result = str(a)
  print(a)
  client.sendto(result.encode('utf-8'),(HOST,PORT))
  time.sleep(2.0)