using UnityEngine;
public class Bird_Item : MonoBehaviour
{
    public TypeOfBird TypeOfBird;

    public int Cost;

    public bool Bought;

    public int[] Upgrade = new int[3];

    [HideInInspector] public int Level;

    public GameObject[] FeatherParticles;

    public void OnBirdItemClicked()
    {
        Instantiate(FeatherParticles[Random.Range(0, FeatherParticles.Length)], transform.position, Quaternion.identity);
    }
}
