using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static EventManager;

public class BossBullet : MonoBehaviour
{

    public AlienBoss AlienBossOrigin;
    public Player Player;
    private string OriginTag = "";
    public BossAttackBehaviour BAB;
    public BulletTemplate BBT;
    public Vector2 Direction;

    private float waitTime = 1f;
    private float waitT;
    private bool isActive;

    private void OnEnable()
    {
        /*EventManager.GameOver += OnGameOver;
        EventManager.GameStart += OnGameStart;
        EventManager.GamePause += OnGamePause;
        EventManager.GameUnPause += OnGameUnPause;*/
    }
    private void OnDisable()
    {
       /* EventManager.GameOver -= OnGameOver;
        EventManager.GameStart -= OnGameStart;
        EventManager.GamePause -= OnGamePause;
        EventManager.GameUnPause -= OnGameUnPause;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        isActive= true;
        Player = StageManager.Instance.Player;
        OriginTag = AlienBossOrigin.tag;
        waitT = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            if (transform.position.x > StageManager.Instance.RangeX || transform.position.x < -StageManager.Instance.RangeX ||
            transform.position.y > StageManager.Instance.RangeY || transform.position.y < -StageManager.Instance.RangeY - 5)
            {
                Destroy(gameObject);
            }

            MoveBB(BAB);
        }

    }

    private void MoveBB(BossAttackBehaviour bab)
    {
        switch(bab)
        {
            case BossAttackBehaviour.Normal: 
                {
                    Direction = new Vector2(0, -1);
                    transform.Translate(Direction.x * Time.deltaTime * BBT.Speed, Direction.y * Time.deltaTime * BBT.Speed, 0);
                    break; 
                }
            case BossAttackBehaviour.ChargedShots: 
                {
                    Direction = new Vector2(0, -1); 
                    transform.Translate(Direction.x * Time.deltaTime * BBT.Speed, Direction.y * Time.deltaTime * BBT.Speed, 0);
                    break;
                }
            case BossAttackBehaviour.OrbitShots: 
                {
                    Direction = new Vector2(0, 0);
                    waitT -= Time.deltaTime;

                    if(waitT < 0)
                    {
                        Direction.x = Player.transform.position.x - transform.position.x;
                        Direction.y = Player.transform.position.y - transform.position.y -2;
                        Direction = Direction.normalized;
                    }
                    
                    transform.Translate(Direction.x * BBT.Speed * Time.deltaTime, Direction.y * BBT.Speed * Time.deltaTime,  0);

                    break; 
                }
            case BossAttackBehaviour.BulletHell: 
                { 
                    break; 
                }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != OriginTag && collision.gameObject.tag != gameObject.tag)
        {
            Debug.Log("hit " + collision.gameObject.name);

            if (collision.gameObject.tag == "Player")
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.TakeDamage(BBT.Damage);
            }
            else if (collision.gameObject.tag == "Alien")
            {
                Alien alien = collision.gameObject.GetComponent<Alien>();
                alien.TakeDamage(BBT.Damage);
            }


            if (BBT.Bounce)
            {
                //do something
            }
            else if (BBT.SplitOnHit)
            {
                //do something
            }
            else
            {
                Destroy(gameObject);
            }
        }


    }
}
