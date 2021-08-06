using UnityEngine;

public class Deal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bird bird))
        {
            Power.instance.Deal(bird);
            Destroy(gameObject);
        }
    }
}
