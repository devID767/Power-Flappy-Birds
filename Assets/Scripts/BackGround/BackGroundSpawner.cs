using UnityEngine;

public class BackGroundSpawner : MonoBehaviour
{
    public bool SpawnAll = false;
    public GameObject[] gameObjects;

    public float y_start;
    public float y_end;

    public bool SpawnIfObjectAttachedCollider = false;
    public string Tag;

    public float time_start;
    public float time_end;
    public float time_fell;

    private void OnEnable()
    {
        Score.Changed += PointsCollected;
    }

    void Start()
    {
        if (!SpawnIfObjectAttachedCollider)
        {
            Create();
        }
    }

    private void PointsCollected(int score)
    {
        if(time_start - time_fell > 0)
            time_start -= time_fell;
        if(time_end - time_fell > time_start)
            time_end -= time_fell;
    }

    private void Create()
    {
        if (SpawnAll)
        {

            foreach (var objects in gameObjects)
            {
                var y = objects.transform.position.y + Random.Range(y_start, y_end);
                Instantiate(objects, new Vector3(objects.transform.position.x, y, 0), Quaternion.identity);

                if (!SpawnIfObjectAttachedCollider)
                    Invoke("Create", Random.Range(time_start, time_end));
            }
        }
        else
        {
            var objects = GetRandomObject();
            var y = objects.transform.position.y + Random.Range(y_start, y_end);
            Instantiate(objects, new Vector3(objects.transform.position.x, y, 0), Quaternion.identity);

            if (!SpawnIfObjectAttachedCollider)
                Invoke("Create", Random.Range(time_start, time_end));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag))
        {
            Create();
        }
    }

    private GameObject GetRandomObject()
    {
        var gameObject = gameObjects[Random.Range(0, gameObjects.Length)];

        return gameObject;
    }

    public void Stop()
    {
        time_start = 0;
        time_end = 0;
    }

    private void OnDisable()
    {
        Score.Changed -= PointsCollected;
    }
}
