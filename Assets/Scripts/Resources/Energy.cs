using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Image Image;
    public float[] RecoverTimeOfLevels;
    public float RecoverTime
    {
        get
        {
            return RecoverTimeOfLevels[AbilitiesLevel.instance.Abilities[(int)EAbility.Energy].Level];
        }
    }

    private bool CanUse;

    void Update()
    {
        if(Image.fillAmount < 1)
        {
            Image.fillAmount += 1 / RecoverTime * Time.deltaTime;

            CanUse = false;
        }
        else
        {
            Image.fillAmount = 1;
            CanUse = true;
        }
    }

    public bool Use()
    {
        if (CanUse)
            Image.fillAmount = 0;
        return CanUse;
    }
}
