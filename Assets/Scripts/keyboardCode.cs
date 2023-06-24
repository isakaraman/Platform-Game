using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyboardCode : MonoBehaviour
{

    public InputField pinText;
    public void numberZero()
    {
        pinText.text += "0";
    }
    public void numberOne()
    {
        pinText.text += "1";
    }
    public void numberTwo()
    {
        pinText.text += "2";
    }
    public void numberThree()
    {
        pinText.text += "3";
    }
    public void numberFour()
    {
        pinText.text += "4";
    }
    public void numberFive()
    {
        pinText.text += "5";
    }
    public void numberSix()
    {
        pinText.text += "6";
    }
    public void numberSeven()
    {
        pinText.text += "7";
    }
    public void numberEight()
    {
        pinText.text += "8";
    }
    public void numberNine()
    {
        pinText.text += "9";
    }
    public void delete()
    {
        pinText.text = "";
    }
}
