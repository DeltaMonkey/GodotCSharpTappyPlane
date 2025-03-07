using Godot;
using System;

public partial class Plane : CharacterBody2D
{
	const float GRAVITY = 800.0f;
	const float POWER = -450.0f;

	[Export] private AnimationPlayer _animationPlayer;
	[Export] private AnimatedSprite2D _animatedSprite2D;

	[Signal] public delegate void OnPlaneDiedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Called during the physics processing step of the main loop.
    public override void _PhysicsProcess(double delta)
    {
		Vector2 velocity = Velocity;
		velocity.Y += GRAVITY * (float)delta;
		
		if(Input.IsActionJustPressed("fly"))
		{
			velocity.Y = POWER;
			_animationPlayer.Play("power");
		}

		Velocity = velocity;

		MoveAndSlide();

		if(IsOnFloor())
		{
			Die();
		}
    }

    private void Die()
    {
        SetPhysicsProcess(false);
		_animatedSprite2D.Stop();
		GD.Print("Die");
		EmitSignal(SignalName.OnPlaneDied);
    }
}
