using Godot;
using System;
using System.Collections.Generic;

public partial class ExplosiveBullet : Bullet
{
    [Export] public PackedScene Bullet;
    [Export] public Timer BoomTimer;
    [Export] public Node2D[] NewBulletPoints;
    
    public override void _Ready()
    {
        base._Ready();
        BoomTimer.Start();
    }

    private void _on_boom_timer_timeout()
    {
        CameraController.Instance.ApplyShake(5, 40);
        AudioManager.Instance.PlaySound(AudioManager.Instance.FireSound);
        
        foreach (var point in NewBulletPoints)
        {
            var newBullet = (RigidBody2D)Bullet.Instantiate();
            newBullet.GlobalPosition = point.GlobalPosition;
            newBullet.GlobalRotation = point.GlobalRotation + Mathf.Pi;
            newBullet.GetNode<Bullet>(".").Speed = Speed;
            // GetTree().Root.AddChild(newBullet);
            GetTree().Root.CallDeferred("add_child", newBullet);
            var newBoomParticle = (GpuParticles2D)BoomParticle.Instantiate();
            newBoomParticle.GlobalPosition = point.GlobalPosition;
            GetTree().Root.CallDeferred("add_child", newBoomParticle);
        }
    }

    public override void _ExitTree()
    {
        _on_boom_timer_timeout();
    }
}
