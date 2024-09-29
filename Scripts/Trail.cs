using Godot;
using System;
using System.Collections.Generic;

public partial class Trail : Line2D
{
    [ExportCategory("Setting")]
    [Export] public float TrailWidth = 20;

    private Queue<Vector2> _posQueue = new();
    private Node2D _parentNode;
    
    public override void _Ready()
    {
        Width = TrailWidth;
        _parentNode = GetParent() as Node2D;
    }

    public override void _Process(double delta)
    {
        var point = _parentNode.GlobalPosition;
        
        
        _posQueue.Enqueue(point);
        
        if(_posQueue.Count > TrailWidth)
            _posQueue.Dequeue();
        

        ClearPoints();
        
        foreach (var pos in _posQueue)
        {
            AddPoint(ToLocal(pos));
            // AddPoint(pos);
        }

    }
}
