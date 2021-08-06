using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _particle;

    public void Add()
    {
        Instantiate(_particle, transform.position, Quaternion.Euler(-90, 0, 0));

        Coins.IncreasePoint();

        Destroy(gameObject);
    }
}
