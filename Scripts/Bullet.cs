using Godot;
using System;

public partial class Bullet : RigidBody2D
{
    [ExportCategory("Components")]
    [Export] public PackedScene BoomParticle;
    
    [ExportCategory("Settings")]
    [Export] public int damage = 1;
    [Export] public float Speed = 200;
    // [Export] public Vector2 Direction = Vector2.Right;
    
    // private Area2D _area;
    private float _collisionCount = 0;
    private Vector2 _point;

    // private Vector2 _localYDirection;
    private Vector2 _contactLocalNormal;
    

    public override void _Ready()
    {
        // _area = GetNode<Area2D>("Area2D");
        _point = Position;
        
        // _localYDirection = GlobalTransform.Y;
        _velocity = GlobalTransform.Y.Normalized() * Speed;

    }

    private Vector2 _velocity;
    public override void _PhysicsProcess(double delta)
    {
        // 僅在不處於碰撞狀態時移動
        var collisionDetection = MoveAndCollide(_velocity * (float)delta);
        
        if (collisionDetection == null) return;
        
        // if colliding 
        _OnCollision(collisionDetection);
        if(collisionDetection.GetCollider() as Node2D is IAttack hit)
        {
            hit.Damage(damage);
        }
    }
    
    private void _OnCollision(KinematicCollision2D collision)
    {
        _collisionCount++;
        
        CameraController.Instance.ApplyShake(15, 40);
        
        // hit object can attack
        if(collision.GetCollider() as Node2D is IAttack hit)
        {
            hit.Damage(damage);
            var newParticle = (GpuParticles2D)BoomParticle.Instantiate();
            newParticle.GlobalPosition = GlobalPosition;
            GetTree().Root.AddChild(newParticle);
            
            QueueFree();
        }
        // has collision twice
        else if (_collisionCount >= 2)
        {
            var newParticle = (GpuParticles2D)BoomParticle.Instantiate();
            newParticle.GlobalPosition = GlobalPosition;
            GetTree().Root.AddChild(newParticle);
            
            QueueFree();
        }
        // rebound
        else
        {
            _velocity = _velocity.Bounce(collision.GetNormal());
        }
    } 
}
