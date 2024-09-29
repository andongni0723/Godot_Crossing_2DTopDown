using Godot;
using System;

public enum WallDirection
{
    Up = 0, Down, Left, Right
}

public partial class Wall : Node2D
{
    [ExportCategory("Setting")]
    [Export] public WallDirection wallDirection; // up, down, left, right
    
    public override void _Ready()
    {
        GD.Print("Wall Ready");
    }

    private void _on_area_2d_body_entered(Node2D body)
    {
        if(body.IsInGroup("Bullet"))
        {
            //TODO: bullet vfx
        }
        else if (body.IsInGroup("Player"))
        {
            EventHandler.Instance.EmitSignal(nameof(EventHandler.Instance.PlayerTouchWall), (int)wallDirection);
            // EventHandler.Instance.EmitSignal(nameof(EventHandler.Instance.PlayerTouchWall), wallDirection);

        }
    }
}
