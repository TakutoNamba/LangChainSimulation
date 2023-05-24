# 0.ライブラリのインポートと変数定義
import socket
import langChain
import Config
import threading

server_ip = "127.0.0.1"
server_port = 8080
listen_num = 5
buffer_size = 1024

prev_receive_data = ""
prev_send_data = ""



def send_tempData():
    # print(prev_send_data)
    prev_send_data = "test"
    print("thread opening")
    while True:
        if Config.send_data != prev_send_data:
            client.sendall(Config.send_data.encode())
            prev_send_data = Config.send_data

langChain.setup()
# 1.ソケットオブジェクトの作成
tcp_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    
# 2.作成したソケットオブジェクトにIPアドレスとポートを紐づける
tcp_server.bind((server_ip, server_port))
    
# 3.作成したオブジェクトを接続可能状態にする
tcp_server.listen(listen_num)
    
# 4.ループして接続を待ち続ける
client,address = tcp_server.accept()
print("[*] Connected!! [ Source : {}]".format(address))

thread = threading.Thread(target=send_tempData)
thread.start()
print("thread open")


while True:
    # 5.クライアントと接続する
    data = client.recv(buffer_size)
        
    #接続が切られたら、終了
    if not data:
        print("receive data don't exist")
        break
    else:
        print("receive data : {} ".format(data.decode("utf-8")))
        Config.receive_data = data.decode("utf-8")
            
        if Config.receive_data != prev_receive_data:


            output = langChain.implementSim(data.decode("utf-8"))
            if(output!=None):
                Config.send_data = output
                    
    prev_receive_data = Config.receive_data
    prev_send_data = Config.send_data
        
# 8.接続を終了させる
print("close client communication")
#clientとserverのsocketを閉じる
client.close()
tcp_server.close()


# if __name__ == '__main__':
#     main()