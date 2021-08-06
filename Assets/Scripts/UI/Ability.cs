using UnityEngine;
using UnityEngine.UI;

public enum EAbility
{
    Energy,
    Icons,
    Shield,
    Rocket,
    Deal,
    Bomb
}

public class Ability : MonoBehaviour
{
    public EAbility EAbility;
    [HideInInspector] public int Level;
    public int[] Cost;

    public Button UpgradeButton;
    public Text LevelText;
    public Text CostText;
    public GameObject ClosePanel;
}