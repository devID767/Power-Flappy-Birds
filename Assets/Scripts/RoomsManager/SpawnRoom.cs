using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public bool SpawnByScore = true;

    public int Lvl_beginner_end;

    public int Lvl_casual_start;
    public int Lvl_casual_end;

    public int Lvl_medium_start;
    public int Lvl_medium_end;

    public int Lvl_hard_start;

    public GameObject[] rooms;
    public string Tag;

    [SerializeField] public float X;

   private void Start()
    {
        CreateRoom();
    }

    public void CreateRoom()
    {
        if (SpawnByScore)
        {
            var difficulties = new List<Difficulty>();
            if (Score.count <= Lvl_beginner_end)
            {
                difficulties.Add(Difficulty.Beginner);
            }
            if (Lvl_casual_start <= Score.count && Score.count <= Lvl_casual_end)
            {
                difficulties.Add(Difficulty.Casual);
            }
            if (Lvl_medium_start <= Score.count && Score.count <= Lvl_medium_end)
            {
                difficulties.Add(Difficulty.Medium);
            }
            if (Lvl_hard_start <= Score.count)
            {
                difficulties.Add(Difficulty.Hard);
            }
            Instantiate(RandomRoom(difficulties), new Vector3(X, 0, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(rooms[Random.Range(0, rooms.Length)], new Vector3(X, 0, 0), Quaternion.identity);

        }
    }

    private GameObject RandomRoom(List<Difficulty> difficulties)
    {
        var room = rooms[Random.Range(0, rooms.Length)];
        if(difficulties.Contains(room.GetComponent<Room>().difficulty))
        {
            return room;
        }
        else
        {
            return RandomRoom(difficulties);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag))
        {
            CreateRoom();
        }
    }
}
