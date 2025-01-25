using Godot;
using System;

public partial class CharacterShoot : Node2D
{ 
    [ExportCategory("Components")]
    [Export] public CharacterController CharacterController;
    [Export] public PackedScene Bullet;
    [Export] public PackedScene ExplosionBullet;
    [Export] public PackedScene FireParticle;
    [Export] public BulletBar BulletBar;
    [Export] public Node2D BulletSpawnPoint;
    [Export] public Timer ReloadTimer;
    [Export] public Timer SkillTimer;
    
    private int _currentAmmoCount = 0;
    private PackedScene _currentBullet;
    
    [ExportCategory("Settings")]
    [Export] public float BulletSpeed = 500;
    
    public override void _Ready()
    {
        EventHandler.Instance.CharacterDetailsSettingDone += _Initial;
    }
    
    private void _Initial()
    {
        _currentAmmoCount = CharacterController.CharacterDetails.ClipBulletSize;
        _currentBullet = Bullet;
        BulletBar.Initial(0, _currentAmmoCount);
    }

    #region Tools


    public void ShootAction()
    {
        // When no bullet and not reloading
        if (_currentAmmoCount <= 0 && ReloadTimer.IsStopped())
        {
            ReloadAction();
            return;
        }
        
        // When reloading
        if (!ReloadTimer.IsStopped()) return;
        
        _PlayFX();
        _CreateBullet();
        _CreateFireParticle();
        _UpdateBulletUI();
    }
    
    public void ReloadAction()
    {
        AudioManager.Instance.PlayImportanceSound(AudioManager.Instance.ReloadSound);
        ReloadTimer.WaitTime = CharacterController.CharacterDetails.ReloadTime;
        ReloadTimer.Start();
    }
    
    public void SkillAction()
    {
        SkillTimer.Start();
        _currentBullet = ExplosionBullet;
    }

    private void _on_shoot_reload_timer_timeout()
    {
        _currentAmmoCount = CharacterController.CharacterDetails.ClipBulletSize;
        BulletBar.UpdateValue(_currentAmmoCount);
    }
    
    private void _on_shoot_skill_timer_timeout()
    {
        _currentBullet = Bullet;
    }
    
    #endregion

    private void _PlayFX()
    {
        CameraController.Instance.ApplyShake(5, 40);
        AudioManager.Instance.PlaySound(AudioManager.Instance.FireSound); 
    }

    private RigidBody2D _CreateBullet()
    {
        var newBullet = (RigidBody2D)_currentBullet.Instantiate();
        newBullet.GlobalPosition = BulletSpawnPoint.GlobalPosition;
        newBullet.GlobalRotation = BulletSpawnPoint.GlobalRotation + Mathf.Pi;
        newBullet.GetNode<Bullet>(".").Speed = BulletSpeed;
        GetTree().Root.AddChild(newBullet); 
        return newBullet;
    }

    private GpuParticles2D _CreateFireParticle()
    {
        var newBoomParticle = (GpuParticles2D)FireParticle.Instantiate();
        newBoomParticle.GlobalPosition = BulletSpawnPoint.GlobalPosition;
        GetTree().Root.AddChild(newBoomParticle);
        return newBoomParticle;
    }
    
    private void _UpdateBulletUI()
    {
        _currentAmmoCount--;
        BulletBar.UpdateValue(_currentAmmoCount);
    }
}