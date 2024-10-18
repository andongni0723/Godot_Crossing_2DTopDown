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
    private float _collisionCount;
    private Vector2 _point;

    private Vector2 _contactLocalNormal;
    protected Vector2 Velocity;
 

    public override void _Ready()
    {
        Move(0);
    }

    
    public override void _PhysicsProcess(double delta)
    {
        CollisionCheck((float)delta);
    }

    protected virtual void Move(float delta)
    {
        _point = Position;
        Velocity = GlobalTransform.Y.Normalized() * Speed; 
    }
    

    private void CollisionCheck(float delta)
    {
        var collisionDetection = MoveAndCollide(Velocity * delta);
        if (collisionDetection == null) return;
        // if colliding 
        _OnCollision(collisionDetection); 
    }

    protected void PlayHitVFX()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.HitSound);
        var newParticle = (GpuParticles2D)BoomParticle.Instantiate();
        newParticle.GlobalPosition = GlobalPosition;
        GetTree().Root.AddChild(newParticle); 
    }
    
    
    protected virtual void _OnCollision(KinematicCollision2D collision)
    {
        _collisionCount++;
        
        CameraController.Instance.ApplyShake(15, 40);
        
        // hit object can attack
        if(collision.GetCollider() as Node2D is IAttack hit)
        {
            PlayHitVFX();

            hit.Damage(hit.Team switch
            {
                Team.Player => -1f,
                Team.Enemy => 1,
                _ => 1
            });
            
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
            Velocity = Velocity.Bounce(collision.GetNormal());
        }
    } 
}
