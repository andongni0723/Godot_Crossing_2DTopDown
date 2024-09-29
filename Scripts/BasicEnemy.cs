using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D, IAttack
{
    [Export] public PackedScene DeathParticle;
	
    private Node2D _target;

    public override void _Ready()
    {
        _target = GetNode<Node2D>("/root/GameWorld/Tower");
        GD.Print(_target.GlobalPosition);
    }

    public void Damage(int damage)
    {
        QueueFree();
    }
}
