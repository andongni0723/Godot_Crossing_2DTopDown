using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawnManager : Node2D
{
    [ExportCategory("Debug")]
    [Export] public bool DebugMode = false;
    
    [ExportCategory("Settings")]
    [Export] public Rect2 UpSpawnAreas;
    [Export] public Rect2 DownSpawnAreas;
    [Export] public Rect2 LeftSpawnAreas;
    [Export] public Rect2 RightSpawnAreas;
    
    [ExportCategory("Components")]
    [Export] public PackedScene BasicEnemy;
    [Export] public Timer SpawnTimer;
    [Export] public Timer WaveTimer;
    
    private Random _random = new();


    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
        if(DebugMode) return;
        WaveSpawn();
    }

    private void WaveSpawn()
    {
        if (!WaveTimer.IsStopped()) return;

        for (int i = 0; i < _random.Next(5, 10); i++)
        {
            // 隨機選擇一個生成區域
            var spawnAreas = new List<Rect2>() { UpSpawnAreas, DownSpawnAreas, LeftSpawnAreas, RightSpawnAreas };
            var randomIndex = _random.Next(0, spawnAreas.Count);
            var selectedArea = spawnAreas[randomIndex];
            Spawn(selectedArea);
        }
        
        WaveTimer.Start();

    }

    private void Spawn(Rect2 selectedArea)
    {
        if (!SpawnTimer.IsStopped()) return;
        SpawnTimer.Start();
            
        // 在選擇的區域內隨機生成一個點
        float xPos = (float)(_random.NextDouble() * selectedArea.Size.X + selectedArea.Position.X);
        float yPos = (float)(_random.NextDouble() * selectedArea.Size.Y + selectedArea.Position.Y);
        Vector2 spawnPosition = new Vector2(xPos, yPos);

        // 實例化並設置敵人位置
        var enemy = BasicEnemy.Instantiate() as BasicEnemy;
        enemy!.GlobalPosition = spawnPosition;

        // 將敵人加入場景
        GetTree().Root.AddChild(enemy);
    }
    
    public override void _Draw()
    {
        // 繪製一個圓形
        // DrawCircle(GlobalPosition, SpawnRadius, Colors.Blue);
        //
        // DrawRect(UpSpawnAreas, Colors.Red, false, 3);
        // DrawRect(DownSpawnAreas, Colors.Blue, false, 3);
        // DrawRect(LeftSpawnAreas, Colors.Yellow, false, 3);
        // DrawRect(RightSpawnAreas, Colors.Green, false, 3);
    }
}
