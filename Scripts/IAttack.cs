using Godot;
using System;

public enum Team
{
    Player, Enemy, Neutral
}
public interface IAttack
{
    Team Team { get; }
    void Damage(float damage);
}
