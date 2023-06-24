using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
public class CharacterInfinity : MonoBehaviour
{
    Rigidbody2D physic;
    
    public float JumpX = 10f;

    int points = 0;
    int totalScore;  // toplam skor verisi
    int newScore;    // girilen yeni skor verisi

    bool jumpBool = true;
    bool jumpBool2 = false;
    public bool speed = false;
    bool AllMoves = true;

    public float moveX = 0.0005f;
    public GameObject LostCanvas;
    public GameObject redCharacterObj;
    public GameObject bullet;
    public GameObject[] objs;


    public AudioSource auidoSound;
    public AudioClip[] sounds;

    public Text coinPoint;
    public Text fireText;
    public Transform characterRight;
    public Transform bulletPlane;
    public Transform[] objTrans; 

    public float eulerAngle = 0f;

    public GameObject skyBir;
    public GameObject skyIki;
    public float arkaPlanHizi = -1.5f;

    Rigidbody2D fizikBir;
    Rigidbody2D fizikIki;

    float uzunluk = 0;
    float timer = 0f;

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
        
        Score();

        fizikBir = skyBir.GetComponent<Rigidbody2D>();
        fizikIki = skyIki.GetComponent<Rigidbody2D>();


        uzunluk = skyBir.GetComponent<BoxCollider2D>().size.x;

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
            speedProcess();

        }
        fizikBir.velocity = new Vector2(arkaPlanHizi, 0);
        fizikIki.velocity = new Vector2(arkaPlanHizi, 0);
        characterSprite();

        backgroundMove();

        if (speed)
        {
            physic.velocity = new Vector2(moveX, 0);
        }


       
    }
 
    void backgroundMove()
    {
        if (skyBir.transform.position.x <= -uzunluk/2)
        {
            skyBir.transform.position += new Vector3(25.05f*2, 0);
        }
        if (skyIki.transform.position.x <= -uzunluk / 2)
        {
            skyIki.transform.position += new Vector3(25.05f * 2, 0);
        }
    }

    void characterSprite()
    {

        characterRight.transform.position = Vector3.MoveTowards(characterRight.transform.position, new Vector3(transform.position.x, transform.position.y), 0.3f);

        bulletPlane.transform.position = Vector3.MoveTowards(characterRight.transform.position, new Vector3(transform.position.x, transform.position.y), 0.3f);

    }

    void characterJump()
    {
        if (jumpBool)
        {
            if (Input.GetKeyDown("space"))
            {
                speed = false;
                physic.AddForce(new Vector2(0, 180f));
                auidoSound.clip = sounds[1];
                auidoSound.Play();
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
                speed = false;
                physic.AddForce(new Vector2(0, 100f));
                auidoSound.Play();
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
            
            arkaPlanHizi = -1f;
            
            moveX = -0.1f;   
            
            characterRight.transform.eulerAngles = new Vector3(0, 0, eulerAngle);

        }

        else if (Input.GetKeyUp("left shift"))
        {
            
            arkaPlanHizi = -1.5f;
           
            moveX = 0.0005f;

            characterRight.transform.eulerAngles = new Vector3(0, 0, 0);        
 
        }
    }
    void speedProcess()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveX= 0.1f;
            arkaPlanHizi = -2f;
            characterRight.transform.eulerAngles = new Vector3(0, 0, -eulerAngle);
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            moveX = 0.0005f;
            arkaPlanHizi = -1.5f;
            characterRight.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    void fireProcess()
    {
        if (timer == 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {

                timer = 10f;
                GameObject bulletTrans = Instantiate(bullet, bulletPlane.transform.position, Quaternion.identity);
                Rigidbody2D bulletrigid = bulletTrans.GetComponent<Rigidbody2D>();
                bulletrigid.velocity = new Vector2(10, 0);
                auidoSound.clip = sounds[2];
                auidoSound.Play();
                Destroy(bulletTrans, 1);
            }
        }
        else if (timer>=1f && timer<=10f)
        {
           
            do
            {
                
                timer -= Time.deltaTime;
                fireText.text = "Fire Time:"+ Mathf.Round(timer);
            } while (timer == 0f);
        }
       if (timer<=1f)
        {

            timer = 0f;
            fireText.text = "Fire Time:" + Mathf.Round(timer);
        }
        
    }

    void SimplePostRequest(string curScore)
    {
        string usernameGet = PlayerPrefs.GetString("username");
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

        wwwForm.Add(new MultipartFormDataSection("puan", curScore));
        wwwForm.Add(new MultipartFormDataSection("username", usernameGet));

        UnityWebRequest www = UnityWebRequest.Post(postURL, wwwForm);

        www.SendWebRequest();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        jumpBool = true;
        jumpBool2 = false;
        speed = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point")
        {
            points += 1;

            coinPoint.text = "POINTS: " + points;
            PlayerPrefs.SetInt("totalPoints", points);
            auidoSound.clip = sounds[0];
            auidoSound.Play();
        }
        if (collision.tag == "Death")
        {
            Time.timeScale = 0;
            
            LostCanvas.SetActive(true);
            if (points > 0)
            {
                SimplePostRequest(points.ToString());
            }
        }
        if (collision.tag == "Monster")
        {
            Time.timeScale = 0;
            LostCanvas.SetActive(true);
            if (points > 0)
            {

                SimplePostRequest(points.ToString());
            }
        }

        if (collision.tag == "translate")
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.25f);
        }
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;

    }
    public void SceneLevelLoad()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        Time.timeScale = 1;
    }
}
