using Godot;
using System;

public partial class MissileEnemy : BasicEnemy
{
    public void StopMoveAndShoot()
    {
        Velocity = Vector2.Zero;
        // LookAt(Vector2.Zero);
        EnemyAttack.Attack();
    }

}
