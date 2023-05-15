using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Threading;

public class UDP : MonoBehaviour
{
    UdpClient udpClient;
    int tickRate = 10;
    Thread receiveThread;
    IPEndPoint receiveEP = new IPEndPoint(IPAddress.Any, 9000);
    void Awake()
    {
        udpClient = new UdpClient(receiveEP);

        receiveThread = new Thread(new ThreadStart(ThreadReceive));
        receiveThread.Start();
        print("受信セットアップ完了");

        StartCoroutine(SendMessage());
    }

    void ThreadReceive()
    {
        while (true)
        {
            IPEndPoint senderEP = null;
            byte[] receivedBytes = udpClient.Receive(ref senderEP);
            Parse(senderEP, receivedBytes);
        }
    }

    void Parse(IPEndPoint senderEP, byte[] message)
    {
        //受信時の処理
        string str = Encoding.GetEncoding("Shift_JIS").GetString(message);
        print(str);
    }

    IEnumerator SendMessage()
    {
        yield return new WaitForSeconds(1f / tickRate);
        //送信処理
    }
}