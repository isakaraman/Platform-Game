using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class RedCharacter : MonoBehaviour
{

    Rigidbody2D physic;

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


    public GameObject WinCanvas;
    public GameObject LostCanvas;
    public GameObject redCharacterObj;

    public GameObject bullet;

    public AudioSource soundAudioSource;
    public AudioClip[] sounds;

    public Text coinPoint;

    public Transform characterRight;
    public Transform characterLeft;
    public Transform bulletPlane;
    public float eulerAngle = 0f;
   

    bool characterBool = true;

    int totalScore;  // toplam skor verisi
    int bulletDirection = 10;
    int newScore;    // girilen yeni skor verisi

    bool AllMoves = true;

    string postURL = "https://tunayyucel.com/oyun/puan.php";

    private void Awake()
    {
        int countGame = FindObjectsOfType<OptionsScript>().Length;
        if (countGame >= 1)
        {
            AllMoves = false;
        }
        else
        {
            AllMoves = true;
        }
    }
    void Start()
    {
        physic = GetComponent<Rigidbody2D>();

        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlk = kamera.transform.position - transform.position;

        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("kacinciLevel"))
        {
            PlayerPrefs.SetInt("kacinciLevel", SceneManager.GetActiveScene().buildIndex);
        }
        

        Score();
    }

    void Score()
    {
        newScore = 0;
        newScore = PlayerPrefs.GetInt("totalPoints");

        if (PlayerPrefs.HasKey("totalScoreKey"))  //totalScoreKey anahtarıyla kaydedilmiş bir veri var mı ?
        {
            totalScore = PlayerPrefs.GetInt("totalScoreKey"); // totalScoreKey anahtarıyla kaydedilmiş veriyi getir

        }
        else
        {

        }

        totalScore += newScore;
        PlayerPrefs.SetInt("totalScoreKey", totalScore);
        PlayerPrefs.SetInt("totalPoints", 0);
    }

    private void Update()
    {
        if (AllMoves)
        {
            characterJump();
            ShitProcess();
            fireProcess();

        }



        characterSprite();
        
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
    void characterSprite()
    {

        characterRight.transform.position = Vector3.MoveTowards(characterRight.transform.position, new Vector3(transform.position.x, transform.position.y), 0.3f);

        characterLeft.transform.position = Vector3.MoveTowards(characterRight.transform.position, new Vector3(transform.position.x, transform.position.y), 0.3f);

        bulletPlane.transform.position = Vector3.MoveTowards(characterRight.transform.position, new Vector3(transform.position.x, transform.position.y), 0.3f);
    }
    void characterMove()
    {
        if (moveBool)
        {
            physic.AddForce(new Vector2(MoveX, 0));
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
                soundAudioSource.clip = sounds[1];
                soundAudioSource.Play();
            }
            if (Input.GetKeyUp("space"))
            {
                physic.AddForce(new Vector2(0, 0));

                jumpBool = false;
                jumpBool2 = true;



            }

        }
        else if (jumpBool2 && !jumpBool)
        {
            if (Input.GetKeyDown("space"))
            {
                physic.AddForce(new Vector2(JumpX, 100f));
                soundAudioSource.clip = sounds[1];
                soundAudioSource.Play();
            }
            if (Input.GetKeyUp("space"))
            {
                physic.AddForce(new Vector2(0, 0));

                jumpBool2 = false;
            }
        }


    }

    void ShitProcess()
    {
        if (Input.GetKeyDown("left shift"))
        {
            physic.drag = 5f;
            if (characterBool)
            {
                characterRight.transform.eulerAngles = new Vector3(0, 0, eulerAngle);
            }
            else if (!characterBool)
            {
                characterLeft.transform.eulerAngles = new Vector3(0, 0, eulerAngle);
            }


        }
        else if (Input.GetKeyUp("left shift"))
        {
            physic.drag = 0f;
            if (characterBool)
            {
                characterRight.transform.eulerAngles = new Vector3(0, 0, 0);

            }
            else if (!characterBool)
            {
                characterLeft.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
    void fireProcess()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletTrans = Instantiate(bullet, bulletPlane.transform.position, Quaternion.identity);
            Rigidbody2D bulletrigid = bulletTrans.GetComponent<Rigidbody2D>();
            bulletrigid.velocity = new Vector2(bulletDirection, 0);
            Destroy(bulletTrans, 1);
            soundAudioSource.clip = sounds[2];
            soundAudioSource.Play();
        }
    }
    void kameraKontrol()
    {
        kameraSon = kameraIlk + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSon, 0.1f);
    }

    void SimplePostRequest(string curScore)
    {
        string usernameGet= PlayerPrefs.GetString("username");
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

        wwwForm.Add(new MultipartFormDataSection("puan", curScore));
        wwwForm.Add(new MultipartFormDataSection("username", usernameGet));

        UnityWebRequest www = UnityWebRequest.Post(postURL, wwwForm);

        www.SendWebRequest();
    }
    public void OnCollisionEnter2D(Collision2D collision)
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
        if (collision.tag == "Point")
        {
            points += 1;

            coinPoint.text = "POINTS: " + points;
            PlayerPrefs.SetInt("totalPoints", points);
            soundAudioSource.clip = sounds[0];
            soundAudioSource.Play();
            Destroy(collision.gameObject);
        }

        if (collision.tag == "LevelDoor")
        {

            Time.timeScale = 0;
            AllMovements = false;
            WinCanvas.SetActive(true);
            if (points>0)
            {
                SimplePostRequest(points.ToString());
            }

        }
        if (collision.tag == "Death")
        {
            Time.timeScale = 0;
            AllMovements = false;
            LostCanvas.SetActive(true);
            if (points > 0)
            {
                SimplePostRequest(points.ToString());
            }
        }
        if (collision.tag == "Monster")
        {
            Time.timeScale = 0;
            AllMovements = false;
            LostCanvas.SetActive(true);
            if (points>0)
            {
                SimplePostRequest(points.ToString());
            }
           
        }
        if (collision.tag == "Down")
        {
            AllMovements = false;
            physic.velocity = new Vector2(0, downVelocityY);
        }
        if (collision.tag == "Left")
        {
            AllMovements = true;
            MoveX = -2f;
            JumpX = -10f;
            downVelocityY = 0f;
            eulerAngle = 30f;
            bulletDirection =-10;
            characterBool = false;
            characterLeft.gameObject.SetActive(true);
            characterRight.gameObject.SetActive(false);
        }
        if (collision.tag=="Right")
        {
            AllMovements = true;
            MoveX = 2f;
            JumpX = 10f;
            downVelocityY = 0f;
            eulerAngle = -30f;
            bulletDirection = 10;
            characterBool = true;
            characterRight.gameObject.SetActive(true);
            characterLeft.gameObject.SetActive(false);
        }
        if (collision.tag=="translate")
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y+0.25f);
        }
    }


}
