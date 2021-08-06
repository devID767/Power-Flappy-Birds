using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    public float force;

    public Rigidbody2D[] rigidbody;

    void Start()
    {
        foreach (var rg in rigidbody)
        {
            rg.AddForce(new Vector2(Random.Range(-1, 1) * force, Random.Range(-1, 1) * force));
        }
    }
}
