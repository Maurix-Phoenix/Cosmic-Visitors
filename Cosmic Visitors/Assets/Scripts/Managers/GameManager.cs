using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum State
    {
        None = -1,
        Playing,
        Paused,
        GameOver,
    }
    State gameState; 
    public State GameState 
    { 
        get{ return gameState; }
        set
        {
            switch (value)
            { 
                case State.None: { break; }
                case State.Playing: { UnPauseGame(); break; }
                case State.Paused: { PauseGame(); break; }
                case State.GameOver: { GameOver(); break; }
                default: { break; }
            }
        }
    }

    private void Awake()
    {
       //singleton
       if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
       else
        { 
            Instance = this; 
        }
    }

    private void Start()
    {
        UIManager.Instance.UIShowPanel("UIMainMenuPanel");
    }

    public void StartGame()
    {
        gameState= State.Playing;
        EventManager.RaiseOnGameStart();
    }
    private void PauseGame()
    {
        gameState= State.Paused;
        EventManager.RaiseOnGamePause();
    }
    private void UnPauseGame()
    {
        gameState = State.Playing;
        EventManager.RaiseGameUnPause();
    }
    private void GameOver()
    {
        gameState= State.GameOver;
        EventManager.RaiseOnGameOver();
    }
}
