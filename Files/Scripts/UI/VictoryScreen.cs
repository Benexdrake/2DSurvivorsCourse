using Godot;
using System;

public partial class VictoryScreen : CanvasLayer
{
    private Button _restartButton;
    private Button _quitButton;
    public override void _Ready()
    {
        _restartButton = GetNode<Button>("%RestartButton");
        _quitButton = GetNode<Button>("%QuitButton");

        GetTree().Paused = true;

        _restartButton.Pressed += HandleRestartButton;
        _quitButton.Pressed += HandleQuitButton;
    }

    private void HandleQuitButton()
    {
        GetTree().Quit();
    }


    private void HandleRestartButton()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile(GameConstants.MAIN_SCENE);
    }

}
