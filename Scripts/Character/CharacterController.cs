using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
	[ExportCategory("Components")] 
	[Export] public CharacterDetailsResource CharacterDetails;
	public CharacterBody2D body;
	private Vector2 _mousePosition;
	private CharacterShoot _characterShoot;
	private CharacterCrossing _characterCrossing;
	
	
	[ExportCategory("Settings")]
	[Export] public float MaxSpeed = 200;
	
	private bool _initialDone = false;
	private float _acceleration;


	public override void _Ready()
	{
		body = GetNode<CharacterBody2D>(".");

		// body = this;
		_characterShoot = GetNode<CharacterShoot>("CharacterShoot");
		_characterCrossing = GetNode<CharacterCrossing>("CharacterCrossing");
		_acceleration = MaxSpeed / 0.2f;

		EventHandler.Instance.EmitSignal(nameof(EventHandler.Instance.CharacterDetailsSettingDone));
		_initialDone = true;
	}

	public override void _Process(double delta)
	{
		if(!_initialDone) return;
		_Rotate();
		_Shoot();
		_Reload();
		_Skill();
	}

	public override void _PhysicsProcess(double delta)
	{
		if(!_initialDone) return;
		_Move(delta);
	}

	private void _Move(double delta)
	{
		var direction = Input.GetVector("move-left",
			"move-right", "move-up", "move-down");
		
		body.Velocity = new Vector2(
			Mathf.MoveToward(body.Velocity.X, direction.X * MaxSpeed, _acceleration * (float)delta),
			Mathf.MoveToward(body.Velocity.Y, direction.Y * MaxSpeed, _acceleration * (float)delta));
		
		// _body.MoveAndSlide();
		body.MoveAndSlide();
	}

	private void _Rotate()
	{
		body.LookAt(GetGlobalMousePosition());
	}

	private void _Shoot()
	{
		if(Input.IsActionJustPressed("shoot")) 
			_characterShoot.ShootAction();
	}

	private void _Reload()
	{
		if(Input.IsActionJustPressed("reload")) 
			_characterShoot.ReloadAction();
	}
	
	private void _Skill()
	{
		if(Input.IsActionJustPressed("skill-1")) 
			_characterShoot.SkillAction();
	}
}
