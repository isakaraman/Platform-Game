using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonProcess : MonoBehaviour
{
    public GameObject controller;
    public GameObject controlOn;
    public GameObject controlOff;
    public GameObject menuButon;
    public GameObject continueButon;
    public void SceneLevelLoad()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        Time.timeScale = 1;
    }
    public void controllersTrue()
    {

        controller.gameObject.SetActive(true);
        controlOn.gameObject.SetActive(true);
        controlOff.gameObject.SetActive(false);

    }
    public void controllerFalse()
    {

        controller.gameObject.SetActive(false);
        controlOn.gameObject.SetActive(false);
        controlOff.gameObject.SetActive(true);

    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        if (currentSceneIndex==11)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void LevelOneStart()
    {
        SceneManager.LoadScene(2);
    }
    public void goLevels()
    {
        SceneManager.LoadScene(12);
    }
 
    public void infiniteLevel()
    {
        SceneManager.LoadScene(13);
    }
    public void options()
    {
        menuButon.gameObject.SetActive(true);
        continueButon.gameObject.SetActive(true);

        Time.timeScale = 0;
    }
    public void continueProcess()
    {
        menuButon.gameObject.SetActive(false);
        continueButon.gameObject.SetActive(false);

        Time.timeScale = 1;
    }
    public void howtoplayscene()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
