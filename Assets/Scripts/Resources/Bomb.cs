using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bird bird))
        {
            Power.instance.Bomb(bird);
            Destroy(gameObject);
        }
    }
}
