using Godot;
using System;

public partial class CharacterCrossing : Node
{
    // [ExportCategory("Components")]
    private CharacterController _character;

    public override void _Ready()
    {
        // _character = GetTree().Root.GetNode<CharacterController>(".");
        _character = GetNode<CharacterController>("..");
    }


    public override void _Process(double delta)
    {
        KinematicCollision2D collision = _character.body.GetLastSlideCollision();
        if (collision != null)
        {
            //TODO: if collision layer is wall
        }
    }
}
