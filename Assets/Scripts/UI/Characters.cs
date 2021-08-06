using UnityEngine;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{
    [SerializeField] private float fillAmountCoolDownPowerFirstLvl;
    [SerializeField] private float fillAmountCoolDownPowerSecondLvl;
    [SerializeField] private float fillAmountCoolDownPowerThirdrLvL;

    private void OnEnable()
    {
        LevelsOfBirds.Changed += LevelOfBirdChanged;
    }

    private void LevelOfBirdChanged(Bird_Item obj)
    {
        ShowPowerCoolDown(obj);
    }

    public Image PowerCoolDown;
    public void ShowPowerCoolDown(Bird_Item bird)
    {
        switch (bird.Level)
        {
            case 0:
                PowerCoolDown.fillAmount = fillAmountCoolDownPowerFirstLvl;
                break;
            case 1:
                PowerCoolDown.fillAmount = fillAmountCoolDownPowerSecondLvl;
                break;
            case 2:
                PowerCoolDown.fillAmount = fillAmountCoolDownPowerThirdrLvL;
                break;

        }
    }

    private void OnDisable()
    {
        LevelsOfBirds.Changed -= LevelOfBirdChanged;
    }
}
