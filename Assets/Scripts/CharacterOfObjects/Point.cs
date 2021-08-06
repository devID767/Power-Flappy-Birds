using UnityEngine;

public class Point : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bird _))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(GameStatus.Current == GameStatusEnum.IsPlaying)
            Score.IncreasePoint();
    }
}
