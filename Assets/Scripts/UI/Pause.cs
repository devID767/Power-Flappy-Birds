using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button button;

    public GameObject PauseMenu;

    public void OnPauseButton()
    {
        GameStatus.Change(GameStatusEnum.Pause);
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueButon()
    {
        Time.timeScale = 1;
        GameStatus.Change(GameStatusEnum.IsPlaying);
        PauseMenu.SetActive(false);
    }

    private void OnApplicationPause(bool pause)
    {
        OnPauseButton();
    }
    private void OnApplicationQuit()
    {
        ContinueButon();
    }
}
