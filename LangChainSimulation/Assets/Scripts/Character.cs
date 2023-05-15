using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string _name;
    public int _age;
    public string _characteristics;
    public string _currentState;
    public enum Emotion
    {
        Happy,
        Sad,
        Angry,
        Normal
    }
    private Emotion _emotion;


    public Character(string name, int age, string characteristics, string currentState, Emotion emotion)
    {
        _name = name;
        _age = age;
        _characteristics = characteristics;
        _currentState = currentState;
        _emotion = emotion;
    }

    public void expressEmotion(Emotion _curEmotion)
    {
        //キャラクターの感情に応じた絵文字を出力
    }
}
