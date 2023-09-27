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

        Time.timeScale = 1.0f;
        gameState= State.Playing;
        EventManager.RaiseOnGameStart();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        gameState= State.Paused;
        EventManager.RaiseOnGamePause();
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        gameState = State.Playing;
        EventManager.RaiseGameUnPause();
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameState= State.GameOver;
        EventManager.RaiseOnGameOver();
    }
}
