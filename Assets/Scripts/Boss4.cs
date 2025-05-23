using UnityEngine;

public class Boss4 : Enemy
{
    [SerializeField] private GameObject bossHpBar;
    [SerializeField] private Vector2 takeDamagePointSize = new Vector2(1f, 1f);
    [SerializeField] private float takeDamagePointAngle = 0f;

    protected override void Movement()
    {
        base.Movement();
        AudioManager.Instance.PlayBossWalkSound();
    }

    protected override void Attack()
    {
        Collider2D hitPlayer = Physics2D.OverlapBox(attackPoint.position, takeDamagePointSize, takeDamagePointAngle, LayerMask.GetMask("Player"));
        if (hitPlayer)
        {
            Debug.Log("Hit Player!");
            player.TakeDamage(attackDamage);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, takeDamagePointSize);
    }

    protected override void Die()
    {
        base.Die();
        AudioManager.Instance.PlayBossDieSound();
        AudioManager.Instance.PlayTempleMusic();
        bossHpBar.SetActive(false);
        StartCoroutine(GameManager.Instance.AnimateWin());
    }
}