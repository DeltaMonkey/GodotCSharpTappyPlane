using Godot;
using System;

public partial class GameManager : Node
{
    public const float SCROLL_SPEED = 120.0f;

    public static GameManager Instance { get; private set; }

    private PackedScene _gameScene = GD.Load<PackedScene>("res://Scenes/Game/Game.tscn");
    private PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");
    // private PackedScene _simpleTransitionScene =
    //                 GD.Load<PackedScene>("res://Scenes/SimpleTransition/SimpleTransition.tscn");
    private PackedScene _complexTransitionScene =
                     GD.Load<PackedScene>("res://Scenes/ComplexTransition/ComplexTransition.tscn");
    private PackedScene _nextScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Instance = this;
    }

    public static PackedScene GetNextScene()
    {
        return Instance._nextScene;
    }

    public void LoadNextScene(PackedScene ns)
    {
        _nextScene = ns;
        //GetTree().ChangeSceneToPacked(Instance._simpleTransitionScene);
        CanvasLayer cxt = (CanvasLayer)_complexTransitionScene.Instantiate();
        AddChild(cxt);
    }

    public static void LoadMain()
    {
        //Instance.GetTree().ChangeSceneToPacked(Instance._mainScene);
        Instance.LoadNextScene(Instance._mainScene);
    }

    public static void LoadGame()
    {
        //Instance.GetTree().ChangeSceneToPacked(Instance._gameScene);
        Instance.LoadNextScene(Instance._gameScene);
    }
}
