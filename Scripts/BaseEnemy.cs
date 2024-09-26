using Godot;
using System;

public partial class BaseEnemy : RigidBody2D, IAttack
{
    public void Damage(int damage)
    {
        QueueFree();
    }
}
