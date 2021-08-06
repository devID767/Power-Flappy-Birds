using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject[] ActivateObjects;
    public GameObject[] DeactivateObjects;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Activate();
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Activate();
        }
    }

    public void Activate()
    {
        FindObjectOfType<Bird>().GameStarted();
        GameStatus.Change(GameStatusEnum.IsPlaying);
        gameObject.SetActive(false);
        foreach (var obj in ActivateObjects)
        {
            obj.SetActive(true);
        }
        foreach (var obj in DeactivateObjects)
        {
            obj.SetActive(false);
        }
    }
}
