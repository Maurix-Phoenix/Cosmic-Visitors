using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour, ISpaceShip
{
    public bool canShoot = false;
    public bool isActive;
    public int Health;
    public float FireRate;
    public float Speed;
    public AlienTemplate AT;
    public Cell CurrentCell = null;
    CapsuleCollider2D cCollider;
    Rigidbody2D rb;

    private void Start()
    {
        isActive= true;
        canShoot = AT.canShoot;
        Health = AT.Health;
        FireRate= AT.FireRate;
        Speed = AT.Speed;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = AT.DrawOrder;
    }


    private void OnEnable()
    {
        
        EventManager.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= OnGameOver;
    }


    private void Update()
    {
        if(isActive)
        {
            Move();
            Shoot();
        }
    }

    public void Move()
    {
       int rX =  StageManager.Instance.RangeX;
       int rY = StageManager.Instance.RangeY;

        if (transform.position.y <= -rY)
        {
            GameManager.Instance.GameState = GameManager.State.GameOver;
        }
        else if (transform.position.y > rY)
        {
            transform.position = new Vector3(transform.position.x, rY);
        }

        if (transform.position.x < -rX)
        {
            transform.position = new Vector3(-rX, transform.position.y);
        }
        else if (transform.position.x > rX-1)
        {
            transform.position = new Vector3(rX-1, transform.position.y);
        }

        for (int i = 0; i < StageManager.Instance.Cells.Count; i++)
        {
            Cell cell = StageManager.Instance.Cells[i];
            if (Vector2.Distance(transform.position, cell.transform.position) < 0.3)
            {
                CurrentCell = cell;
            }
        }
        if (CurrentCell != null)
        {
            transform.Translate(CurrentCell.MoveDirection * AT.Speed * Time.deltaTime);
        }

    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <=0)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        if(canShoot)
        {
            FireRate -= Time.deltaTime;
            if (FireRate <= 0)
            {
                //shoot
                Bullet bullet = Instantiate(AT.Bullet.BulletPrefab,transform.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.BT = AT.Bullet;
                bullet.OriginTag = gameObject.tag;
                FireRate = AT.FireRate;
            }
        }

    }

    private void OnGameOver()
    {
        isActive= false;
    }


}
