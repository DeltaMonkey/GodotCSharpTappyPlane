using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }	

	private uint _score { get; set; } = 0;// the game score
	private uint _highScore { get; set; } = 0; // the high score

	private const string SCORE_FILE = "user://tappy.save";
	/*
		.txt
		.dat
		.bin
	*/

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		LoadScoreFromFile();
	}

    public override void _ExitTree()
    {
		SaveScoreToFile();
    }

	public static uint GetScore() => Instance._score; // return the score
	public static uint GetHighScore() => Instance._highScore; // return the highScore

	public static void SetScore(uint score) // set the score
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

	private void SaveScoreToFile()
	{
		using FileAccess file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Write);
		if(file != null)
		{
			// file.StoreString(_highScore.ToString());
			file.Store32(_highScore);
		}
	}

	private void LoadScoreFromFile()
	{
		using FileAccess file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Read);
		if(file != null)
		{
			_highScore = file.Get32();
		}
	}

}
