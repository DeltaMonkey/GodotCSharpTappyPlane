using Godot;
using System;

public partial class Pipes : Node2D
{
	[Export] VisibleOnScreenNotifier2D _visibleOnScreenNotifier2D;
	[Export] private Area2D _upperPipe;
	[Export] private Area2D _lowerPipe;
	[Export] private Area2D _laser;
	[Export] private AudioStreamPlayer2D _scoreSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;

		_upperPipe.BodyEntered += OnPipeBodyEntered;
		_lowerPipe.BodyEntered += OnPipeBodyEntered;
		_laser.BodyEntered += OnLaserBodyEntered;

		SignalManager.Instance.OnPlaneDied += OnPlaneDied;
	}

    public override void _ExitTree()
    {
        SignalManager.Instance.OnPlaneDied -= OnPlaneDied;
    }


    private void OnPlaneDied()
    {
        SetProcess(false);
    }

	private void OnLaserBodyEntered(Node2D body)
	{
		ScoreManager.IncrementScore();
		_laser.BodyEntered -= OnLaserBodyEntered;
		_scoreSound.Play();
		// emit
	}

    private void OnPipeBodyEntered(Node2D body)
    {
        if(body is Plane)
		{
			(body as Plane).Die();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Position -= new Vector2(GameManager.SCROLL_SPEED * (float)delta, 0.0f);
	}

    private void OnScreenExited() 
	{
		QueueFree();
	}
}
