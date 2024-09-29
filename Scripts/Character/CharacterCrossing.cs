using Godot;
using System;

public partial class CharacterCrossing : Node
{
    [ExportCategory("Components")] 
    [Export] public PackedScene CrossingParticle;
    [Export] public Timer CrossingCooldownTimer;
    private CharacterController _character;

    public override void _Ready()
    {
        Node parent = GetParent();
        GD.Print("Parent Node: " + parent.GetType());
        _character = GetNode<CharacterController>("..");
        
        EventHandler.Instance.PlayerTouchWall += Crossing;
    }

    private void Crossing(WallDirection wallDirection)
    {
        if(!CrossingCooldownTimer.IsStopped()) return;
        CrossingCooldownTimer.Start();
        
        // is not cooldown
        var newParticle = CrossingParticle.Instantiate() as Node2D;
        GetTree().Root.AddChild(newParticle);
        newParticle!.GlobalPosition = _character.GlobalPosition;
        
        _character.Position = wallDirection is WallDirection.Down or WallDirection.Up ?
            new Vector2(_character.Position.X, -_character.Position.Y) : 
            new Vector2(-_character.Position.X, _character.Position.Y);
    }
}
