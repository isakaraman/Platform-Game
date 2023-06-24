using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicCode : MonoBehaviour
{

    void Start()
    {
        int countGame = FindObjectsOfType<musicCode>().Length;
        if (countGame>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
       
    }

}
