using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    bool playingStage = false;

    public int StageNumber = 0;
    public Stage CurrentStage = null;

    public int RangeX = 20;
    public int RangeY = 11;

    public List<Cell> Cells = new List<Cell>();
    public List<Cell> UsableCells = new List<Cell>();

    [HideInInspector] public Player Player;
    public GameObject PlayerPrefab;
    public Vector3 PlayerSpawnPos = new Vector3(0,-11,0);

    public List<AlienTemplate> AlienTemplates = new List<AlienTemplate>();
    public List<Alien> Aliens = new List<Alien>();

    [HideInInspector] public AlienBoss AlienBoss = null;
    public AlienBossTemplate AlienBossTemplate;
    public Vector3 AlienBossSpawnPos = new Vector3(0,15,0);

    private void Awake()
    {
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
        EventManager.GameStart += OnGameStart;
        EventManager.GamePause += OnGamePause;
        EventManager.GameOver += OnGameOver;
        EventManager.StageGenerated += OnStageGenerated;
        EventManager.BossStageComplete += OnBossStageComplete;
    }

    private void OnDisable()
    {
        EventManager.GameStart -= OnGameStart;
        EventManager.GamePause -= OnGamePause;
        EventManager.GameOver -= OnGameOver;
        EventManager.StageGenerated -= OnStageGenerated;
        EventManager.BossStageComplete -= OnBossStageComplete;


    }

    private void Update()
    {
        if(playingStage)
        {
            if(StageNumber % 5 != 0) 
            {
                Debug.Log("playingStage");
                if (Aliens.Count < 1)
                {
                    StageComplete();
                }
            }
        }
    }

    private void OnGameStart()
    {
        GenerateStage();
    }

    private void OnGamePause()
    {
        
    }
    private void OnGameOver()
    {
        StageNumber = 0;
        for(int i = 0; i < Aliens.Count; i++)
        {
            Aliens[i].isActive = false;
        }
    }

    private void OnStageGenerated()
    {
        Debug.Log("New Stage");
        SpawnPlayer();
        StartStage();
        StartCoroutine("StartStage");
    }

    public void StartStage() //should be COROUTINE
    {
        //setting a 5 sec timer here..
        if(StageNumber % 5 != 0)
        {
            EventManager.RaiseOnStageStart();
        }
        else
        {
            RaiseOnBossStageStart();
        }

        playingStage = true;
        
    }

    public void GenerateStage()
    {
        playingStage= false;
        if(CurrentStage == null)
        {
            CurrentStage = new GameObject($"Stage").AddComponent<Stage>();
            CurrentStage.GenerateGrid(RangeX, RangeY);
        }

        ClearStage();
        StageNumber++;
        CurrentStage.GenerateStage(StageNumber);
        CurrentStage.name = $"Stage {StageNumber}";
    }    

    public void ClearStage()
    {
        for(int i = 0; i < Aliens.Count; i++)
        {
            Destroy(Aliens[i]);
        }
        Aliens.Clear();

        if(AlienBoss != null)
        {
            Destroy(AlienBoss.gameObject);
        }
    }

    private void StageComplete()
    {
        playingStage= false;
        Debug.Log("StageCompleted");
        EventManager.RaiseOnStageComplete();
        GenerateStage();
    }

    private void OnBossStageComplete()
    {
        StageComplete();
    }

    private void SpawnPlayer()
    {
        if(Player == null)
        {
            Player = Instantiate(PlayerPrefab, PlayerSpawnPos, Quaternion.identity).GetComponent<Player>();
        }
        else
        {
            Player.transform.position = PlayerSpawnPos;
        }
    }
}
