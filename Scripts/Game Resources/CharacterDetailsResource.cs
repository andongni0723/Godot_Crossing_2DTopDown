using Godot;
using System;

[Tool]
public partial class CharacterDetailsResource : Resource
{
    [Export] public string CharacterName;
    [Export] public string CharacterDescription;

    [Export] public int ClipBulletSize;
    [Export] public float ReloadTime;
    [Export] public float FireRate;
}
