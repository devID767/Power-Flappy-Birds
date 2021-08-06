using UnityEngine;

public class AbilitiesLevel : MonoBehaviour
{
    public static AbilitiesLevel instance;
    public AbilitiesMenu AbilitiesMenu;
    [HideInInspector] public Ability[] Abilities;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(this != instance)
        {
            Destroy(this);
        }

        Abilities = AbilitiesMenu.Abilities;
    }
}
