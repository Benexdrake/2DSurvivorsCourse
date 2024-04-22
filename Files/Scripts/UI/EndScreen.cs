using Godot;
using System;

public partial class EndScreen : CanvasLayer
{
    private Label _titleLabel;
    private Label _descriptionLabel;
    private Button _restartButton;
    private Button _quitButton;

    private PanelContainer _panelContainer;
    public override void _Ready()
    {
        _restartButton = GetNode<Button>("%RestartButton");
        _quitButton = GetNode<Button>("%QuitButton");

        _titleLabel = GetNode<Label>("%TitleLabel");
        _descriptionLabel = GetNode<Label>("%DescriptionLabel");

        _panelContainer = GetNode<PanelContainer>("%PanelContainer");

        _panelContainer.PivotOffset = _panelContainer.Size / 2;

        var tween = CreateTween();
        tween.TweenProperty(_panelContainer,"scale", Vector2.Zero,0);
        tween.TweenProperty(_panelContainer,"scale", Vector2.One,.4)
        .SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Back);

        GetTree().Paused = true;

        _restartButton.Pressed += HandleRestartButton;
        _quitButton.Pressed += HandleQuitButton;
    }

    public void SetDefeat()
    {
        _titleLabel.Text = GameConstants.DEFEAT_TITLE_LABEL;
        _descriptionLabel.Text = GameConstants.DEFEAT_DESCRIPTION_LABEL; 
        PlayJingle(true);
    }

    public void SetWin()
    {
        _titleLabel.Text = GameConstants.WIN_TITLE_LABEL;
        _descriptionLabel.Text = GameConstants.WIN_DESCRIPTION_LABEL; 
    }

    public void PlayJingle(bool defeat)
    {
        if(defeat)
            GetNode<AudioStreamPlayer>("DefeatStreamPlayer").Play();
        else
            GetNode<AudioStreamPlayer>("VictoryStreamPlayer").Play();
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
