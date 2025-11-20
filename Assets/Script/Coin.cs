using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) // Gunakan OnTriggerEnter jika 3D
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().AddCoin();

            Destroy(gameObject);
        }
    }
}