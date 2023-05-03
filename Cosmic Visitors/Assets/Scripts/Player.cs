using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour, ISpaceShip
{
    public BulletTemplate[] BT = null;
    public bool canMove;
    int maxHealth = 5;
    int currhealth;
    float moveSpeed = 15f;
    float fireRate = 0.1f;
    float currFireRate = 0;
    Vector3 direction;

    private void OnEnable()
    {
        InputManager.InputFire += OnInputFire;
        InputManager.InputMoveLeft += OnInputMoveLeft;
        InputManager.InputMoveRight += OnInputMoveRight;

        EventManager.StageStart += OnStageStart;
        EventManager.StageComplete += OnStageComplete;

    }

    private void OnDisable()
    {
        InputManager.InputFire -= OnInputFire;
        InputManager.InputMoveLeft -= OnInputMoveLeft;
        InputManager.InputMoveRight -= OnInputMoveRight;

        EventManager.StageStart -= OnStageStart;
        EventManager.StageComplete -= OnStageComplete;

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
        currFireRate= 0;
    }
    private void OnStageComplete()
    {
        currhealth = maxHealth;
    }

    public void Move()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        direction = Vector3.zero;
    }

    public void Shoot()
    {
       if(currFireRate <= 0)
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

    public void TakeDamage(int damage)
    {
        currhealth -= damage;
        if (currhealth <= 0)
        {
            GameManager.Instance.GameState = GameManager.State.GameOver;
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
