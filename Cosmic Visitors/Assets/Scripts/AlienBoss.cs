using UnityEngine;


public class AlienBoss : MonoBehaviour
{
    public AlienBossTemplate ABT;
    public BossAttackBehaviour BAB;
  
    private bool IsInFight = false;
    private bool canTakeDamage = false;
    private bool isActive = false;
    private int currHealth;
    private float animationSpeed = 2.0f;
    private int dirX = 0;
    private BossPhases _bossPhase;
    public BossPhases BossPhase 
    { 
        get 
        {
            return _bossPhase;
        } 
        set 
        {
            _bossPhase = value;
            PhaseCheck();
        } 
    }

    //behaviour variables
    private float[] moveT = new float[(int)BossPhases.ALL];
    private float[] stayT = new float[(int)BossPhases.ALL];
    private float[] shootT = new float[(int)BossPhases.ALL];
    private float bulletHellTC;
    private float dirChangeT = 1f;
    private bool canMove = false;
    private Vector3 scale = new Vector3(1, 1, 1);
    private float shotDelay = 0.3f;
    private float orbitsShots = 8;

    private void Start()
    {
        ABT.InitBoss();
        currHealth = ABT.Health;
        BossPhase = BossPhases.Discending;
        isActive= true;


        for(int i = 0; i< (int)BossPhases.ALL; i++)
        {
            moveT[i] = ABT.MoveTimer[i];
            stayT[i] = ABT.StayTimer[i];
            shootT[i] = ABT.ShotsTimer[i];
        }
        bulletHellTC = ABT.BulletHellTimeChange;
    }

    private void OnEnable()
    {
        EventManager.StageStart += OnStageStart;
        EventManager.GameUnPause += OnGameUnPause;
        EventManager.GamePause += OnGamePause;
        EventManager.GameOver += OnGameOver;
        EventManager.GameStart += OnGameStart;

    }

    private void OnDisable()
    {
        EventManager.StageStart -= OnStageStart;
        EventManager.GameUnPause -= OnGameUnPause;
        EventManager.GamePause -= OnGamePause;
        EventManager.GameOver -= OnGameOver;
        EventManager.GameStart -= OnGameStart;

    }

    private void Update()
    {
        if(isActive)
        {
            MovementsCheck();
            PhaseCheck();
        }
    }

    private void MovementsCheck()
    {
        if(IsInFight)
        {
            //DIRECTION
            dirChangeT -= Time.deltaTime;
            if (transform.position.x > -1 && transform.position.x < 1)
            {
                if (dirChangeT < 0)
                {
                    dirX = Random.value > 0.5 ? 1 : -1;
                    dirChangeT = 1f;
                }

            }
            else if (transform.position.x >= StageManager.Instance.RangeX - 0.5f)
            {
                dirX = -1;
            }
            else if (transform.position.x <= -StageManager.Instance.RangeX + 0.5f)
            {
                dirX = 1;
            }
            else { }
        }
    }

    void PhaseCheck()
    {
        switch (BossPhase)
        {
            case BossPhases.Discending:
                {
                    IsInFight = false;
                    canTakeDamage = false;
                    InitialAnimation();
                    break;
                }
            case BossPhases.Phase1:
                {
                    IsInFight = true;
                    canTakeDamage = true;
                    PerformAttackBehaviour(ABT.behaviourP1);
                    break;
                }
            case BossPhases.Phase2:
                {
                    IsInFight = true;
                    canTakeDamage = true;
                    PerformAttackBehaviour(ABT.behaviourP2);
                    break;
                }
            case BossPhases.Phase3:
                {
                    IsInFight = true;
                    canTakeDamage = true;
                    PerformAttackBehaviour(ABT.behaviourP3);
                    break;
                }
            case BossPhases.Dying:
                {
                    IsInFight = false;
                    canTakeDamage = false;
                    break;
                }
        }
    }

