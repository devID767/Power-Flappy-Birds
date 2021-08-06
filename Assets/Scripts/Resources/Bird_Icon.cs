using UnityEngine;
using UnityEngine.UI;

public class Bird_Icon : MonoBehaviour
{
    public Button Button;

    public TypeOfBird TypeOfBird;

    public void OnChangeBirdButton()
    {
        var birds = FindObjectsOfType<Bird>();

        foreach (var bird in birds)
        {
            FindObjectOfType<BirdsChanger>().SelectBird(TypeOfBird, bird);
        }

        Icons.birds.RemoveThis(gameObject);
    }
}
