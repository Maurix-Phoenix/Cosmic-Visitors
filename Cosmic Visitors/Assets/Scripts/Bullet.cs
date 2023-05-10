using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;
using static EventManager;

public class Bullet: MonoBehaviour
{
    public string OriginTag;
    public BulletTemplate BT = null;
    private float Speed;
    private int xDir = 0;
    private int yDir = 0;
    private bool isActive;
    

    private void OnEnable()
    {
        EventManager.GameOver += OnGameOver;
        EventManager.GameStart += OnGameStart;
        EventManager.GamePause += OnGamePause;
        EventManager.GameUnPause += OnGameUnPause;
    }
    private void OnDisable()
    {
        EventManager.GameOver -= OnGameOver;
        EventManager.GameStart -= OnGameStart;
        EventManager.GamePause -= OnGamePause;
        EventManager.GameUnPause -= OnGameUnPause;
    }

    void Start()
    {

        isActive = true;
        Speed = BT.Speed;

        if(OriginTag == "Alien")
        {
            yDir= -1;
        }
        if (OriginTag == "Player")
        {
            yDir = 1;
            Speed = BT.Speed*1.5f;
        }

    }

    // Update is called once per frame
    void Update()
    {      
        if(isActive)
        {
            if (BT.FollowEnemies)
            {
                //do stuff
            }
            else
            {
                transform.Translate(xDir, yDir * Speed * Time.deltaTime, 0);
            }


            if (transform.position.x > StageManager.Instance.RangeX || transform.position.x < -StageManager.Instance.RangeX ||
                transform.position.y > StageManager.Instance.RangeY || transform.position.y < -StageManager.Instance.RangeY - 5)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnGameStart()
    {
        isActive= false;
    }

    private void OnGamePause()
    {
        isActive = false;
    }

    private void OnGameUnPause()
    {
        isActive = true;
    }

    private void OnGameOver()
    {
        isActive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != OriginTag)
        {
            if(collision.gameObject.tag == "Player")
            {
               Player player = collision.gameObject.GetComponent<Player>();
               player.TakeDamage(BT.Damage);
            }
            else if(collision.gameObject.tag == "Alien")
            {
                Alien alien = collision.gameObject.GetComponent<Alien>();
                alien.TakeDamage(BT.Damage);
            }
            else if(collision.gameObject.tag == "AlienBoss")
            {
                AlienBoss alienBoss = collision.gameObject.GetComponent<AlienBoss>();
                alienBoss.TakeDamage(BT.Damage);
            }


            if (BT.Bounce)
            {
                //do something
                
            }
            
            if (BT.SplitOnHit)
            {
                //do something

            }

            if (BT.OnHitOneKill)
            {
                //Alien Boss can't die with onehitonekill
                if(collision.gameObject.tag != "AlienBoss")
                {

                }
                else
                {
                    Alien alien = collision.gameObject.GetComponent<Alien>();
                    alien.TakeDamage(alien.Health);
                }
            }

            else
            {
                Destroy(gameObject);
            }
        }


    }
}
