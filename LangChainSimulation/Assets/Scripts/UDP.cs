using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

public class UDP : MonoBehaviour
{
    UdpClient udpForReceive;
    Thread receiveThread;
    IPEndPoint receiveEP;

    UdpClient udpForSend;
    IPEndPoint sendEP;

    public string host = "192.168.0.76";
    public int _rcvPort;
    public int _hostPort;



    public UDP()
    {

    }


    public void Init()
    {
        sendEP = new IPEndPoint(IPAddress.Parse(host), _hostPort);
        udpForSend = new UdpClient(sendEP);
        udpForSend.Connect(sendEP);

        //receiveEP = new IPEndPoint(IPAddress.Any, _rcvPort);
        //udpForReceive = new UdpClient(receiveEP);

        print("セットアップ完了");
    }

    public void sendMessage(string msg)
    {

        byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
        udpForSend.SendAsync(data, data.Length);

    }

    public void startReceive()
    {
        receiveThread = new Thread(new ThreadStart(ThreadReceive));
        receiveThread.Start();
    }

    public void ThreadReceive()
    {
        IPEndPoint senderEP = null;
        while (true)
        {
            try
            {
                byte[] receivedBytes = udpForReceive.Receive(ref senderEP);
                Parse(senderEP, receivedBytes);
            }
            catch　{　}
        }
    }


    public void stopReceive()
    {
        receiveThread.Interrupt();
    }

    public void end()
    {
        udpForReceive.Close();
        udpForSend.Close();
        receiveThread.Abort();
    }

    public string Parse(IPEndPoint senderEP, byte[] message)
    {
        //受信時の処理
        var str = Encoding.UTF8.GetString(message);
        print(str);
        return str;
    }



}