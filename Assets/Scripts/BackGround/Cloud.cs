using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Mover mover;

    public float speed_min;
    public float speed_max;

    [Range(1, 3)]
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        var ranScale = Random.Range(1.0f, scale);
        transform.localScale = new Vector3(transform.localScale.x * ranScale, transform.localScale.y * ranScale, transform.localScale.z);

        mover = GetComponent<Mover>();
        mover.speed += Random.Range(speed_min, speed_max);
        mover.speed /= ranScale;
    }
}
