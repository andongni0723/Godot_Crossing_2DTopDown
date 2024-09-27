using Godot;
using System;

public partial class CameraController : Singleton<CameraController>
{
    [ExportCategory("Setting")]
    private float _randomStrength = 30;
    private float _shakeFade = 5;

    private Camera2D _camera;
    private RandomNumberGenerator _rng = new();
    private float _shakeStrength = 0;

    public override void _Ready()
    {
        base._Ready();
        _camera = GetNode<Camera2D>("Camera2D");
    }

    public void ApplyShake(float randomStrength, float shakeFade)
    {
        _randomStrength = randomStrength;
        _shakeFade = shakeFade;
        
        _shakeStrength = _randomStrength;
    }

    public override void _Process(double delta)
    {
        if (_shakeStrength > 0.1)
        {
            _shakeStrength = (float)Mathf.Lerp(_shakeStrength, 0, _shakeFade * delta);
            

            _camera.Offset = RandomOffset();
        }
    }
    
    private Vector2 RandomOffset()
    {
        return new Vector2(_rng.RandfRange(-_shakeStrength, _shakeStrength), _rng.RandfRange(-_shakeStrength, _shakeStrength));
    }
}
