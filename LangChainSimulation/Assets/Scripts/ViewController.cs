using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;



public class ViewController : MonoBehaviour
{
    private Main main;
    private LangChainOperator langchainOperator;

    public GameObject testAgt;
    public GameObject[] agents;

    public GameObject text_00;
    public GameObject text_01;
    private string output;
    private string prevOutput;

    // 次の文字を表示するまでの時間[s]
    [SerializeField] private float _delayDuration = 0.1f;
    private Coroutine _showCoroutine = null;

    string put = "Put";

    void Awake()
    {
        main = GetComponent<Main>();
        langchainOperator = GetComponent<LangChainOperator>();
        output = text_00.GetComponent<TextMeshProUGUI>().text;
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
        

        if(output != prevOutput)
        {
            text_00.SetActive(false);
            text_00.GetComponent<TextMeshProUGUI>().text = output;
            text_00.GetComponent<TextMeshProSimpleAnimator>().enabled = true;
            text_00.SetActive(true);
        }

        prevOutput = output;
    }



    public void showInterviewContent(string msg)
    {
        string[] msgComp = msg.Split('|');
        string result = msgComp[1].Split('"')[1];

        output = result;
    }

    public void changeText(string script)
    {
        Debug.Log("Called");
        text_00.GetComponent<TextMeshProUGUI>().text = script;
        Debug.Log("Done");

    }

}
