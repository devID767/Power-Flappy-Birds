using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] GameObjects;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        var randomObject = GameObjects[Random.Range(0, GameObjects.Length)];
        Instantiate(randomObject, transform);
    }
}
