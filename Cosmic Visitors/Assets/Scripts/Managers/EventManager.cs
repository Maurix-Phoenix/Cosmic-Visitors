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

    #region STAGE EVENTS
    public delegate void OnStageGenerated();
    public static event OnStageGenerated StageGenerated;
    public delegate void OnStageStart();
    public static event OnStageStart StageStart;
    public delegate void OnStageComplete();
    public static event OnStageComplete StageComplete;
    public delegate void OnBossStageStart();
    public static event OnBossStageStart BossStageStart;
    public delegate void OnBossStageComplete();
    public static event OnBossStageComplete BossStageComplete;

    public static void RaiseOnStageGenerated()
    {
        if(StageGenerated != null)
        {
            StageGenerated();
        }
    }

    public static void RaiseOnStageStart()
    {
        if(StageStart != null)
        {
            StageStart();
        }
    }

    public static void RaiseOnStageComplete()
    {
        if(StageComplete!=null)
        {
            StageComplete();
        }
    }

    public static void RaiseOnBossStageStart()
    {
        if(BossStageStart!=null)
        {
            BossStageStart();
        }
    }

    public static void RaiseOnBossStageComplete()
    {
        if(BossStageComplete!=null)
        {
            BossStageComplete();
        }
    }

    #endregion

    #endregion
}
