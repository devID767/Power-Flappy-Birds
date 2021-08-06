using UnityEngine;
using UnityEngine.UI;

public enum ESetting
{
    SlideControl,
    Music
}

public class SetSettings : MonoBehaviour
{
    public ESetting setting;

    private void Start()
    {
        switch (setting)
        {
            case ESetting.SlideControl:
                SetSavedCharacter(Settings.SlideControl);
                break;
            case ESetting.Music:
                SetSavedCharacter(!Settings.MusicOn);
                break;
        }
    }

    public void SetSavedCharacter(bool IsOn)
    {
        GetComponent<Toggle>().isOn = IsOn;
    }

    public void SetSlideControl(bool status)
    {
        Settings.ChangeSlideControl(status);
    }

    public void SetMusicStatus(bool status)
    {
        Settings.ChangeMusicStatus(!status);
    }
}
