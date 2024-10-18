using Godot;
using System;
using Range = Godot.Range;

public partial class MissileBullet : Bullet
{
    // [ExportCategory("Settings")]
    // [Export] public float minAngle = -50;
    // [Export] public float maxAngle = 50;
    //
    // public Node2D Target;
    // private float _multipleAngle;
    //
    // public override void _Ready()
    // {
    //     _multipleAngle = Mathf.Lerp(minAngle, maxAngle, GD.Randf());
    // }
    //
    // public override void _PhysicsProcess(double delta)
    // {
    //     base._PhysicsProcess(delta);
    //     if(Target == null) return;
    //     Move((float)delta);
    //     BulletRotate();
    // }
    //
    // protected override void Move(float delta)
    // {
    //     Position += GlobalTransform.X.Normalized() * Speed * delta;
    //     Velocity = Vector2.Zero;
    // }
    //
    // private void BulletRotate()
    // {
    //     Vector2 targetDirection = Vector2.Zero - GlobalPosition;
    //     float angle = Mathf.Atan2(targetDirection.Y, targetDirection.X);
    //     Rotation = angle;
    //     Rotation *= _multipleAngle;
    // }
    //
    // protected override void _OnCollision(KinematicCollision2D collision)
    // {
    //     PlayHitVFX();
    //     QueueFree();
    // }
    //
    
    private Vector2 _startPosition;    // 導彈的起點（槍口位置）
    public Vector2 TargetPosition;   // 導彈的目標點
    private Vector2 _controlPoint;     // 貝塞爾曲線的控制點，用於決定彎曲的弧度

    private float _travelTime = 0;     // 導彈的飛行時間
    [Export] public float TravelDuration = 0.5f; // 導彈飛行的總時長

    public override void _Ready()
    {
        // 設置導彈的初始位置
        // GlobalPosition = _startPosition;
        _startPosition = GlobalPosition;
        Init(GlobalPosition, TargetPosition);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        _travelTime += (float)delta;
        float t = _travelTime / TravelDuration;

        // 當t達到1時，表示導彈已經到達目標點
        if (t >= 1)
        {
            QueueFree(); // 導彈到達後刪除
        }
        else
        {
            // 使用貝塞爾曲線計算導彈的位置
            GlobalPosition = CalculateBezierPosition(t);
        }
    }

    // 計算二次貝塞爾曲線上的位置
    private Vector2 CalculateBezierPosition(float t)
    {
        float u = 1 - t;
        return (u * u * _startPosition) + (2 * u * t * _controlPoint) + (t * t * TargetPosition);
    }

    // 初始化導彈的運動參數
    public void Init(Vector2 startPosition, Vector2 targetPosition)
    {
        _startPosition = startPosition;
        TargetPosition = targetPosition;

        // 生成一個隨機的控制點，用於決定導彈的弧度
        Vector2 direction = (TargetPosition - _startPosition).Normalized();
        float distance = _startPosition.DistanceTo(TargetPosition);
        _controlPoint = _startPosition + direction.Rotated((float)(GD.RandRange(90, -90) * Mathf.Pi / 180.0)) * distance / 2;
    }

    protected override void _OnCollision(KinematicCollision2D collision)
    {
        if(collision.GetCollider() as Node2D is IAttack hit)
        {
            hit.Damage(hit.Team switch
            {
                Team.Player => 1f,
                _ => 0
            });
            
        }
        
        PlayHitVFX();
        QueueFree();
    }
}
