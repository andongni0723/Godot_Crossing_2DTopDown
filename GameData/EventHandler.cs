using Godot;
using System;

public partial class EventHandler : Singleton<EventHandler>
{
    [Signal]
    public delegate void PlayerTouchWallEventHandler(WallDirection wallDirection);
}
