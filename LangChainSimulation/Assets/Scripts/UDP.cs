//#C#�iunity�j��M��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDP : MonoBehaviour
{

    static UdpClient udp;
    IPEndPoint remoteEP = null;
    int i = 0;
    // Use this for initialization
    void Start()
    {
        int LOCA_LPORT = 50007;

        udp = new UdpClient(LOCA_LPORT);
        //udp.Client.ReceiveTimeout = 2000;
    }

    // Update is called once per frame
    void Update()
    {
        IPEndPoint remoteEP = null;
        byte[] data = udp.Receive(ref remoteEP);
        string text = Encoding.UTF8.GetString(data);
        Debug.Log(text);
    }

    //public void ConnectPython()
    //{
    //    // �G���h�|�C���g��ݒ肷��
    //    IPEndPoint RemoteEP = new IPEndPoint(IPAddress.Any, 50007);

    //    // TcpListener���쐬����
    //    TcpListener Listener = new TcpListener(RemoteEP);

    //    // TCP�ڑ���҂��󂯂�
    //    Listener.Start();
    //    TcpClient Client = Listener.AcceptTcpClient();

    //    // �ڑ����ł���΁A�f�[�^������肷��X�g���[����ۑ�����
    //    NetworkStream Stream = Client.GetStream();

    //    GetPID(Stream);

    //    // �ڑ���؂�
    //    Client.Close();
    //}


    //void GetPID(NetworkStream Stream)
    //{
    //    Byte[] data = new Byte[256];
    //    String responseData = String.Empty;

    //    // �ڑ��悩��f�[�^��ǂݍ���
    //    Int32 bytes = Stream.Read(data, 0, data.Length);

    //    // �ǂݍ��񂾃f�[�^�𕶎���ɕϊ�����
    //    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
    //    UnityEngine.Debug.Log("PID: " + responseData);

    //    // �󂯎����������ɕ�����t�������Ė߂�
    //    Byte[] buffer = System.Text.Encoding.ASCII.GetBytes("responce: " + responseData);
    //    Stream.Write(buffer, 0, buffer.Length);
    //}
}