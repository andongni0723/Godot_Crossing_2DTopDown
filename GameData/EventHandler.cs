using Godot;
using System;

public partial class EventHandler : Singleton<EventHandler>
{
    [Signal]
    public delegate void PlayerTouchWallEventHandler(WallDirection wallDirection);
    
    [Signal]
    public delegate void TowerDamagedEventHandler(int damage, int nowHealth);
}
