using UnityEngine;
using UnityEngine.UI;

public class AbilitiesMenu : MonoBehaviour
{
    public Ability[] Abilities;

    private void OnEnable()
    {
        CheckStateOfButtonsUpgradeAndSetTextOfCost();
    }

    private void CheckStateOfButtonsUpgradeAndSetTextOfCost()
    {
        foreach (var ability in Abilities)
        {
            var lvl = ability.Level;
            ability.LevelText.text = (lvl + 1).ToString();

            if (ability.Level >= ability.Cost.Length)
            {
                ability.ClosePanel.SetActive(true);
                ability.CostText.gameObject.SetActive(false);
                ability.UpgradeButton.GetComponentInChildren<Text>().text = "Max";
            }
            else
            {
                ability.CostText.text = ability.Cost[ability.Level].ToString();
                if (ability.Cost[ability.Level] > Coins.count)
                    ability.ClosePanel.SetActive(true);
                else
                    ability.ClosePanel.SetActive(false);
            }

        }
    }

    public void OnUpgradeButtonClicked(int num)
    {
        Coins.AddPoints(-Abilities[num].Cost[Abilities[num].Level]);
        Abilities[num].Level++;
        CheckStateOfButtonsUpgradeAndSetTextOfCost();
    }
}
