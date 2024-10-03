using Godot;
using System;
using System.Globalization;

public partial class Glitch : ColorRect
{
    [Export] public Label PowerLabel;
    public override void _Ready()
    {
        EventHandler.Instance.TowerDamaged += SettingShaderValue;
        SettingShaderValue(0, 100);
    }

    private void SettingShaderValue(int damage, int nowHealth)
    {
        var material = Material;
        if (material is ShaderMaterial shaderMaterial)
        {
            shaderMaterial.SetShaderParameter("setting_shake_power", TowerHealthToGlitchPower(nowHealth));
            PowerLabel.Text = $"Glitch: {TowerHealthToGlitchPower(nowHealth).ToString(CultureInfo.CurrentCulture)}";
            shaderMaterial.SetShaderParameter("shake_color_rate", nowHealth <= 90 ? 0.005f : 0 );
        }

    }

    private float TowerHealthToGlitchPower(float health)
    {
        // 定義已知的數據點 (x, y)
        float[] xPoints = { 90, 50, 30, 10, 0 };
        float[] yPoints = { 0.05f, 0.2f, 0.5f, 1f, 3f };

        // 遍歷所有點，找到 x 所屬的區間，並進行線性插值
        for (int i = 0; i < xPoints.Length - 1; i++)
        {
            if (health <= xPoints[i] && health >= xPoints[i + 1])
            {
                // 線性插值公式
                float t = (health - xPoints[i + 1]) / (xPoints[i] - xPoints[i + 1]);
                return yPoints[i + 1] + t * (yPoints[i] - yPoints[i + 1]);
            }
        }

        return health switch
        {
            >= 90 => 0,
            <= 0 => 3f,
            _ => throw new ArgumentOutOfRangeException(nameof(health), health, null)
        };
    }

}
