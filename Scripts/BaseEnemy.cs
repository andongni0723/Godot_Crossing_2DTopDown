using Godot;
using System;

public partial class BaseEnemy : RigidBody2D, IAttack
{
    [Export] public PackedScene DeathParticle;
    public void Damage(int damage)
    {
        // var newParticle = (GpuParticles2D)DeathParticle.Instantiate();
        // newParticle.GlobalPosition = GlobalPosition;
        // GetTree().Root.AddChild(newParticle);
        QueueFree();
    }
}
