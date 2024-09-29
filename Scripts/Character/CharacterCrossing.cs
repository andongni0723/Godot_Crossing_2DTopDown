using Godot;
using System;

public partial class CharacterCrossing : Node
{
    // [ExportCategory("Components")]
    private CharacterController _character;

    public override void _Ready()
    {
        // _character = GetTree().Root.GetNode<CharacterController>(".");
        Node parent = GetParent();
        GD.Print("Parent Node: " + parent.GetType());
        _character = GetNode<CharacterController>("..");
        
        EventHandler.Instance.PlayerTouchWall += Crossing;
    }


    public override void _Process(double delta)
    {
        KinematicCollision2D collision = _character.body.GetLastSlideCollision();
        if (collision != null)
        {
            //TODO: if collision layer is wall
        }
    }

    private void Crossing(WallDirection wallDirection)
    {
        GD.Print(wallDirection);
        _character.Position = wallDirection is WallDirection.Down or WallDirection.Up ?
            new Vector2(_character.Position.X, -_character.Position.Y) : 
            new Vector2(-_character.Position.X, _character.Position.Y);
    }
}
