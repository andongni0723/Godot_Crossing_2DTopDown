using Godot;
using System;

public partial class GameManager : Singleton<GameManager>
{
    [Export] public Label ScoreLabel;
    private int _score = 0;
    
    
    public void AddScore(int score)
    {
        _score += score;
        ScoreLabel.Text = $"Score: {_score.ToString()}";
    }
}
