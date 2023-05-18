using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System;

public class TCP : MonoBehaviour
{
    public IPEndPoint ServerIPEndPoint { get; set; }
    private Socket Socket { get; set; }
    public const int BufferSize = 1024;
    public string host = "127.0.0.1";

    public byte[] Buffer { get; } = new byte[BufferSize];

    public TCP()
    {
        this.ServerIPEndPoint = new IPEndPoint(IPAddress.Loopback, 8080);
    }

    // �\�P�b�g�ʐM�̐ڑ�
    public void Connect()
    {
        this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        this.Socket.Connect(this.ServerIPEndPoint);

        // �񓯊��Ŏ�M��ҋ@
        this.Socket.BeginReceive(this.Buffer, 0, BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), this.Socket);
        Debug.Log("Connceted");


    }

    // �\�P�b�g�ʐM�ڑ��̐ؒf
    public void DisConnect()
    {
        this.Socket?.Disconnect(false);
        this.Socket?.Dispose();
    }

    // ���b�Z�[�W�̑��M(��������)

    public void Send(string message)
    {
        var sendBytes = new UTF8Encoding().GetBytes(message);
        this.Socket.Send(sendBytes);
    }

    // �񓯊���M�̃R�[���o�b�N���\�b�h(�ʃX���b�h�Ŏ��s�����)
    private void ReceiveCallback(IAsyncResult asyncResult)
    {
        var socket = asyncResult.AsyncState as Socket;

        var byteSize = -1;
        try
        {
            // ��M��ҋ@
            byteSize = socket.EndReceive(asyncResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        // ��M�����f�[�^������ꍇ�A���̓��e��\������
        // �ēx�񓯊��ł̎�M���J�n����
        if (byteSize > 0)
        {
            Debug.Log("get data back! : " + $"{Encoding.UTF8.GetString(this.Buffer, 0, byteSize)}");
            socket.BeginReceive(this.Buffer, 0, this.Buffer.Length, SocketFlags.None, ReceiveCallback, socket);
        }
    }

}

