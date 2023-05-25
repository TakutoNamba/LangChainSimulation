using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;



public class ViewController : MonoBehaviour
{
    private Main main;
    private LangChainOperator langchainOperator;

    public GameObject[] agents;
    public string[] interview_samples;

    public GameObject text_00;
    public GameObject text_01;
    public GameObject text_name;

    private string output;
    private string prevOutput;
    public bool isTextDisplaying;

    // 次の文字を表示するまでの時間[s]
    [SerializeField] private float _delayDuration = 0.1f;


    void Awake()
    {
        main = GetComponent<Main>();
        langchainOperator = GetComponent<LangChainOperator>();
        output = text_00.GetComponent<TextMeshProUGUI>().text;
        isTextDisplaying = false;
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Q))
        {
            //新しくキャラクターを生成
            langchainOperator.initializeCharacter(agents[0]);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //普段の行動をインプットさせる
            langchainOperator.inputObservation(agents[0], agents[0].GetComponent<Character>()._baseBehaviors);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //なにかインタビューする
            //resetText(false);
            langchainOperator.interviewCharacter(agents[0], interview_samples[0]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //１日の会話を始める
            //resetText(false);
            text_name.GetComponent<TextMeshProUGUI>().text = agents[0].GetComponent<Character>()._name;
            output = interview_samples[0];
            langchainOperator.runConversation(agents[0], agents[1], interview_samples[0]);
        }




        if (output != prevOutput)
        {
            Debug.Log(output);
            StartCoroutine(showSentence(output));
            prevOutput = output;
        }

    }



    public void showInterviewContent(string msg)
    {
        string[] msgComp = msg.Split('|');
        string result = msgComp[1].Split('"')[1];

        output = result;
        Debug.Log(output + "  " + prevOutput);
    }

    public void showConversationContent(string msg)
    {
        string[] msgComp = msg.Split('|');
        string result = msgComp[2];

        output = result;
        Debug.Log(output + "  " + prevOutput);
    }

    public void resetText(bool isVisible)
    {
        text_name.SetActive(isVisible);
    }

    private IEnumerator showSentence(string sentence)
    {

        string[] sentences = sentence.Split('/');
        int count = 0;
        isTextDisplaying = true;

        while (count<sentences.Length)
        {
            text_00.SetActive(false);
            text_00.GetComponent<TextMeshProUGUI>().text = sentences[count];
            text_00.GetComponent<TextMeshProSimpleAnimator>().enabled = true;
            text_00.SetActive(true);

            yield return new WaitForSeconds(text_00.GetComponent<TextMeshProSimpleAnimator>().speedPerCharacter * text_00.GetComponent<TextMeshProUGUI>().text.Length + 1);
            count++;
        }

        Debug.Log("Text output done!");
        isTextDisplaying = false;

    }

}
