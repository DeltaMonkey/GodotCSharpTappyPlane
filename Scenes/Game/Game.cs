using Godot;
using System;

public partial class Game : Node2D
{
	[Export] private Marker2D _spawnUpper;
	[Export] private Marker2D _spawnLower;
	[Export] private Timer _spawnTimer;
	[Export] private PackedScene _pipesScene;
	[Export] private Node2D _pipesHolder;
	[Export] private Plane _plane;

	private static readonly PackedScene MAIN_SCENE =
				GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");

	private bool _gameOver = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_spawnTimer.Timeout += SpawnPipes;

		SpawnPipes();

		_plane.OnPlaneDied += GameOver;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("fly") && _gameOver)
		{
			ChangeToMain();
		}

		if(Input.IsKeyPressed(Key.Q))
		{
			ChangeToMain();
		}
	}

	public float GetSpawnY()
	{
		return (float)GD.RandRange(_spawnUpper.Position.Y, _spawnLower.Position.Y);
	}

	private void SpawnPipes() 
	{
		Pipes np = _pipesScene.Instantiate<Pipes>();
        _pipesHolder.AddChild(np);
		np.GlobalPosition = new Vector2(_spawnLower.Position.X, GetSpawnY());
	}

	private void StopPipes()
	{
		_spawnTimer.Stop();

		foreach (Pipes pipe in _pipesHolder.GetChildren())
		{
			pipe.SetProcess(false);
		}
	}

	private void GameOver()
	{
		GD.Print("Gameover!");
		StopPipes();
		_gameOver = true;
	}

	private void ChangeToMain()
	{
		GetTree().ChangeSceneToPacked(MAIN_SCENE);
	}
}
