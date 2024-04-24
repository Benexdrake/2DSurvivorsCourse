using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
    private AnimationPlayer _animationPlayer;
    private PanelContainer _panelContainer;
    [Export] PackedScene _optionsMenu;
    private ScreenTransition _transition;

    bool isClosing;
    public override void _Ready()
    {
        _transition = GetTree().Root.GetNode<ScreenTransition>("ScreenTransition");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _panelContainer = GetNode<PanelContainer>("%PanelContainer");

        GetNode<Button>("%ResumeButton").Pressed += HandleResumeButton;
        GetNode<Button>("%OptionsButton").Pressed += HandleOptionsButton;
        GetNode<Button>("%QuitToMenuButton").Pressed += HandleQuitToMenuButton;
        GetNode<Button>("%QuitGameButton").Pressed += HandleQuitGameButton;
        GetTree().Paused = true;

        _animationPlayer.Play(GameConstants.ANIM_IN);

        _panelContainer.PivotOffset = _panelContainer.Size/2;

        var tween = CreateTween();
        tween.TweenProperty(_panelContainer, "scale", Vector2.Zero, 0);
        tween.TweenProperty(_panelContainer, "scale", Vector2.One, .3)
        .SetEase(Tween.EaseType.Out). SetTrans(Tween.TransitionType.Back);
    }

    private async void HandleOptionsButton()
    {
        _transition.Transition();
        await ToSignal(_transition,"TransitionedHalfway");
        var optionsInstance = _optionsMenu.Instantiate() as OptionsMenu;
        AddChild(optionsInstance);
        optionsInstance.BackPressed += HandleOptionsButtonBackPressed;
    }

    private async void HandleOptionsButtonBackPressed(Node node)
    {
        node.QueueFree();
    }

    private void HandleQuitToMenuButton()
    {
        GetTree().Paused = false;
        _transition.TransitionToScene(GameConstants.MAIN_MENU_SCENE);
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("pause"))
        {
            Close();
            GetTree().Root.SetInputAsHandled();
        }            
    }

    private void HandleResumeButton()
    {
        Close();
    }


    private void HandleQuitGameButton()
    {
        Close();
        GetTree().Quit();
    }

    private async void Close()
    {
        if(isClosing)
            return;
        isClosing = true;
        _animationPlayer.PlayBackwards(GameConstants.ANIM_IN);
        var tween = CreateTween();
        tween.TweenProperty(_panelContainer, "scale", Vector2.One, 0);
        tween.TweenProperty(_panelContainer, "scale", Vector2.Zero, .3)
        .SetEase(Tween.EaseType.In). SetTrans(Tween.TransitionType.Back);

        await ToSignal(tween, "finished");
        GetTree().Paused = false;
        QueueFree();
    }

}
