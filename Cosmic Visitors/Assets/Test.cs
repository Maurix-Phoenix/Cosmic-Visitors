using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.GameStart += OnGameStart;
    }

    private void OnDisable()
    {
        EventManager.GameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        Debug.Log("GameStarted");
    }
}
