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
    private string output;
    private string prevOutput;
    public bool isTextDisplaying;

    // ���̕�����\������܂ł̎���[s]
    [SerializeField] private float _delayDuration = 0.1f;
    private Coroutine _showCoroutine = null;

    string put = "Put";

    void Awake()
    {
        main = GetComponent<Main>();
        langchainOperator = GetComponent<LangChainOperator>();
        output = text_00.GetComponent<TextMeshProUGUI>().text;
        isTextDisplaying = false;
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
            langchainOperator.initializeCharacter(agents[0]);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //���i�̍s�����C���v�b�g������
            langchainOperator.inputObservation(agents[0], agents[0].GetComponent<Character>()._baseBehaviors);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //�Ȃɂ��C���^�r���[����
            langchainOperator.interviewCharacter(agents[0], interview_samples[0]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //�P���̉�b���n�߂�
            langchainOperator.runConversation(agents[0], agents[1], interview_samples[0]);
        }




        if (output != prevOutput)
        {
            StartCoroutine(showSentence(output));
        }

        prevOutput = output;
    }



    public void showInterviewContent(string msg)
    {
        string[] msgComp = msg.Split('|');
        string result = msgComp[1].Split('"')[1];

        output = result;
        //StartCoroutine(showSentence(output));
    }

    public void showConversationContent(string msg)
    {
        string[] msgComp = msg.Split('/');

        for(int i =0; i< msgComp.Length; i++)
        {
            StartCoroutine(showSentence(msgComp[i]));
        }


    }

    public void changeText(string script)
    {
        Debug.Log("Called");
        text_00.GetComponent<TextMeshProUGUI>().text = script;
        Debug.Log("Done");

    }

    private IEnumerator showSentence(string sentence)
    {
        isTextDisplaying = true;
        text_00.SetActive(false);
        text_00.GetComponent<TextMeshProUGUI>().text = output;
        text_00.GetComponent<TextMeshProSimpleAnimator>().enabled = true;
        text_00.SetActive(true);

        yield return new WaitForSeconds(text_00.GetComponent<TextMeshProSimpleAnimator>().speedPerCharacter * text_00.GetComponent<TextMeshProUGUI>().text.Length + 1);
        Debug.Log("Text output done!");
        isTextDisplaying = false;


        string[] conversations = sentence.Split('/');
        while()

    }

}
