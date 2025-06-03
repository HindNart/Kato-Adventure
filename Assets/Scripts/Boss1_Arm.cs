using UnityEngine;

public class Boss1_Arm : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(5f);
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject, "arm");
        }
        else if (other.gameObject.layer == 6)
        {
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject, "arm");
        }
    }
}
