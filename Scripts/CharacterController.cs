using Godot;
using System;

public partial class CharacterController : Node2D
{
    [ExportCategory("Components")]
    private CharacterBody2D _body;
    private Vector2 _mousePosition;
    private CharacterShoot _characterShoot;
    
    
    [ExportCategory("Settings")]
    [Export] public float MaxSpeed = 200;
    
    private float _acceleration;


    public override void _Ready()
    {
        _body = GetNode<CharacterBody2D>("Player/CharacterBody2D");
        _characterShoot = GetNode<CharacterShoot>("CharacterShoot");
        _acceleration = MaxSpeed / 0.2f;
    }

    public override void _Process(double delta)
    {
        // _Move(delta);
        _Rotate();
        _Shoot();
    }

    public override void _PhysicsProcess(double delta)
    {
        _Move(delta);
    }

    private void _Move(double delta)
    {
        var direction = Input.GetVector("move-left",
            "move-right", "move-up", "move-down");
        // _body.Velocity = _direction.Normalized() * MaxSpeed; 
        _body.Velocity = new Vector2(
            Mathf.MoveToward(_body.Velocity.X, direction.X * MaxSpeed, _acceleration * (float)delta),
            Mathf.MoveToward(_body.Velocity.Y, direction.Y * MaxSpeed, _acceleration * (float)delta));
        
        _body.MoveAndSlide();
    }

    private void _Rotate()
    {
        _body.LookAt(GetGlobalMousePosition());
    }

    private void _Shoot()
    {
        if(Input.IsActionJustPressed("shoot")) 
            _characterShoot.ShootAction();
    }
}
