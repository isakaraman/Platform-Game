using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateScript : MonoBehaviour
{
    public GameObject[] monsters;
    float ttime=0f;
    float timer = 0f;
    float coinTimer = 0f;
    void Update()
    {

        instantiater();
    }

    void instantiater()
    {

            ttime += Time.deltaTime;
            if (ttime>3)
            {
                ttime = 0;
                int b = Random.Range(1, 5);
                switch (b)
                {
                    case 1:
                        GameObject obj1 = Instantiate(monsters[0], new Vector3(12f, -3.57f), Quaternion.identity);
                        Rigidbody2D physh1 = obj1.GetComponent<Rigidbody2D>();
                        physh1.velocity = new Vector2(-2.5f, 0);
                        
                        break;
                    case 2:
                        GameObject obj2 = Instantiate(monsters[1], new Vector3(12f, -3.57f), Quaternion.identity);
                        Rigidbody2D physh2 = obj2.GetComponent<Rigidbody2D>();
                        physh2.velocity = new Vector2(-2.5f, 0);
                        break;
                    case 3:
                        GameObject obj3 = Instantiate(monsters[2], new Vector3(12f, -3.57f), Quaternion.identity);
                        Rigidbody2D physh3 = obj3.GetComponent<Rigidbody2D>();
                        physh3.velocity = new Vector2(-2.5f, 0);
                        break;
                    case 4:
                        GameObject obj4 = Instantiate(monsters[3], new Vector3(12f, -3.57f), Quaternion.identity);
                        Rigidbody2D physh4 = obj4.GetComponent<Rigidbody2D>();
                        physh4.velocity = new Vector2(-2.5f, 0);
                        break;
                }
            }
        
        timer += Time.deltaTime;
        if (timer>3.5f)
        {
            timer = 0f;
            GameObject obj1 = Instantiate(monsters[4], new Vector3(12f, Random.Range(-2.50f,-3.57f)), Quaternion.identity);
            Rigidbody2D physh1 = obj1.AddComponent<Rigidbody2D>();
            physh1.gravityScale = 0f;
            physh1.velocity = new Vector2(-2.5f, 0);
        }

        coinTimer += Time.deltaTime;
        if (coinTimer>2)
        {
            coinTimer = 0f;
            GameObject obj1 = Instantiate(monsters[5], new Vector3(12f, -3.75f), Quaternion.identity);
            Rigidbody2D physh1 = obj1.AddComponent<Rigidbody2D>();
            physh1.gravityScale = 0f;
            physh1.velocity = new Vector2(-1.7f, 0);
        }
    }
}
