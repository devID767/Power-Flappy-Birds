using UnityEngine;

public class ButtonControlChecker : MonoBehaviour
{
    public GameObject PowerButton;
    public GameObject EnergyButton;

    void Start()
    {
        ButtonStatus(!Settings.SlideControl);

        Settings.ChangedSlideControl += Settings_ChangedSlideControl;
    }

    private void ButtonStatus(bool status)
    {
        PowerButton.SetActive(status);
        EnergyButton.SetActive(status);
    }

    private void Settings_ChangedSlideControl(bool status)
    {
        ButtonStatus(!status);
    }

    private void OnDisable()
    {
        Settings.ChangedSlideControl -= Settings_ChangedSlideControl;
    }
}
