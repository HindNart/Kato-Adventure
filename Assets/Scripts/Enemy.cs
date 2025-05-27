using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    protected Player player;
    protected Animator anim;
    protected bool isFacingRight = true;
    protected bool inRange = false;
    protected bool dropedItem = false;
    [SerializeField] protected GameObject dropItem;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 0.5f;
    // [SerializeField] protected float chaseSpeed = 1.5f;
    // [SerializeField] protected Transform groundCheck;
    // [SerializeField] protected LayerMask groundLayer;
    // [SerializeField] protected float groundCheckDistance = 1f;

    [Header("Attack")]
    [SerializeField] protected float attackRate = 1f;
    [SerializeField] protected float attackRange = 5f;
    [SerializeField] protected float retrieveDistance = 0.5f;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRadius = 0.3f;
    [SerializeField] protected float attackDamage = 1f;

    [Header("Hp")]
    protected float currentHp;
    [SerializeField] protected float maxHp = 10f;
    [SerializeField] private Image hpBar;

    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        currentHp = maxHp;
        UpdateHpBar();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            inRange = true;
        }
        else inRange = false;

        if (inRange)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > retrieveDistance)
            {
                MoveToPlayer();
            }
            else
            {
                if (anim != null && anim.HasState(0, Animator.StringToHash("attack")))
                {
                    if (!IsInvoking(nameof(TriggerAttack)))
                    {
                        Invoke(nameof(TriggerAttack), attackRate);
                    }
                }
            }
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    private void TriggerAttack()
    {
        anim.SetTrigger("attack");
        AudioManager.Instance.PlayBossAttackSound();

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    protected virtual void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        anim.SetBool("walk", true);
        FlipEnemy();
    }

    protected void FlipEnemy()
    {
        if (isFacingRight && player.transform.position.x < transform.position.x ||
            !isFacingRight && player.transform.position.x > transform.position.x)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        anim.SetBool("hit", true);
        AudioManager.Instance.PlayBossHurtSound();

        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp == 0)
        {
            Die();
        }
    }

    protected void EndHit()
    {
        anim.SetBool("hit", false);
    }

    protected virtual void Die()
    {
        anim.SetTrigger("death");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        Destroy(gameObject, 1f);
        if (dropItem != null && !dropedItem)
        {
            dropedItem = true;
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
    }

    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    protected virtual void Attack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRadius, LayerMask.GetMask("Player"));
        if (hitPlayer)
        {
            Debug.Log("Hit Player!");
            player.TakeDamage(attackDamage);
        }
    }
}
