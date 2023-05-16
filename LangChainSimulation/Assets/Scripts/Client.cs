//#CÅiunityÅjéÛêMë§

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UniRx;
using UnityEngine.UI;
using System.Threading;

public class Client : MonoBehaviour
{
    static UdpClient udp;
    IPEndPoint remoteEP = null;

    private string host = "127.0.0.1";

    private int LOCAL_PORT = 50007;

    private Subject<string> subject = new Subject<string>();
    [SerializeField] Text message;


    void Start()
    {
        udp = new UdpClient(LOCAL_PORT);
        udp.BeginReceive(OnReceived, udp);
        udp.Client.ReceiveTimeout = 2000;

        subject
            .ObserveOnMainThread()
            .Subscribe(msg => {
                message.text = msg;
            }).AddTo(this);
    }

    private void OnReceived(System.IAsyncResult result)
    {
        UdpClient getUdp = (UdpClient)result.AsyncState;
        IPEndPoint ipEnd = null;

        byte[] getByte = getUdp.EndReceive(result, ref ipEnd);

        var message = Encoding.UTF8.GetString(getByte);
        subject.OnNext(message);
        print(message);

        getUdp.BeginReceive(OnReceived, getUdp);
    }


    void Update()
    {


    }
    private void OnDestroy()
    {
        udp.Close();
    }

}
