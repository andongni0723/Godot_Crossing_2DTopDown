using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D, IAttack
{
    [ExportCategory("Settings")]
    [Export] public float Speed = 100;

    private BaseEnemyAttack _enemyAttack;
    public Team Team => Team.Enemy;


    private Node2D _target;
    private Vector2 _targetDirection;

    public override void _Ready()
    {
        _target = GetNode<Node2D>("/root/GameWorld/Tower");
        _enemyAttack = GetNode<BaseEnemyAttack>("EnemyAttack");
    }

    // public override void (double delta)
    public override void _PhysicsProcess(double delta)
    {
        _targetDirection = (_target.GlobalPosition - GlobalPosition).Normalized();
        Velocity = _targetDirection * Speed;
        var angle = Mathf.Atan2(_targetDirection.Y, _targetDirection.X);
        Rotation = angle;

        MoveAndSlide();
    }


    public void Damage(float damage)
    {
        GameManager.Instance.AddScore(1);
        QueueFree();
    }
}
