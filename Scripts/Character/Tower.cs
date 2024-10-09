using Godot;
using System;

public partial class Tower : Node2D, IAttack
{
    public Team Team => Team.Player;
    
    
    private float _maxHealth = 100;
    [Export] public float Health = 100;
    
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


    public void Damage(float damage)
    {
        CameraController.Instance.ApplyShake(15, 40);
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, _maxHealth);
        EventHandler.Instance.EmitSignal(nameof(EventHandler.Instance.TowerDamaged), damage, Health);
        
        // Dead
        if(Health <= 0) 
            EventHandler.Instance.EmitSignal(nameof(EventHandler.Instance.TowerDead));


    }
    

}
