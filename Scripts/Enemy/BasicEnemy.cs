using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D, IAttack
{
    [ExportCategory("Settings")]
    [Export] public float Speed = 100;
    [Export] public float Health = 1;
    
    protected BaseEnemyAttack EnemyAttack;
    public Team Team => Team.Enemy;

    public float TargetDistance = 1000f;
    public Node2D Target;
    protected Vector2 TargetDirection;
    
    public override void _Ready()
    {
        Target = GetNode<Node2D>("/root/GameWorld/Tower");
        EnemyAttack = GetNode<BaseEnemyAttack>("EnemyAttack");
    }
    
    public override void _Process(double delta)
    {
        TargetDistance = GlobalPosition.DistanceTo(Target.GlobalPosition);
    }


    public void Move()
    {
        TargetDirection = (Target.GlobalPosition - GlobalPosition).Normalized();
        Velocity = TargetDirection * Speed;
        MoveAndSlide();
        Rotate();
    }

    public virtual void Rotate()
    {
        var angle = Mathf.Atan2(TargetDirection.Y, TargetDirection.X);
        Rotation = angle;
    }

    public void Damage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            QueueFree();
    }
}
