using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private Main main;
    private LangChainOperator langchainOperator;
    private UDP udp;
    private TCP tcp;
    public GameObject _dataManager;

    void Start()
    {
        main = GetComponent<Main>();
        langchainOperator = GetComponent<LangChainOperator>();
        //udp = _dataManager.GetComponent<UDP>();
        //udp.Init();
        //udp.startReceive();

        tcp = _dataManager.GetComponent<TCP>();
        tcp.Connect();


    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            //langchainOperator.sendTest();
            tcp.Send("Hello World");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //新しくキャラクターを生成
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            //１日の会話を始める
        }

    }
}
