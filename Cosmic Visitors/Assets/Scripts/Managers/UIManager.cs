using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EventManager;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public List<GameObject> UIPanelsList;
    public List<TMP_Text> UIElementsList;

    private void Awake()
    {
        //Singleton
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        EventManager.GameOver += OnGameOver;
        EventManager.GameStart += OnGameStart;
        EventManager.GamePause += OnGamePause;
        EventManager.GameUnPause += OnGameUnPause;

        EventManager.StageComplete += OnStageComplete;
        EventManager.StageGenerated += OnStageGenerated;
        EventManager.StageStart += OnStageStart;
        EventManager.BossStageStart += OnBossStageStart;
    }
    private void OnDisable()
    {
        EventManager.GameOver -= OnGameOver;
        EventManager.GameStart -= OnGameStart;
        EventManager.GamePause -= OnGamePause;
        EventManager.GameUnPause -= OnGameUnPause;

        EventManager.StageComplete -= OnStageComplete;
        EventManager.StageGenerated -= OnStageGenerated;
        EventManager.StageStart -= OnStageStart;
        EventManager.BossStageStart -= OnBossStageStart;
    }

    #region METHODS
    public void UIShowPanel(string uiPanelName)
    {
        for(int i = 0; i < UIPanelsList.Count; i++)
        {
            if (UIPanelsList[i].name == uiPanelName)
            {
                UIPanelsList[i].SetActive(true);
            }
        }
    }
    public void UIHidePanel(string uiPanelName)
    {
        for (int i = 0; i < UIPanelsList.Count; i++)
        {
            if (UIPanelsList[i].name == uiPanelName)
            {
                UIPanelsList[i].SetActive(false);
            }
        }
    }

    private void UIHideAll()
    {
        for (int i = 0; i < UIPanelsList.Count; i++)
        {
            UIHidePanel(UIPanelsList[i].name);
        }
    }


    public void UIUpdateElement(string uiElementName, string content)
    {
        for (int i = 0; i < UIElementsList.Count; i++)
        {
            if (UIElementsList[i].name == uiElementName)
            {
                UIElementsList[i].text = " ";
                UIElementsList[i].text = content;
            }
        }
    }
    #endregion


    #region EVENTS LISTENER
    private void OnGameOver()
    {
        UIShowPanel("UIGameOverPanel");
    }
    private void OnGameStart()
    {
        UIHideAll();
    }

    private void OnGamePause()
    {
        UIShowPanel("UIPausePanel");
    }
    private void OnGameUnPause()
    {
        UIHidePanel("UIPausePanel");
    }
    private void OnStageComplete()
    {
        
    }
    private void OnStageGenerated()
    {
        UIShowPanel("UIStageStartingPanel");
    }
    private void OnStageStart()
    {
        UIHidePanel("UIStageStartingPanel");
    }
    private void OnBossStageStart()
    {
        UIHidePanel("UIStageStartingPanel");
    }

    #endregion



}
