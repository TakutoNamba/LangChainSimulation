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
    public GameObject[] agents;
    private int agentCode = 0;
    

    private void Start()
    {
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

    public void interviewCharacter(GameObject agent, string script)
    {
        string msg = "1" + "|" + agent.GetComponent<Character>()._name +"|" + script;
        tcp.Send(msg);
    }

    public void runConversation(GameObject agentA, string script)
    {
        string msg = "2" + "|" + agentA.GetComponent<Character>()._name + "|" + script;
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

    public void getCharacterStatus(GameObject agent)
    {

    }
    public void updateCharacterFeeling(GameObject agent)
    {

    }

}
