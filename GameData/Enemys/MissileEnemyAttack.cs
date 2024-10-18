using Godot;
using System;
using System.Threading.Tasks;

public partial class MissileEnemyAttack : BaseEnemyAttack
{
    [ExportCategory("Settings")]
    [Export] public PackedScene MissileBullet;
    [Export] public Node2D[] FirePoints;
    [Export] public int FireRound;


    public override void Attack()
    {
        AttackAsyncAction();
    }

    private async void AttackAsyncAction()
    {
        for (int i = 0; i < FireRound; i++)
        {
            foreach (var point in FirePoints)
            {
                var bullet = (MissileBullet)MissileBullet.Instantiate();
                bullet.TargetPosition = Enemy.Target.GlobalPosition;
                bullet.GlobalPosition = point.GlobalPosition;
                GetTree().Root.CallDeferred("add_child", bullet); 
                await Task.Delay(50);
            }
        } 
    }
}
