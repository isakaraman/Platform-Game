using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class idprocess : MonoBehaviour
{
    public InputField scoreToSend;

    public Text wrongText;
    public Text welcomeText;

    JSONNode jsonData;

    readonly string URL = "https://tunayyucel.com/oyun/kisilerjson.php";

    string ozelid;
    string username;


    void Start()
    {
        StartCoroutine(GetRequest(URL));

    }
    public void onButtonSendScore()
    {

        for (int i = 0; i < jsonData.Count; i++)
        {
            ozelid = jsonData[i]["ozelid"];

            if (scoreToSend.text==ozelid)
            {
                username=jsonData[i]["username"];

                PlayerPrefs.SetString("username", username);
                    
                scoreToSend.gameObject.SetActive(false);
                
                
                Invoke("uploadScene", 3);
                welcomeText.gameObject.SetActive(true);
                welcomeText.text = "WELCOME " + username;
                break;
            }
            else
            {
                wrongText.gameObject.SetActive(true);
                
            }
        }
    }

    void uploadScene()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);


        }

        if (uwr.isDone)
        {
            jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(uwr.downloadHandler.data));
        }
    }

}
