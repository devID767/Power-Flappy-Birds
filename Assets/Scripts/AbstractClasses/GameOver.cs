using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public GameObject GameOverMenu;
    public GameObject GameMenu;

    public static event Action Activated = delegate {}; 

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(this);
        }
    }
    
    public void Activate()
    {
        SaveManager.instance.SaveGame();
        GameStatus.Change(GameStatusEnum.GameOver);
        GameOverMenu.SetActive(true);
        GameMenu.SetActive(false);

        DestroyBirdController();
        DestroyMovers();
        DestroySpawners();

        Time.timeScale = 0.5f;
    }

    private void DestroyBirdController()
    {
        var birds = FindObjectsOfType<Bird>();
        foreach (var item in birds)
        {
            Destroy(item);
        }
    }

    private void DestroyMovers()
    {
        var moverObjects = FindObjectsOfType<Mover>();
        foreach (var item in moverObjects)
        {
            if (!item.gameObject.TryGetComponent(out Cloud _))
            {
                Destroy(item);
            }
        }
    }

    private void DestroySpawners()
    {
        var spawnerObjects = FindObjectsOfType<BackGroundSpawner>();
        foreach (var item in spawnerObjects)
        {
            if (!item.gameObjects[0].TryGetComponent(out Cloud _))
            {
                Destroy(item);
            }
        }
    }
}
