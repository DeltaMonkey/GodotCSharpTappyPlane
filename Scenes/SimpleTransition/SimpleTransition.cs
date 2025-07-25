using Godot;
using System;

public partial class SimpleTransition : Control
{
	[Export] private Timer _timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer.Timeout += OnTimeOut;
	}

    private void OnTimeOut()
    {
		GetTree().ChangeSceneToPacked(GameManager.GetNextScene());
    }
}
