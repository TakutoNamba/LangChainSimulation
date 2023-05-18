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
            //新しくキャラクターを生成
            langchainOperator.initializeCharacter(testAgt);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //なにかインタビューする
            langchainOperator.interviewCharacter(testAgt, "How are you feeling right now?");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //１日の会話を始める
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            //普段の行動をインプットさせる

        }

    }
}
