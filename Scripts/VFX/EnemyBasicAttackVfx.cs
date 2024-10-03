using Godot;
using System;

public partial class EnemyBasicAttackVfx : Node2D
{
    [ExportCategory("Settings")]
    [Export] public float Duration = 0.5f;

    public override void _Ready()
    {
        Scale = new Vector2(0.3f, 0.3f);
        Play();
    }

    private void Play()
    {
        Tween tween = CreateTween().SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Sine);
        tween.TweenProperty(this, "scale", Vector2.One, Duration);
        tween.Parallel().TweenProperty(this, "modulate:a", 0, Duration);
        tween.TweenCallback(Callable.From(QueueFree));
    }
}
