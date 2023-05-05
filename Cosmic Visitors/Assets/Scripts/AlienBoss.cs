using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Xml;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class AlienBoss : MonoBehaviour
{
    public AlienBossTemplate ABT;

    public bool IsInFight = false;
    public bool canTakeDamage = false;

    private int currHealth;
    private float animationSpeed = 2.0f;
    private int dirX = 0;

   


    public BossPhases BossPhase;

    private float[] moveT = new float[(int)BossPhases.ALL];
    private float[] stayT = new float[(int)BossPhases.ALL];
    private float[] shootT = new float[(int)BossPhases.ALL];

    private void Start()
    {
        ABT.InitBoss();
        currHealth = ABT.Health;
        BossPhase = BossPhases.Discending;

        for(int i = 0; i< (int)BossPhases.ALL; i++)
        {
            moveT[i] = ABT.MoveTimer[i];
            stayT[i] = ABT.StayTimer[i];
            shootT[i] = ABT.ShotsTimer[i];
        }

    }

    private void OnEnable()
    {
        EventManager.StageStart += OnStageStart;      

    }

    private void OnDisable()
    {
        EventManager.StageStart += OnStageStart;      
    }

    private void Update()
    {
        switch(BossPhase)
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
                    IsInFight= true;
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
                    IsInFight= false;
                    canTakeDamage = false;
                    break;
                }

        }
    }

    private void PerformAttackBehaviour(BossAttackBehaviour bab)
    {
        switch(bab)
        {
            case BossAttackBehaviour.Normal:
            {
                    //boss move left and right shooting every x seconds.
                    PerformNormal();                    
                    break;
            }
            case BossAttackBehaviour.OrbitShots:
            { 
                    //boss stay at the center of the stage the shots he shot follow an orbit.
                    break; 
            }
            case BossAttackBehaviour.ChargedShots:
            {
                    //boss move left and right does not attack when moving but release a scaled bullet once stay in position
                    PerformChargedShot();

                    break;
            }
            case BossAttackBehaviour.BulletHell:
            {
                    //all the above
                    break;
            }
        }
    }

    float dirChangeT = 1f;
    bool canMove = false;
    private void PerformNormal()
    {
        int bossP = (int)BossPhase;
        //DIRECTION
        dirChangeT -= Time.deltaTime;
        if (transform.position.x > -1 && transform.position.x < 1)
        {
            if(dirChangeT <0)
            {
                dirX = Random.value > 0.5 ? 1 : -1;
                dirChangeT = 1f;
            }
            
        }
        else if (transform.position.x >= StageManager.Instance.RangeX-0.5f)
        {
            dirX = -1;
        }
        else if (transform.position.x <= -StageManager.Instance.RangeX+0.5f)
        {
            dirX = 1;
        }
        else { }


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

    Vector3 scale = new Vector3(1,1,1);
    private void PerformChargedShot() 
    {
        int bossP = (int)BossPhase;
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
                SpawnBossBullet(transform.position, scale);
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
    
    }
    private void PerformBulletHell()
    {
    
    }

    private void SpawnBossBullet(Vector3 position, Vector2 addScale)
    {
        BossBullet bb = Instantiate(ABT.BossBT[(int)BossPhase].BulletPrefab, position, Quaternion.identity).GetComponent<BossBullet>();
        bb.AlienBossOrigin = this;
        bb.BAB = BossAttackBehaviour.Normal;
        bb.BBT = ABT.BossBT[(int)BossPhase];
        SpriteRenderer sr = bb.GetComponent<SpriteRenderer>();
        sr.size = sr.size + addScale;
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
