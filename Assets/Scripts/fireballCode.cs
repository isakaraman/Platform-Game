using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCode : MonoBehaviour
{
    public Sprite[] fires;
    bool moving = true;

    float waitingTime = 0;
    SpriteRenderer spriterenderer;
    int waiter = 0;
    public float animaTime = 0;
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        animatioN();
    }
    void animatioN()
    {
        if (moving)
        {
            waitingTime += Time.deltaTime;
            if (waitingTime > animaTime)
            {
                spriterenderer.sprite = fires[waiter++];
                if (waiter == fires.Length)
                {
                    waiter = 0;
                }
                waitingTime = 0;
            }
        }
    }
}