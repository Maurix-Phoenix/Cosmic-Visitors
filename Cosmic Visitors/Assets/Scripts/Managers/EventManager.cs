using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        //singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #region EVENTS
    #region GAMEFLOW EVENTS

    public delegate void OnGameStart();
    public static event OnGameStart GameStart;
    public delegate void OnGamePause();
    public static event OnGamePause GamePause;
    public delegate void OnGameUnPause();
    public static event OnGameUnPause GameUnPause;
    public delegate void OnGameOver();
    public static event OnGameOver GameOver;

    public static void RaiseOnGameStart()
    {
        if(GameStart!=null)
        {
            GameStart();
        }
    }
    public static void RaiseOnGamePause()
    {
        if (GamePause != null)
        {
            GamePause();
        }
    }
    public static void RaiseGameUnPause()
    {
        if(GameUnPause != null)
        {
            GameUnPause();
        }
    }
    public static void RaiseOnGameOver()
    {
        if (GameOver != null)
        {
            GameOver();
        }
    }
    #endregion
    #endregion
}
