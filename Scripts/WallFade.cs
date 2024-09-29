using Godot;
using System;

public partial class WallFade : Node2D
{
    [ExportCategory("Settings")] 
    [Export] public Node2D EndPoint;
    [Export] public float duration = 1;


    public override void _Ready()
    {
        Fade();
    }

    private void Fade()
    {
        var tween = CreateTween().SetTrans(Tween.TransitionType.Sine);;
        tween.TweenProperty(this, "position", EndPoint.GlobalPosition, duration);
        tween.Parallel().TweenProperty(this, "modulate", new Color(1, 1, 1, 0), duration);
        tween.Play();
        tween.TweenCallback(Callable.From(QueueFree));
    }

}
