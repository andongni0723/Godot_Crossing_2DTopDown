using Godot;
using System;

public partial class MouseShield : RigidBody2D
{
    // [Export] public float Radius = 10;
    // [Export] public Vector2 Center = Vector2.Zero;
    //
    // private Vector2 _direction = Vector2.Zero;
    // private Vector2 _normal = Vector2.Zero;
    // private float _slope = 0;
    //
    // /// <summary>
    // /// Input the circle radius and line slope, return the point on the circle (Because of the line is vector, so the point is unique)
    // /// </summary>
    // /// <param name="radius"></param>
    // /// <param name="slope"></param>
    // /// <returns>
    // /// a = slope.Y, b = slope.X
    // /// a^2 + b^2!=0, x = (b r)/sqrt(a^2 + b^2), b!=0, y = (a r)/sqrt(a^2 + b^2)
    // /// </returns>
    // public Vector2 CircleAndLineGetPoint(float radius, Vector2 slope)
    // {
    //     var a = slope.Y;
    //     var b = slope.X;
    //     
    //     return new Vector2(
    //         (b * radius) / Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2)),
    //         (a * radius) / Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2)));
    // }
    //
    // public override void _Process(double delta)
    // {
    //     // GlobalPosition = GetGlobalMousePosition();
    //     _direction = (GetGlobalMousePosition() - Center).Normalized();
    //     _normal = new Vector2(-_direction.Y, _direction.X);
    //     _slope = _normal.Y / _normal.X;
    //     
    //     // Let the shield follow the mouse direction, and the shield will distance from the center by Radius
    //     GlobalPosition = CircleAndLineGetPoint(Radius, _direction);
    //     
    //     LookAt(Vector2.Zero);
    // }
    
    [Export] public float Radius = 100;
    [Export] public Vector2 Center = Vector2.Zero;

    private Vector2 _direction = Vector2.Zero;

    /// <summary>
    /// Input the circle radius and line slope, return the point on the circle
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="direction"></param>
    /// <returns>The Point Position on circle</returns>
    public Vector2 GetPointOnCircle(float radius, Vector2 direction)
    {
        var normalizedDir = direction.Normalized();
        return normalizedDir * radius;
    }

    public override void _Process(double delta)
    {
        _direction = (GetGlobalMousePosition() - Center).Normalized();

        // Update shield direction with mouse position, distance is Radius of scope circle
        GlobalPosition = Center + GetPointOnCircle(Radius, _direction);
        
        LookAt(Center);
    }
}
