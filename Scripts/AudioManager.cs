using Godot;
using System;

public partial class AudioManager : Singleton<AudioManager>
{
    [ExportCategory("Components")]
    [Export] public AudioStreamPlayer BgmPlayer;
    [Export] public AudioStreamPlayer SfxPlayer;
    [Export] public AudioStreamPlayer EnemySfxPlayer;
    
    
    [ExportCategory("Sound Settings")]
    [Export] public AudioStream FightBgm;
    [Export] public AudioStream FireSound;
    [Export] public AudioStream HitWallSound;
    [Export] public AudioStream HitSound;

    public override void _Ready()
    {
        base._Ready();
    }

    public void PlaySound(AudioStream audioStream)
    {
        SfxPlayer.Stream = audioStream;
        SfxPlayer.Play();
    }

}
