using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private Main main;
    private LangChainOperator langchainOperator;

    public GameObject testAgt;


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
            langchainOperator.initializeCharacter(testAgt);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //�Ȃɂ��C���^�r���[����
            langchainOperator.interviewCharacter(testAgt, "How are you feeling right now?");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //�P���̉�b���n�߂�
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            //���i�̍s�����C���v�b�g������

        }

    }
}
