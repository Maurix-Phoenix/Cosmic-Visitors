using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBullet : MonoBehaviour
{

    public AlienBoss AlienBossOrigin;
    private string OriginTag = "";
    public BossAttackBehaviour BAB;
    public BulletTemplate BBT;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        OriginTag = AlienBossOrigin.tag;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > StageManager.Instance.RangeX || transform.position.x < -StageManager.Instance.RangeX ||
        transform.position.y > StageManager.Instance.RangeY || transform.position.y < -StageManager.Instance.RangeY - 5)
        {
            Destroy(gameObject);
        }

        MoveBB(BAB);
    }

    private void MoveBB(BossAttackBehaviour bab)
    {
        switch(bab)
        {
            case BossAttackBehaviour.Normal: 
                {
                    direction = new Vector2(0, -1);
                    transform.Translate(direction.x * Time.deltaTime * BBT.Speed, direction.y * Time.deltaTime * BBT.Speed, 0);
                    break; 
                }
            case BossAttackBehaviour.ChargedShots: 
                {
                    direction = new Vector2(0, -1); 
                    transform.Translate(direction.x * Time.deltaTime * BBT.Speed, direction.y * Time.deltaTime * BBT.Speed, 0);
                    transform.localScale = transform.localScale * 1.3f;
                    break;
                }
            case BossAttackBehaviour.OrbitShots: 
                {
                    
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
        if (collision.gameObject.tag != OriginTag)
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
