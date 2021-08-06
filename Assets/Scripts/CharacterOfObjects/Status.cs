using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public GameStatusEnum status;

    void Start()
    {
        CheckCurrentStatus();
    }

    private void GameStatusChanged(GameStatusEnum current)
    {
        CheckCurrentStatus();
    }

    public void CheckCurrentStatus()
    {
        foreach (var item in gameObjects)
        {
            if (item != null)
            {
                if (GameStatus.Current == status)
                {
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }
    }

    private void OnEnable()
    {
        GameStatus.Changed += GameStatusChanged;
    }

    private void OnDisable()
    {
        GameStatus.Changed -= GameStatusChanged;
    }


}
