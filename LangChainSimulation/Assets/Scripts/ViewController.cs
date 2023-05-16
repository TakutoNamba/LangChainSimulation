using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private Main main;
    private LangChainOperator langchainOperator;
    private UDP udp;
    public GameObject _dataManager;

    void Start()
    {
        main = GetComponent<Main>();
        langchainOperator = GetComponent<LangChainOperator>();
        udp = _dataManager.GetComponent<UDP>();
        udp.Init();
        udp.startReceive();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            langchainOperator.sendTest();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            //�V�����L�����N�^�[�𐶐�
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            //�P���̉�b���n�߂�
        }

    }
}
