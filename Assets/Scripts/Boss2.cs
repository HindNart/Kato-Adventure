using System.Collections;
using UnityEngine;

public class Boss2 : Enemy
{
    [SerializeField] private GameObject bossHpBar;
    [SerializeField] private GameObject caveWall;

    protected override void Movement()
    {
        base.Movement();
        AudioManager.Instance.PlayBossWalkSound();
    }

    protected override void Die()
    {
        anim.SetTrigger("death");
        AudioManager.Instance.PlayBossDieSound();
        AudioManager.Instance.PlayCaveMusic();

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        Destroy(gameObject, 1f);

        bossHpBar.SetActive(false);
        caveWall.SetActive(false);

        if (dropItem != null && GameManager.Instance.collectedGems == 1 && !dropedItem)
        {
            dropedItem = true;
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
    }
}