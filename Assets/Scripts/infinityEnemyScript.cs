using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infinityEnemyScript : MonoBehaviour
{

    Rigidbody2D rigid;

    public float addforce = 0f;

    bool jump = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!jump)
        {
            rigid.AddForce(new Vector2(0, 0));
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "plane")
        {
            jump = true;
            if (jump)
            {
                rigid.AddForce(new Vector2(0, addforce));
                jump = false;
            }   
        }


    }
}
