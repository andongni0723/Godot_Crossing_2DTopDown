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
        _point = Position;
        _velocity = GlobalTransform.Y.Normalized() * Speed;

    }

    private Vector2 _velocity;
    public override void _PhysicsProcess(double delta)
    {
        var collisionDetection = MoveAndCollide(_velocity * (float)delta);
        
        if (collisionDetection == null) return;
        
        // if colliding 
        _OnCollision(collisionDetection);
        // if(collisionDetection.GetCollider() as Node2D is IAttack hit)
        // {
        //     hit.Damage(damage);
        // }
    }
    
    private void _OnCollision(KinematicCollision2D collision)
    {
        _collisionCount++;
        
        CameraController.Instance.ApplyShake(15, 40);
        
        // hit object can attack
        if(collision.GetCollider() as Node2D is IAttack hit)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.HitSound);
            var newParticle = (GpuParticles2D)BoomParticle.Instantiate();
            newParticle.GlobalPosition = GlobalPosition;
            GetTree().Root.AddChild(newParticle);

            hit.Damage(hit.Team switch
            {
                Team.Player => -1f,
                Team.Enemy => 1,
                _ => 1
            });

            switch (hit.Team)
            {
                case Team.Player:
                    break;
                case Team.Enemy:
                    break;
                case Team.Neutral:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            QueueFree();
        }
        // has collision twice
        else if (_collisionCount >= 2)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.HitSound);
            
            var newParticle = (GpuParticles2D)BoomParticle.Instantiate();
            newParticle.GlobalPosition = GlobalPosition;
            GetTree().Root.AddChild(newParticle);
            
            QueueFree();
        }
        // rebound
        else
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.HitWallSound);
            _velocity = _velocity.Bounce(collision.GetNormal());
        }
    } 
}
