using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public float spinForce = 0f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, spinForce));
    }
}
