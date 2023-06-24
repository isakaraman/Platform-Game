using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Monster")
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag=="Point")
        {
            Destroy(collision.gameObject);
        }
    }
}
