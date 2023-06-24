using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class CharacterControl : MonoBehaviour
{
    Rigidbody2D physic;

    public float speed = 0f;
    public float MoveX = 0f;
    public float JumpX = 10f;

    float downVelocityY = -1f;

    int points = 0;

    
    Vector3 kameraIlk;
    Vector3 kameraSon;
    GameObject kamera;

    bool moveBool = false;
    bool jumpBool = true;
    bool jumpBool2 = false;
    bool moveBoolActivate = false;
    bool AllMovements = true;
    bool shiftbool1 = true;
    bool shiftbool2 = false;


    public GameObject WinCanvas;
    public GameObject LostCanvas;
    public GameObject JumpCanvas;
    public GameObject SlowCanvas;

    public AudioSource jumpSound;

    public Button butonJump;
    public Button butonSlow;

    public Text coinPoint;



    

    void Start()
    {
        physic = GetComponent<Rigidbody2D>();
        
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlk = kamera.transform.position - transform.position;
       
        butonJump.gameObject.GetComponent<Button>().onClick.AddListener(jumpButton);

        butonSlow.gameObject.GetComponent<Button>().onClick.AddListener(ShiftButton);
    }

    private void Update()
    {
        characterJump();
        ShitProcess();
        
    }

    void FixedUpdate()
    {
        if (AllMovements)
        {
            characterMove();
           
        }

    }


    private void LateUpdate()
    {
        kameraKontrol();
    }

    void characterMove()
    {
        if (moveBool)
        {
            physic.AddForce(new Vector2(MoveX,0));    
        }
        
    }

   void characterJump()
    {
        if (jumpBool)
        {
        if (Input.GetKeyDown("space"))
        {
                moveBool = false;
                physic.AddForce(new Vector2(JumpX, 180f));
                jumpSound.Play();
        }
        if (Input.GetKeyUp("space"))
        {
            physic.AddForce(new Vector2(0, 0));

            jumpBool = false;
            jumpBool2 = true;

            
            
        }

        }
        else if (jumpBool2&&!jumpBool)
        {
            if (Input.GetKeyDown("space"))
            {
                physic.AddForce(new Vector2(JumpX, 100f));
                jumpSound.Play();
            }
            if (Input.GetKeyUp("space"))
            {
                physic.AddForce(new Vector2(0, 0));

                jumpBool2 = false;
            }
        }
        

    }
    void jumpButton()
    {
        moveBool = false;
        if (jumpBool)
        {
            physic.AddForce(new Vector2(JumpX, 180f));
            jumpSound.Play();
            jumpBool = false;
            jumpBool2 = true;
        }
        else if (jumpBool2)
        {
            physic.AddForce(new Vector2(JumpX, 100f));
            jumpSound.Play();
            jumpBool2 = false;
        }
       
    }

    void ShitProcess()
    {
        if (Input.GetKeyDown("left shift"))
        {
            physic.drag = 5f;
        }
        else if (Input.GetKeyUp("left shift"))
        {
            physic.drag = 0f;
        }
    }
    void ShiftButton()
    {
        if (shiftbool1)
        {
            physic.drag = 5f;
            shiftbool1 = false;
            shiftbool2 = true;
        }
        else if (shiftbool2)
        {
            physic.drag = 0f;
            shiftbool1 = true;
        }
       
    }


    void kameraKontrol()
    {
        kameraSon = kameraIlk + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSon, 0.1f);
    }
    public void ActiveScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);

    }
    public void NextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene+1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!moveBoolActivate)
        {
            moveBool = true;
        }
         
        jumpBool = true;
        jumpBool2 = false;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag=="LevelDoor")
        {
            int gamePoints = 0;
            int nowPoints = 0;
            int sonuc = 0;

            

            Destroy(gameObject);
            AllMovements = false;
            WinCanvas.SetActive(true);
            JumpCanvas.SetActive(false);
            SlowCanvas.SetActive(false);
            PlayerPrefs.SetInt("totalPoints", points);

            gamePoints = PlayerPrefs.GetInt("totalPoints");

            nowPoints = PlayerPrefs.GetInt("coin");

            sonuc = gamePoints + nowPoints;

            PlayerPrefs.SetInt("coin", sonuc);

        }
        if (collision.tag=="Death")
        {
            int gamePoints = 0;
            int nowPoints = 0;
            int sonuc = 0;

            Destroy(gameObject);
            AllMovements = false;
            LostCanvas.SetActive(true);
            JumpCanvas.SetActive(false);
            SlowCanvas.SetActive(false);

            PlayerPrefs.SetInt("totalPoints", points);

            gamePoints = PlayerPrefs.GetInt("totalPoints");

            nowPoints = PlayerPrefs.GetInt("coin");

            sonuc = gamePoints + nowPoints;

            PlayerPrefs.SetInt("coin", sonuc);
        }
        if (collision.tag=="Down")
        {
            AllMovements = false;
            physic.velocity = new Vector2(0, downVelocityY);
        }
        if (collision.tag=="Left")
        {
            AllMovements = true;
            MoveX = -2;
            JumpX = -10;
            downVelocityY = 0f;
        }
        if (collision.tag=="Point")
        {
            points += 1;

            coinPoint.text = "POINTS: " + points;

            Destroy(collision.gameObject);
        }
        
    }
    
  

}
