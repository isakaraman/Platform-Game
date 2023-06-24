using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointScript : MonoBehaviour
{
    public Text pointText;
    int totalScore;  // toplam skor verisi
    int newScore;    // girilen yeni skor verisi
    void Start() 
    {
        
        newScore = 0;
        newScore= PlayerPrefs.GetInt("totalPoints");

        if (PlayerPrefs.HasKey("totalScoreKey"))  //totalScoreKey anahtarıyla kaydedilmiş bir veri var mı ?
        {
            totalScore = PlayerPrefs.GetInt("totalScoreKey"); // totalScoreKey anahtarıyla kaydedilmiş veriyi getir

        }
        else
        {

        }

        totalScore += newScore;
        PlayerPrefs.SetInt("totalScoreKey", totalScore);

        pointText.text = "Total Points: " + PlayerPrefs.GetInt("totalScoreKey");
        PlayerPrefs.SetInt("totalPoints",0);
    }

    
}
