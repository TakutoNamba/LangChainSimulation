using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

/*
 * �p�r�ɉ�����LangChain�ɃV�~�����[�V�������o�͂��Ă��炤
 */

public class LangChainOperator : MonoBehaviour
{
    UdpClient udpClient;
    UDP udp;
    public GameObject _dataManager;

    private void Awake()
    {
        udp = _dataManager.GetComponent<UDP>();
    }

    public void sendTest()
    {
        udp.sendMessage("TEST");
    }

    public void initializeCharacter()
    {
       
    }

    public void getCharacterStatus()
    {

    }
    public void updateCharacterFeeling()
    {

    }

}
