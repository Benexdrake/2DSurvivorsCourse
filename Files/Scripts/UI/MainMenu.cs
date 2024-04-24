using Godot;
using System;
using System.Threading.Tasks;

public partial class MainMenu : CanvasLayer
{
    [Export] PackedScene _optionsMenu;
    private ScreenTransition _transition;
    public override void _Ready()
    {
        _transition = GetTree().Root.GetNode<ScreenTransition>("ScreenTransition");
        GetNode<Button>("%PlayButton").Pressed += HandlePlayButtonPressed;
        GetNode<Button>("%OptionsButton").Pressed += HandleOptionsButtonPressed;
        GetNode<Button>("%QuitButton").Pressed += HandleQuitButtonPressedAsync;
        GetNode<Button>("%UpgradeButton").Pressed += HandleUpgradeButtonPressedAsync;
    }

    private void HandleUpgradeButtonPressedAsync()
    {
        _transition.TransitionToScene(GameConstants.META_MENU_SCENE);
    }


    private void HandlePlayButtonPressed()
    {
        _transition.TransitionToScene(GameConstants.CHARACTER_MENU_SCENE);
    }

    private async void HandleOptionsButtonPressed()
    {
        _transition.Transition();
        await ToSignal(_transition,"TransitionedHalfway");

        var optionsInstance = _optionsMenu.Instantiate() as OptionsMenu;
        AddChild(optionsInstance);
        optionsInstance.BackPressed += HandleBackPressed;

    }

    private async void HandleBackPressed(Node node)
    {
        _transition.Transition();
        await ToSignal(_transition,"TransitionedHalfway");
        node.QueueFree();
    }


    private async void HandleQuitButtonPressedAsync()
    {
        _transition.Transition();
        await ToSignal(_transition,"TransitionedHalfway");
        GetTree().Quit();
    }
}
