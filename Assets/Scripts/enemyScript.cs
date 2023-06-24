using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed = 0f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (IsFacingRight())
        {
            rigid.velocity = new Vector2(speed, 0f);
        }
        else
        {
            rigid.velocity = new Vector2(-speed, 0f);
        }
    }
    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1f);
    }
}
