using Godot;
using System;

public partial class CharacterShoot : Node2D
{ 
    //Header component use attribute
    
    [ExportCategory("Components")]
    [Export] public PackedScene Bullet;
    [Export] public PackedScene FireParticle;
    [Export] public Node2D BulletSpawnPoint;
    
    
    [ExportCategory("Settings")]
    [Export] public float BulletSpeed = 500;

    // public override void _Ready()
    // {
    // }

    public void ShootAction()
    {
        // CameraController.ApplyShake(0.1f, 0.1f);
        CameraController.Instance.ApplyShake(5, 40);
        
        var newBullet = (RigidBody2D)Bullet.Instantiate();
        newBullet.GlobalPosition = BulletSpawnPoint.GlobalPosition;
        newBullet.GlobalRotation = BulletSpawnPoint.GlobalRotation + Mathf.Pi;
        newBullet.GetNode<Bullet>(".").Speed = BulletSpeed;
        GetTree().Root.AddChild(newBullet);

        var newBoomParticle = (GpuParticles2D)FireParticle.Instantiate();
        newBoomParticle.GlobalPosition = BulletSpawnPoint.GlobalPosition;
        GetTree().Root.AddChild(newBoomParticle);
    }
}