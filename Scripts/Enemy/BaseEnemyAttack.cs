using Godot;
using System;
using System.Collections.Generic;

public partial class BaseEnemyAttack : Node
{
    [ExportCategory("Components")]
    [Export] public PackedScene AttackVfx;
    [Export] public Timer AttackTimer;
    
    private List<Node2D> _CollidedBodies = new();

    private void _on_attack_area_body_entered(Node2D body)
    {
        _CollidedBodies.Add(body);
    }
    
    private void _on_attack_area_body_exited(Node2D body)
    {
        _CollidedBodies.Remove(body);
    }


    public override void _Process(double delta)
    {
        // On Collision Stay in Area2D
        for (int i = 0; i < _CollidedBodies.Count; i++)
        {
            var body = _CollidedBodies[i];
            
            if(AttackTimer.IsStopped() && body is IAttack attack)
            {
                AttackTimer.Start();
                attack.Damage(2);
                var vfx = (Node2D)AttackVfx.Instantiate();
                vfx.GlobalPosition = body.GlobalPosition;
                GetParent().AddChild(vfx);
            } 
        }
    }
}
