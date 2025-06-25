using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }	

	private int _score { get; set; } = 0;// the game score
	private int _highScore { get; set; } = 0; // the high score

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		ResetScore();
	}

	public static int GetScore() => Instance._score; // return the score
	public static int GetHighScore() => Instance._highScore; // return the highScore
	public static void SetScore(int score) // set the score
	{
		Instance._score = score;
		if(Instance._score > Instance._highScore) 
		{
			Instance._highScore = Instance._score;
		}
		GD.Print($"Score: {Instance._score}, High Score: {Instance._highScore}");
		SignalManager.EmitOnScored();
	}
	public static void ResetScore() // reset score to 0
	{
		SetScore(0);		
	}
	public static void IncrementScore() => SetScore(GetScore() + 1);// increment score by 1

}
