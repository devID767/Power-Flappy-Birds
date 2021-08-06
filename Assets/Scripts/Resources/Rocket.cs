using UnityEngine;

public class Rocket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bird bird))
        {
            Power.instance.Rocket(bird);
            Destroy(gameObject);
        }
    }
}
