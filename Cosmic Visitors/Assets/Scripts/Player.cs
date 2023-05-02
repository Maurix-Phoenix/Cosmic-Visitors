using UnityEngine;


public class Player : MonoBehaviour, ISpaceShip
{
    public bool canMove;
    public GameObject Prefab;
    int health = 100;
    float moveSpeed = 15f;
    float fireRate = 0.5f;
    float currFireRate = 0;
    Vector3 direction;

    private void Start()
    {
        Instantiate(Prefab,transform);
    }

    private void OnEnable()
    {
        InputManager.InputFire += OnInputFire;
        InputManager.InputMoveLeft += OnInputMoveLeft;
        InputManager.InputMoveRight += OnInputMoveRight;

        EventManager.StageStart += OnStageStart;

    }

    private void OnDisable()
    {
        InputManager.InputFire -= OnInputFire;
        InputManager.InputMoveLeft -= OnInputMoveLeft;
        InputManager.InputMoveRight -= OnInputMoveRight;

        EventManager.StageStart -= OnStageStart;

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
            currFireRate = fireRate;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
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
