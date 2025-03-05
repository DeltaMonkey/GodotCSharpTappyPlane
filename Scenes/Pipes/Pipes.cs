using Godot;
using System;

public partial class Pipes : Node2D
{
	const float SPEED = 120.0f;
	
	[Export] VisibleOnScreenNotifier2D _visibleOnScreenNotifier2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
	}

	private void OnScreenExited() 
	{
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
		Vector2 position = GlobalPosition;
	 	position.X -= SPEED * (float)delta;
		GlobalPosition = position;
    }

}
