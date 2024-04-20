using Godot;
using System;

public partial class EndScreen : CanvasLayer
{
    private Label _titleLabel;
    private Label _descriptionLabel;
    private Button _restartButton;
    private Button _quitButton;
    public override void _Ready()
    {
        _restartButton = GetNode<Button>("%RestartButton");
        _quitButton = GetNode<Button>("%QuitButton");

        _titleLabel = GetNode<Label>("%TitleLabel");
        _descriptionLabel = GetNode<Label>("%DescriptionLabel");

        GetTree().Paused = true;

        _restartButton.Pressed += HandleRestartButton;
        _quitButton.Pressed += HandleQuitButton;
    }

    public void SetDefeat()
    {
        _titleLabel.Text = GameConstants.DEFEAT_TITLE_LABEL;
        _descriptionLabel.Text = GameConstants.DEFEAT_DESCRIPTION_LABEL; 
    }

    public void SetWin()
    {
        _titleLabel.Text = GameConstants.WIN_TITLE_LABEL;
        _descriptionLabel.Text = GameConstants.WIN_DESCRIPTION_LABEL; 
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
