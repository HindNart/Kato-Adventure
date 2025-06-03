using System.Collections;
using UnityEngine;

public class EnemyGroundRangeAtk : Enemy
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 5f;
    [SerializeField] private ObjectPool bombPool;

    protected override void Movement()
    {
        base.Movement();
        if (inRange)
        {
            if (Vector2.Distance(attackPoint.position, player.transform.position) <= retrieveDistance)
            {
                FlipEnemy();
                if (!IsInvoking(nameof(Fire)))
                {
                    Invoke(nameof(Fire), fireInterval);
                }
            }
        }
    }

    private void Fire()
    {
        GameObject bomb = bombPool.GetObject("bomb");
        bomb.transform.position = firePoint.position;
        bomb.transform.rotation = firePoint.rotation;

        // float direction = transform.localScale.x > 0 ? 1f : -1f;
        // bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * 8f, 0);

        Vector2 direction = (player.transform.position - transform.position).normalized;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // bomb.transform.rotation = Quaternion.Euler(0, 0, angle);
        bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * 5f, direction.y);
    }
}