using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public Button[] levels;
   
    void Start()
    {
        int episodes = PlayerPrefs.GetInt("kacinciLevel");
        for (int i = 0; i < episodes-1; i++)
        {
            levels[i].interactable = true;
        }
    }
    public void level1()
    {
        SceneManager.LoadScene(2);
    }
    public void level2()
    {
        SceneManager.LoadScene(3);
    }
    public void level3()
    {
        SceneManager.LoadScene(4);
    }
    public void level4()
    {
        SceneManager.LoadScene(5);
    }
    public void level5()
    {
        SceneManager.LoadScene(6);
    }
    public void level6()
    {
        SceneManager.LoadScene(7);
    }
    public void level7()
    {
        SceneManager.LoadScene(8);
    }
    public void level8()
    {
        SceneManager.LoadScene(9);
    }
    public void level9()
    {
        SceneManager.LoadScene(10);
    }
    public void level10()
    {
        SceneManager.LoadScene(11);
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
