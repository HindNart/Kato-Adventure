using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(100f);
        }
        else if (other.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}
