using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxinstantieteCode : MonoBehaviour
{
    public Transform plane;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            collision.transform.Translate(plane.transform.position);  
        }
    }

}
