using Godot;
using System;

public partial class Tower : Node2D, IAttack
{
    [Export] public int Health = 100;
    
    [Export] public Label HealthLabel;
    [Export] public Timer HealCooldownTimer;
    
    public override void _Ready()
    {
        HealCooldownTimer.Timeout += _Heal;
    }

    private void _Heal()
    {
        if(Health >= 100) return;
        Damage(-1);
    }
    
    public void Damage(int damage)
    {
        GD.Print($"Tower Damaged (-{damage})!!!!!!!");
        CameraController.Instance.ApplyShake(15, 40);
        Health -= damage;
        HealthLabel.Text = $"Health: {Health.ToString()}";
        EventHandler.Instance.EmitSignal(nameof(EventHandler.Instance.TowerDamaged), damage, Health);
    }
    

}
