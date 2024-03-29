using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        //singleton
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.Instance.GameState == GameManager.State.Playing)
            {
                //esc Pause/unpause
                GameManager.Instance.GameState = GameManager.State.Paused;
                RaiseOnInputPause();
            }
            else
            {
                GameManager.Instance.GameState = GameManager.State.Playing;
                RaiseOnInputUnpause();
            }

        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //moveleft
            RaiseOnInputMoveLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //move right
            RaiseOnInputMoveRight();

        }

        if (Input.GetKey(KeyCode.Space))
        {
            //fire
            RaiseOnInputFire();
        }
    }

    public delegate void OnInputPause();
    public static event OnInputPause InputPause;
    public delegate void OnInputUnpause();
    public static event OnInputUnpause InputUnpause;
    public delegate void OnInputMoveLeft();
    public static event OnInputMoveLeft InputMoveLeft;
    public delegate void OnInputMoveRight();
    public static event OnInputMoveRight InputMoveRight;
    public delegate void OnInputFire();
    public static event OnInputFire InputFire;

    public static void RaiseOnInputPause()
    {
        if (InputPause != null)
        {
            InputPause();
        }
    }

    public static void RaiseOnInputUnpause()
    {
        if(InputUnpause != null)
        {
            InputUnpause();
        }
    }

    public static void RaiseOnInputMoveLeft()
    {
        if (InputMoveLeft != null)
        {
            InputMoveLeft();
        }
    }
    public static void RaiseOnInputMoveRight()
    {
        if (InputMoveRight != null)
        {
            InputMoveRight();
        }
    }
    public static void RaiseOnInputFire()
    {
        if (InputFire != null)
        {
            InputFire();
        }
    }


}
