using UnityEngine;

public class Boss3 : Enemy
{
    [SerializeField] private GameObject bossHpBar;
    [SerializeField] private float castInterval = 5f;
    [SerializeField] private ObjectPool spellPool;
    [SerializeField] private GameObject exitDoor;

    protected override void Movement()
    {
        base.Movement();
        AudioManager.Instance.PlayBossWalkSound();

        if (inRange)
        {
            if (Vector2.Distance(attackPoint.position, player.transform.position) > 4f)
            {
                anim.SetTrigger("cast_spell");

                if (!IsInvoking(nameof(Cast)))
                {
                    Invoke(nameof(Cast), castInterval);
                }
            }
        }
    }

    private void Cast()
    {
        GameObject spell = spellPool.GetObject("spell");
        spell.transform.position = player.transform.position + new Vector3(0, 1.5f, 0);
        spell.transform.rotation = player.transform.rotation;
    }

    protected override void Die()
    {
        anim.SetTrigger("death");
        AudioManager.Instance.PlayBossDieSound();
        AudioManager.Instance.PlaySwampMusic();

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        Destroy(gameObject, 1f);

        bossHpBar.SetActive(false);

        if (exitDoor != null)
        {
            exitDoor.GetComponent<SpriteRenderer>().enabled = true;
            exitDoor.GetComponent<Collider2D>().enabled = true;
        }

        if (dropItem != null && GameManager.Instance.collectedGems == 2 && !dropedItem)
        {
            dropedItem = true;
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
    }
}
