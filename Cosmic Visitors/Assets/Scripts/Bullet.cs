using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public BulletTemplate BT = null;
    public Vector2 Direction = new Vector2(0, 0);
    public float Speed;
    public GameObject Prefab;
    void Start()
    {
        Prefab = BT.BulletPrefab;
        Speed = BT.Speed;
        
        Instantiate(Prefab,transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(BT.FollowEnemies)
        {
            //do stuff
        }
        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if the collision is Alien or other


        if(BT.OnHitOneKill)
        {
            //do stuff
        }
        if(BT.Bounce)
        {
            //do stuff
        }
        if(BT.SplitOnHit)
        {
            //do stuff
        }

        Destroy(gameObject);
    }
}
