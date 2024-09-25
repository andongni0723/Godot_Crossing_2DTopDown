using Godot;
using System;

public partial class Bullet : RigidBody2D
{
    [ExportCategory("Components")]
    [Export] public PackedScene BoomParticle;
    
    [ExportCategory("Settings")]
    [Export] public float Speed = 200;
    [Export] public Vector2 Direction = Vector2.Right;
    private Area2D _area;

    public override void _Ready()
    {
        _area = GetNode<Area2D>("Area2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        var localYDirection = GlobalTransform.Y;
        LinearVelocity = localYDirection.Normalized() * Speed;
    }

    private void _on_area_2d_area_entered(Area2D area)
    {
        QueueFree();
    }

    private void _on_area_2d_body_entered(Node2D body)
    {
        var newParticle = (GpuParticles2D)BoomParticle.Instantiate();
        newParticle.GlobalPosition = GlobalPosition;
        GetTree().Root.AddChild(newParticle);
        
        QueueFree();
    }
}
