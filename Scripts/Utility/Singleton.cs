using Godot;
using System;

public partial class Singleton<T> : Node2D where T : Singleton<T>
{
    private static T _instance;
    
    public static T Instance => _instance;

    public override void _Ready()
    {
        if(_instance != null)
            QueueFree();
        else
            _instance = (T)this;
    }
}