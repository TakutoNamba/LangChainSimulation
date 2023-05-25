using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using TMPro;


/*
 * 用途に応じてLangChainにシミュレーションを出力してもらう
 */

public class LangChainOperator : MonoBehaviour
{
    TCP tcp;
    public GameObject _dataManager;
    private ViewController viewController;
    private int agentCode = 0;
    

    private void Start()
    {
        viewController = this.GetComponent<ViewController>();
        tcp = _dataManager.GetComponent<TCP>();
        tcp.Connect();
    }

    public void sendTest()
    {

    }

    public void initializeCharacter(GameObject agent)
    {
        Character character = agent.GetComponent<Character>();
        string msg = "0" + "|" + character._name + "|" + character._age + "|" + character._traits + "|" + character._currentState;
        tcp.Send(msg);
    }

    public void inputObservation(GameObject agent, string script)
    {
        string msg = "1" + "|" + agent.GetComponent<Character>()._name + "|" + script;
        tcp.Send(msg);
    }

    public void interviewCharacter(GameObject agent, string script)
    {
        string msg = "2" + "|" + agent.GetComponent<Character>()._name +"|" + script;
        tcp.Send(msg);
    }



    public void runConversation(GameObject agentA, GameObject agentB, string script)
    {
        string msg = "3" + "|" + agentA.GetComponent<Character>()._name + "|" + agentB.GetComponent<Character>()._name + "|" + script;
        tcp.Send(msg);
    }

    public void implementDaySimulation(GameObject agent, List<string> movements)
    {
        string script = "";
        for (int i =0; i<movements.Count; i++)
        {
            script += movements[i];
            if(i!=movements.Count-1)
            {
                script += ",";
            }
        }

        string msg = "3" + "|" + agent.GetComponent<Character>()._name + "|" + movements;
        tcp.Send(msg);
    }


    public void decideAction(string msg)
    {
        string[] msgComp = msg.Split('|');
        Debug.Log(msgComp[0]);
        string sample = msgComp[0];
        
        switch( msgComp[0].ToString() )
        {
            case "0":
                //インタビュー内容反映
                //viewController.showInterviewContent(msg);
                Debug.Log("Action 0 taken");
                break;
            case "1":
                //インタビュー内容反映
                Debug.Log("Action 1 taken");
                break;
            case "2":
                Debug.Log("passed here");
                viewController.showInterviewContent(msg);
                Debug.Log("Action 2 taken");
                break;
            //その他
            case "3":
                viewController.showConversationContent(msg);
                Debug.Log("Action 3 taken");
                break;
        }
    }

    public void getCharacterStatus(GameObject agent)
    {

    }
    public void updateCharacterFeeling(GameObject agent)
    {

    }

}
