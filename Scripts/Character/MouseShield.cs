using Godot;
using System;

public partial class MouseShield : RigidBody2D
{
    public override void _Process(double delta)
    {
        GlobalPosition = GetGlobalMousePosition();
        
        LookAt(Vector2.Zero);
    }
}