    private void PerformAttackBehaviour(BossAttackBehaviour bab)
    {
        if(IsInFight)
        {
            switch (bab)
            {
                case BossAttackBehaviour.Normal:
                    {
                        //boss move left and right shooting every x seconds.
                        BAB = BossAttackBehaviour.Normal;
                        PerformNormal();
                        break;
                    }
                case BossAttackBehaviour.OrbitShots:
                    {
                        //his shot follow an orbit.
                        BAB = BossAttackBehaviour.OrbitShots;
                        PerformOrbitShot();
                        break;
                    }
                case BossAttackBehaviour.ChargedShots:
                    {
                        //release multiple bigger bullets
                        BAB = BossAttackBehaviour.ChargedShots;
                        PerformChargedShot();

                        break;
                    }
                case BossAttackBehaviour.BulletHell:
                    {
                        //all the above
                        bab = BossAttackBehaviour.BulletHell;
                        PerformBulletHell(ABT.BulletHellTimeChange);
                        break;
                    }
            }
        }

    }
    private void PerformNormal()
    {
        int bossP = (int)BossPhase;

        if(canMove)
        {
            moveT[bossP] -= Time.deltaTime;
            transform.Translate(dirX * ABT.MoveSpeed * Time.deltaTime, 0, 0);
            if(moveT[bossP] <= 0)
            {
                canMove = false;
                moveT[bossP] = ABT.MoveTimer[bossP];
            }
        }
        else
        {
            stayT[bossP] -= Time.deltaTime;
            if (stayT[bossP] <= 0)
            {
                dirX = Random.value > 0.5 ? 1 : -1;
                canMove = true;
                stayT[bossP] = ABT.StayTimer[bossP];
            }
        }

        if (shootT[bossP] < 0)
        {
            SpawnBossBullet(transform.position, new Vector2(0,0));
            shootT[bossP] = ABT.ShotsTimer[bossP];
        }
        else
        {
            shootT[bossP] -= Time.deltaTime;
        }
    }
    private void PerformChargedShot() 
    {
        int bossP = (int)BossPhase;
        if (canMove)
        {
            moveT[bossP] -= Time.deltaTime;
            transform.Translate(dirX * ABT.MoveSpeed * Time.deltaTime, 0, 0);
            if (moveT[bossP] <= 0)
            {
                canMove = false;
                moveT[bossP] = ABT.MoveTimer[bossP];
            }
        }
        else
        {
            stayT[bossP] -= Time.deltaTime;
            if (stayT[bossP] <= 0)
            {
                dirX = Random.value > 0.5 ? 1 : -1;
                canMove = true;
                stayT[bossP] = ABT.StayTimer[bossP];
            }
        }

        if (shootT[bossP] < 0)
        {
            if(!canMove)
            {
                for(int i = 0; i < 3; i++)
                {
                    Vector3 bPos = new Vector3(transform.position.x + Random.Range(-i,i), transform.position.y);
                    SpawnBossBullet(bPos, scale);
                }
                
                shootT[bossP] = ABT.ShotsTimer[bossP];
                scale = new Vector3(0, 0);
            }
        }
        else
        {
            shootT[bossP] -= Time.deltaTime;
            scale = new Vector3(1+Time.deltaTime*2, 1+Time.deltaTime*2);
        }        
    }

    private void PerformOrbitShot() 
    {
        int bossP = (int)BossPhase;

        if (canMove)
        {
            moveT[bossP] -= Time.deltaTime;
            transform.Translate(dirX * ABT.MoveSpeed * Time.deltaTime, 0, 0);
            if (moveT[bossP] <= 0)
            {
                canMove = false;
                moveT[bossP] = ABT.MoveTimer[bossP];
            }
        }
        else
        {
            stayT[bossP] -= Time.deltaTime;
            if (stayT[bossP] <= 0)
            {
                dirX = Random.value > 0.5 ? 1 : -1;
                canMove = true;
                stayT[bossP] = ABT.StayTimer[bossP];
            }
        }

        if (shootT[bossP] < 0)
        { 
            shotDelay -= Time.deltaTime;
            if(shotDelay< 0)
            {
                if(orbitsShots > 0)
                {
                    scale = Vector2.zero;
                    SpawnBossBullet(CVProject.RandomPointOnCircumference(transform.position, 5), scale);
                    orbitsShots--;
                    shotDelay = 0.3f;
                }
                else
                {
                    orbitsShots = 8;
                    shootT[bossP] = ABT.ShotsTimer[bossP];
                }
            }
        }
        else
        {
            shootT[bossP] -= Time.deltaTime;
        }
    }
    private void PerformBulletHell(float timeChange)
    {
        PerformNormal();
       // PerformOrbitShot(); not working with others
        PerformChargedShot();
    }

    private void SpawnBossBullet(Vector3 position, Vector2 addScale)
    {
        BossBullet bb = Instantiate(ABT.BossBT[(int)BossPhase].BulletPrefab, position, Quaternion.identity).GetComponent<BossBullet>();
        bb.AlienBossOrigin = this;
        bb.BAB = BAB;
        bb.BBT = ABT.BossBT[(int)BossPhase];
        SpriteRenderer sr = bb.GetComponent<SpriteRenderer>();
        sr.size = sr.size + addScale;
        StageManager.Instance.BulletsList.Add(bb.gameObject);
    }


    private void OnGameStart()
    {
        isActive= false;
    }
    private void OnGamePause()
    {
        isActive= false;
    }
    private void OnGameUnPause()
    {
        isActive = true;
    }
    private void OnGameOver()
    {
        isActive = false;
    }

    private void OnStageStart()
    {
        Debug.Log("BOSS StageStarted");
        EventManager.RaiseOnBossStageStart();
    }
    public void InitialAnimation()
    {
        if(transform.position.y > 5)
        {
            transform.Translate(0, -1 * Time.deltaTime * animationSpeed, 0);
        }
        else
        {
            BossPhase++;
        }
    }
    public void FinalAnimation()
    {
        //going towards player but explode before
        //animation
        //vfx / sfx
        //destroy
        EventManager.RaiseOnBossStageComplete();
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currHealth -= damage;
            if (currHealth <= 0)
            {
                FinalAnimation();
            }

            if(currHealth <= ABT.Health - (ABT.Health/3) && currHealth > ABT.Health/3)
            {
                BossPhase = BossPhases.Phase2;
            }
            else if( currHealth <= ABT.Health /3 && currHealth > 0)
            {
                BossPhase = BossPhases.Phase3;
            }
        }
    }

}
