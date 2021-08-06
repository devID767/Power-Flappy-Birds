using UnityEngine;

public class Shield : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bird bird))
        {
            Power.instance.Shield(bird);
            Destroy(gameObject);
        }
    }
}
