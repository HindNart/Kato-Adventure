using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float damageRange = 5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            anim.SetTrigger("on_ground");
            Invoke(nameof(BombExplosion), 1f);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            BombExplosion();
        }
    }

    private void BombExplosion()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        AudioManager.Instance.PlayBombExplosionSound();
        anim.SetTrigger("explosion");
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, damageRange, LayerMask.GetMask("Player"));
        if (hitPlayer)
        {
            hitPlayer.GetComponent<Player>().TakeDamage(5f);
        }
        // Destroy(gameObject, 1f);
        FindObjectOfType<ObjectPool>().ReturnObject(gameObject, "bomb", 1f);
    }
}
