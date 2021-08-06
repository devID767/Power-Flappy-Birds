using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMenuButton : MonoBehaviour
{
    public Button button;

    public void OnToMenuButton()
    {
        Time.timeScale = 1;
        GameStatus.Change(GameStatusEnum.NotStarted);

        Coins.ClearCoinsByRound();
        Score.ClearByRound();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
