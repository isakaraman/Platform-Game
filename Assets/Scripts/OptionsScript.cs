using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OptionsScript : MonoBehaviour
{
    public Button butonJump;
    public Button butonSlow;
    public Button butonFire;
    public Button butonFast;

    public GameObject bullet;

    public AudioSource soundSource;
    public AudioClip[] sounds;
    Rigidbody2D characterRigid;

    Transform characterRight;
    Transform characterLeft;

    CharacterInfinity scriptValue;

    bool fastBool = true;
    bool fastBool2 = false;
    bool jumpBool = true;
    bool jumpBool2 = false;
    bool shiftbool1 = true;
    bool shiftbool2 = false;
    bool characterBool = true;
    bool infinityController = false;

    public float timer = 0f;
    public float JumpX = 10f;
    public float eulerAngle = 0f;
    
    void Awake()
    {

        int countGame = FindObjectsOfType<OptionsScript>().Length;
        if (countGame > 1)
        {
            
            GameObject obj = GameObject.FindGameObjectWithTag("controllers").gameObject;
            Destroy(obj);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        

    }
    private void Start()
    {
        
        butonJump.gameObject.GetComponent<Button>().onClick.AddListener(jumpButton);
        butonSlow.gameObject.GetComponent<Button>().onClick.AddListener(ShiftButton);
        butonFire.gameObject.GetComponent<Button>().onClick.AddListener(FireButton);
        butonFast.gameObject.GetComponent<Button>().onClick.AddListener(fastButton);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            timer = 0f;
        }
   
    }
    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            butonJump.gameObject.SetActive(false);
            butonSlow.gameObject.SetActive(false);
            butonFire.gameObject.SetActive(false);
            butonFast.gameObject.SetActive(false);

        }
        else if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            int characterLeftCount = FindObjectsOfType<characterLeft>().Length;
            int characterRightCount = FindObjectsOfType<CharacterRight>().Length;
            if (characterLeftCount > 0)
            {
                characterLeft = GameObject.FindGameObjectWithTag("CharacterLeft").GetComponent<Transform>();
            }
            else
            {

            }
            if (characterRightCount > 0)
            {
                characterRight = GameObject.FindGameObjectWithTag("CharacterRight").GetComponent<Transform>();
            }
            else
            {

            }
            if (SceneManager.GetActiveScene().buildIndex==12|| SceneManager.GetActiveScene().buildIndex == 14)
            {

            }
            else
            {
                characterRigid = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

                butonJump.gameObject.SetActive(true);
                butonSlow.gameObject.SetActive(true);
                butonFire.gameObject.SetActive(true);
            }
            if (SceneManager.GetActiveScene().buildIndex == 13)
            {
                infinityController = true;
                scriptValue = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfinity>();
                butonFast.gameObject.SetActive(true);
               
            }
            else
            {
                infinityController = false;
            }

           
        }
    }


    void jumpButton()
    {
        if (!infinityController)
        {

            if (jumpBool)
            {
                characterRigid.AddForce(new Vector2(JumpX, 180f));
                soundSource.clip = sounds[0];
                soundSource.Play();
                jumpBool = false;
                jumpBool2 = true;

                StartCoroutine(jumptime());
            }
            else if (jumpBool2)
            {
                characterRigid.AddForce(new Vector2(JumpX, 100f));
                soundSource.clip = sounds[0];
                soundSource.Play();
                jumpBool2 = false;
            }
        }
        else if (infinityController)
        {

            if (jumpBool)
            {
                characterRigid.AddForce(new Vector2(0, 180f));
                scriptValue.speed = false;
                soundSource.clip = sounds[0];
                soundSource.Play();
                jumpBool = false;
                jumpBool2 = true;
               
                StartCoroutine(jumptime());
            }
            else if (jumpBool2)
            {
                scriptValue.speed = false;
                characterRigid.AddForce(new Vector2(0, 100f));
                soundSource.clip = sounds[0];
                soundSource.Play();
                jumpBool2 = false;
            }
        }
       
       
    }
    IEnumerator jumptime()
    {
        yield return new WaitForSecondsRealtime(1);
        jumpBool = true;
    }
    void ShiftButton()
    {
        if (!infinityController)
        {
            if (shiftbool1)
            {
                characterRigid.drag = 5f;
                if (characterBool)
                {
                    characterRight.transform.eulerAngles = new Vector3(0, 0, eulerAngle);
                }
                else if (!characterBool)
                {
                    characterLeft.transform.eulerAngles = new Vector3(0, 0, eulerAngle);
                }
                shiftbool1 = false;
                shiftbool2 = true;
            }
            else if (shiftbool2)
            {
                characterRigid.drag = 0f;
                shiftbool1 = true;
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
        else if (infinityController)
        {
            if (shiftbool1)
            {
                scriptValue.arkaPlanHizi = -1f;

                scriptValue.moveX = -0.1f;
                if (characterBool)
                {
                    characterRight.transform.eulerAngles = new Vector3(0, 0, eulerAngle);
                }
                else if (!characterBool)
                {
                    characterLeft.transform.eulerAngles = new Vector3(0, 0, eulerAngle);
                }
                shiftbool1 = false;
                shiftbool2 = true;
            }
            else if (shiftbool2)
            {
                scriptValue.arkaPlanHizi = -1.5f;

                scriptValue.moveX = 0.0005f;
                shiftbool1 = true;
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
       
    }

    void fastButton()
    {
        if (fastBool)
        {
            scriptValue.moveX = 0.1f;
            scriptValue.arkaPlanHizi = -2f;
            if (characterBool)
            {
                characterRight.transform.eulerAngles = new Vector3(0, 0, -eulerAngle);
            }
            else if (!characterBool)
            {
                characterLeft.transform.eulerAngles = new Vector3(0, 0, -eulerAngle);
            }
            fastBool = false;
            fastBool2 = true;
        }
        else if (fastBool2)
        {
            scriptValue.moveX = 0.0005f;
            scriptValue.arkaPlanHizi = -1.5f;
            fastBool = true;
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
    void FireButton()
    {
       
            if (!infinityController)
            {
                GameObject bulletFire = GameObject.FindGameObjectWithTag("BulletPlane");

                GameObject bulletTrans = Instantiate(bullet, bulletFire.transform.position, Quaternion.identity);
                Rigidbody2D bulletrigid = bulletTrans.GetComponent<Rigidbody2D>();
                bulletrigid.velocity = new Vector2(10, 0);
                soundSource.clip = sounds[1];
                soundSource.Play();
                Destroy(bulletTrans, 1);
            }
            else if (infinityController)
            {
                if (timer == 0f)
                {

                    timer = 10f;
                    GameObject bulletFire = GameObject.FindGameObjectWithTag("BulletPlane");
                    GameObject bulletTrans = Instantiate(bullet, bulletFire.transform.position, Quaternion.identity);
                    Rigidbody2D bulletrigid = bulletTrans.GetComponent<Rigidbody2D>();
                    bulletrigid.velocity = new Vector2(10, 0);
                    soundSource.clip = sounds[1];
                    soundSource.Play();
                    Destroy(bulletTrans, 1);
                    StartCoroutine(fireTimeProcess());

                }

            }
        
    }
    IEnumerator fireTimeProcess()
    {
        scriptValue.fireText.text = "Wait 10 seconds";
        yield return new WaitForSecondsRealtime(10);
        timer = 0f;
        scriptValue.fireText.text = "FIRE ON!";
 
    }


}
