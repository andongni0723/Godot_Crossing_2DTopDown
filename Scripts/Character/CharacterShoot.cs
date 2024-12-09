using Godot;
using System;

public partial class CharacterShoot : Node2D
{ 
    //Header component use attribute
    
    [ExportCategory("Components")]
    [Export] public CharacterController CharacterController;
    [Export] public PackedScene Bullet;
    [Export] public PackedScene FireParticle;
    [Export] public BulletBar BulletBar;
    [Export] public Node2D BulletSpawnPoint;
    [Export] public Timer ReloadTimer;
    
    private int currentBullet = 0;
    
    [ExportCategory("Settings")]
    [Export] public float BulletSpeed = 500;
    
    public override void _Ready()
    {
        currentBullet = CharacterController.CharacterDetails.ClipBulletSize;
    }

    #region Tools


    public void ShootAction()
    {
        // When no bullet and not reloading
        if (currentBullet <= 0 && ReloadTimer.IsStopped())
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

    private void _on_shoot_reload_timer_timeout()
    {
        currentBullet = CharacterController.CharacterDetails.ClipBulletSize;
        BulletBar.UpdateValue(currentBullet);
    }
    
    #endregion

    private void _PlayFX()
    {
        CameraController.Instance.ApplyShake(5, 40);
        AudioManager.Instance.PlaySound(AudioManager.Instance.FireSound); 
    }

    private RigidBody2D _CreateBullet()
    {
        var newBullet = (RigidBody2D)Bullet.Instantiate();
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
        currentBullet--;
        BulletBar.UpdateValue(currentBullet);
    }
}