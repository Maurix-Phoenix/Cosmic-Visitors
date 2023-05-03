using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public string OriginTag;
    public BulletTemplate BT = null;
    public float Speed;
    private int yDir = 0;
    void Start()
    {
       
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
        if(BT.FollowEnemies)
        {
            //do stuff
        }
        transform.Translate(0, yDir * Speed * Time.deltaTime,0);

        if(transform.position.x > StageManager.Instance.RangeX || transform.position.x < -StageManager.Instance.RangeX ||
            transform.position.y > StageManager.Instance.RangeY || transform.position.y < -StageManager.Instance.RangeY -5)
        { 
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != OriginTag)
        {
            Debug.Log("hit " + collision.gameObject.name);

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


            if (BT.Bounce)
            {
                //do something
            }
            else if (BT.SplitOnHit)
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
