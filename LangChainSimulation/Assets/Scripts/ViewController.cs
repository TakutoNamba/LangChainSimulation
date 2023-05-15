using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private Main _main;
    private CharacterController _characterController;
    void Start()
    {
        _main = GetComponent<Main>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            _main.runConversation();
        }
    }
}
