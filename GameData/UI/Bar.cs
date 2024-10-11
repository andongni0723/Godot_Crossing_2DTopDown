using Godot;
using System;

public partial class Bar : Control
{
    [ExportCategory("Components")]
    [Export] public ProgressBar ProgressBar;
    [Export] public Label BarText;
    
    // ------Tool------
    public void Initial(int minValue, int maxValue)
    {
        ProgressBar.MinValue = minValue;
        ProgressBar.MaxValue = maxValue;
        ProgressBar.Value = maxValue;
        BarText.Text = $"{maxValue}";
    }
    
    public void UpdateValue(int value)
    {
        ProgressBar.Value = value;
        BarText.Text = $"{value}";
    }
}
