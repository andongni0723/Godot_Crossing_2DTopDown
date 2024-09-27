using Godot;
using System;

public partial class CharacterController : Node2D
{
    [ExportCategory("Components")]
    public CharacterBody2D body;
    private Vector2 _mousePosition;
    private CharacterShoot _characterShoot;
    private CharacterCrossing _characterCrossing;
    
    
    [ExportCategory("Settings")]
    [Export] public float MaxSpeed = 200;
    
    private float _acceleration;


    public override void _Ready()
    {
        body = GetNode<CharacterBody2D>("Player/CharacterBody2D");
        _characterShoot = GetNode<CharacterShoot>("CharacterShoot");
        _characterCrossing = GetNode<CharacterCrossing>("CharacterCrossing");
        _acceleration = MaxSpeed / 0.2f;
    }

    public override void _Process(double delta)
    {
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
        
        body.Velocity = new Vector2(
            Mathf.MoveToward(body.Velocity.X, direction.X * MaxSpeed, _acceleration * (float)delta),
            Mathf.MoveToward(body.Velocity.Y, direction.Y * MaxSpeed, _acceleration * (float)delta));
        
        // _body.MoveAndSlide();
        body.MoveAndSlide();
    }

    private void _Rotate()
    {
        body.LookAt(GetGlobalMousePosition());
    }

    private void _Shoot()
    {
        if(Input.IsActionJustPressed("shoot")) 
            _characterShoot.ShootAction();
    }
}
