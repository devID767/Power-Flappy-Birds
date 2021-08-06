using UnityEngine;

public class BirdsChanger : MonoBehaviour
{
    public GameObject[] Bird_Items;

    [SerializeField] private Shop shop;

    public static System.Action<Bird> Changed;

    private void Start()
    {
        //shop = FindObjectOfType<Shop>();
        CreateStartedBird(shop.CurrentBird);
    }

    public void CreateStartedBird(TypeOfBird typeOfBird)
    {
        foreach (var bird in Bird_Items)
        {
            if (bird.GetComponent<Bird>().typeOfBird == typeOfBird)
            {
                Instantiate(bird, transform.position, bird.transform.rotation);
            }
        }
    }

    public void SelectBird(TypeOfBird typeOfBird, Bird Current)
    {
        int num = 0;
        while(Bird_Items[num].GetComponent<Bird>().typeOfBird != typeOfBird)
        {
            num++;
        }
        ChangeBird(num, Current);
    }

    private void ChangeBird(int number, Bird Current)
    {
        var CurrentTransform = Current.GetComponent<Transform>();
        var newebird = Instantiate(Bird_Items[number], CurrentTransform.position, CurrentTransform.rotation);
        newebird.GetComponent<Rigidbody2D>().velocity = Current.GetComponent<Rigidbody2D>().velocity;
        Current.Change();

        Changed?.Invoke(newebird.GetComponent<Bird>());
    }
}
