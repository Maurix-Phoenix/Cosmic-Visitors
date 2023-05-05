using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour, ISpaceShip
{
    public BulletTemplate[] BT = null;
    public bool canMove = false;
    public bool canShoot = false;
    int maxHealth = 5;
    int currhealth;
    float moveSpeed = 15f;
    float fireRate = 0.03f;
    float currFireRate = 0;
    Vector3 direction;

    private void Start()
    {
        currhealth = maxHealth;
    }

    private void OnEnable()
    {
        

        InputManager.InputFire += OnInputFire;
        InputManager.InputMoveLeft += OnInputMoveLeft;
        InputManager.InputMoveRight += OnInputMoveRight;

        EventManager.GameOver += OnGameOver;
        EventManager.StageStart += OnStageStart;
        EventManager.StageComplete += OnStageComplete;
        EventManager.BossStageStart += OnBossStageStart;
        EventManager.BossStageComplete += OnBossStageComplete;

    }

    private void OnDisable()
    {
        InputManager.InputFire -= OnInputFire;
        InputManager.InputMoveLeft -= OnInputMoveLeft;
        InputManager.InputMoveRight -= OnInputMoveRight;

        EventManager.GameOver -= OnGameOver;
        EventManager.StageStart -= OnStageStart;
        EventManager.StageComplete -= OnStageComplete;
        EventManager.BossStageStart -= OnBossStageStart;
        EventManager.BossStageComplete -= OnBossStageComplete;


    }

    private void Update()
    {
        if(currFireRate > 0)
        {
            currFireRate-=Time.deltaTime;
        }
    }

    private void OnStageStart()
    {
        canMove = true;
        canShoot = true;
        currFireRate= 0;
    }
    private void OnStageComplete()
    {
        currhealth = maxHealth;
    }

    private void OnBossStageStart()
    {
        canMove = true;
        canShoot = true;
    }
    private void OnBossStageComplete()
    {

    }

    private void OnGameOver()
    {
        canMove= false;
        canShoot= false;

        // death animation here...

        gameObject.SetActive(false);
    }


    public void Move()
    {
        if(canMove)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            direction = Vector3.zero;
        }
    }

    public void Shoot()
    {
        if(canShoot)
        {
            if (currFireRate <= 0)
            {
                //shoothere
                Debug.Log("Player Shoot");

                //shoot
                Bullet bullet = Instantiate(BT[0].BulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.BT = BT[0];
                bullet.OriginTag = gameObject.tag;

                currFireRate = fireRate;
            }
        }

    }

    public void TakeDamage(int damage)
    {
        currhealth -= damage;
        if (currhealth <= 0)
        {
            GameManager.Instance.GameState = GameManager.State.GameOver;
            canShoot = false;
            canMove = false;
        }
    }

    private void OnInputMoveLeft()
    {
        direction = new Vector3(-1,0,0);
        Move();
    }
    private void OnInputMoveRight()
    {
        direction = new Vector3(1,0,0);
        Move();

    }
    private void OnInputFire()
    {
        Shoot();
    }


}
