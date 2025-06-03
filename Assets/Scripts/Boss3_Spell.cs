using System.Collections;
using UnityEngine;

public class Boss3_Spell : MonoBehaviour
{
    [SerializeField] protected Transform takeDamagePoint;
    [SerializeField] protected Vector2 takeDamagePointSize = new(1f, 1f);
    [SerializeField] protected float takeDamagePointAngle = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject, "spell", 1f);
        }
        else
        {
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject, "spell", 1f);
        }
    }

    private void Attack()
    {
        Collider2D hitPlayer = Physics2D.OverlapBox(takeDamagePoint.position, takeDamagePointSize, takeDamagePointAngle, LayerMask.GetMask("Player"));
        {
            if (hitPlayer)
            {
                Debug.Log("Hit Player!");
                hitPlayer.GetComponent<Player>().TakeDamage(5f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(takeDamagePoint.position, takeDamagePointSize);
    }
}