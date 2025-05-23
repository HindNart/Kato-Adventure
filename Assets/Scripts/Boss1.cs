using System.Collections;
using UnityEngine;

public class Boss1 : Enemy
{
    [SerializeField] private GameObject bossHpBar;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 5f;
    [SerializeField] private ObjectPool armPool;
    [SerializeField] private GameObject stone;
    [SerializeField] private GameObject lever;

    protected override void Movement()
    {
        base.Movement();
        AudioManager.Instance.PlayBossWalkSound();

        if (inRange)
        {
            if (Vector2.Distance(attackPoint.position, player.transform.position) > 4f)
            {
                anim.SetTrigger("range_attack");

                if (!IsInvoking(nameof(Fire)))
                {
                    Invoke(nameof(Fire), fireInterval);
                }
            }
        }
    }

    private void Fire()
    {
        GameObject arm = armPool.GetObject();
        arm.transform.position = firePoint.position;
        arm.transform.rotation = firePoint.rotation;

        // float directionScale = transform.localScale.x > 0 ? 1f : -1f;
        // arm.transform.localScale = new Vector3(0.6f * directionScale, 0.6f, 0.6f);

        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arm.transform.rotation = Quaternion.Euler(0, 0, angle);
        arm.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * 5f, direction.y - 0.8f);
        Debug.Log("Arm Direction: " + direction);
    }

    protected override void Die()
    {
        anim.SetTrigger("death");
        AudioManager.Instance.PlayBossDieSound();
        AudioManager.Instance.PlayForestMusic();

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        Destroy(gameObject, 1f);

        bossHpBar.SetActive(false);
        stone.SetActive(false);
        lever.SetActive(true);

        if (dropItem != null && GameManager.Instance.collectedGems == 0 && !dropedItem)
        {
            dropedItem = true;
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
    }
}