using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private Main main;
    private LangChainOperator langchainOperator;


    void Awake()
    {
        main = GetComponent<Main>();
        langchainOperator = GetComponent<LangChainOperator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            langchainOperator.sendTest();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //�V�����L�����N�^�[�𐶐�
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            //�P���̉�b���n�߂�
        }

    }
}
