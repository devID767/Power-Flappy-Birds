using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Beginner,
    Casual,
    Medium,
    Hard
}

public class Room : MonoBehaviour
{
    public Difficulty difficulty;

    public GameObject[] ObjectsWhichNeedDestroyInStart;

    private void Start()
    {
        foreach (var item in ObjectsWhichNeedDestroyInStart)
        {
            Destroy(item);
        }
    }
}
