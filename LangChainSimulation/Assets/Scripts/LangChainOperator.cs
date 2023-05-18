using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

/*
 * 用途に応じてLangChainにシミュレーションを出力してもらう
 */

public class LangChainOperator : MonoBehaviour
{
    TCP tcp;
    public GameObject _dataManager;

    private void Start()
    {
        tcp = _dataManager.GetComponent<TCP>();
        tcp.Connect();
    }

    public void sendTest()
    {
        tcp.Send("HELLO WORLD");
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
