using UnityEngine;

public class EnemyNonAtk : Enemy
{
    protected override void Update()
    {
        base.Update();
        Attack();
    }
}