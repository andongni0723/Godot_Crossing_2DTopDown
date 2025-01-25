using Godot;
using System;

public partial class SkillDetailsSO : Resource
{
    [Export] public string SkillName;
    [Export] public string SkillDescription;
    [Export] public Texture SkillIcon;
    [Export] public float SkillCooldown;
    [Export] public float SkillDuration;
    
    [Export] public SkillType SkillType;
    
    // Change Bullet
    [ShowIf("SkillType", "ChangeBullet")]
    
    [Export] public PackedScene Bullet;
    
}

public enum SkillType
{
    ChangeBullet,
    Buff,
    Special
}