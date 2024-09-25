using Godot;
using System;

public partial class BoomParticle : GpuParticles2D
{
    public override void _Ready()
    {
        Emitting = true;
    }
    private void _on_finished()
    {
        QueueFree();
    }
}
