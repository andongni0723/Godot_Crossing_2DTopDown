using Godot;
using System;

public partial class GameManager : Singleton<GameManager>
{
    [ExportCategory("UI")]
    [Export] public Label VersionLabel;
    [Export] public Label ScoreLabel;
    [Export] public Label HealthLabel;
    [Export] public Panel BlackPanel;

    private int _score = 0;

    public override void _Ready()
    {
        base._Ready();
        EventHandler.Instance.TowerDamaged += UpdateHealthUI;
        EventHandler.Instance.TowerDead += OnTowerDead;
        
        _SetVersion();
    }
    
    //---------- Tools ----------//
    public void AddScore(int score)
    {
        _score += score;
        ScoreLabel.Text = $"Score: {_score.ToString()}";
    }

    
    //---------- Events ----------//
    private void OnTowerDead()
    {
        BlackPanel.Visible = true;
        GetTree().Paused = true;
    }

    private void UpdateHealthUI(int damage, int nowHealth)
    {
        HealthLabel.Text = $"Health: {nowHealth.ToString()}";
    }

    private void _SetVersion()
    {
        VersionLabel.Text = $"Crossing v{ProjectSettings.GetSetting("application/config/version")}";
    }


}
