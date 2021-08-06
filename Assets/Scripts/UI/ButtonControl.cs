using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public void OnPowerButton()
    {
        var birds = FindObjectsOfType<Bird>();
        foreach(var bird in birds)
        {
            bird.PowerActivate(bird);
        }
    }

    public void OnEnergyButton()
    {
        var birds = FindObjectsOfType<Bird>();
        foreach (var bird in birds)
        {
            if(bird.CurrentPower != PowerEnum.Rocket)
                bird.UseEnergy();
        }
    }
}
