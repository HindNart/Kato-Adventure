using UnityEngine;

public class EnemyFly : Enemy
{
    protected override void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position + Vector3.up * 0.5f, moveSpeed * Time.deltaTime);
        anim.SetBool("walk", true);
        FlipEnemy();
    }
}