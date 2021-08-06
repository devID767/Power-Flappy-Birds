using UnityEngine;

public class Bird_Resource : MonoBehaviour
{
    public TypeOfBird typeOfBird;

    public void Add()
    {
        Icons.birds.Add(x => x.GetComponent<Bird_Icon>().TypeOfBird == typeOfBird);
        Destroy(gameObject);
    }
}
