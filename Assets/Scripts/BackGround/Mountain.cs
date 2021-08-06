using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    public float x_start;
    public float x_end;
    void Start()
    {
        var x = Random.Range(x_start, x_end);
        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
    }
}
