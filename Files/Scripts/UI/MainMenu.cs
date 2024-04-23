using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
    [Export] PackedScene _optionsMenu;
    public override void _Ready()
    {
        GetNode<Button>("%PlayButton").Pressed += HandlePlayButtonPressed;
        GetNode<Button>("%OptionsButton").Pressed += HandleOptionsButtonPressed;
        GetNode<Button>("%QuitButton").Pressed += HandleQuitButtonPressed;
    }

    private void HandlePlayButtonPressed()
    {
        GetTree().ChangeSceneToFile(GameConstants.CHARACTER_MENU_SCENE);
    }

    private void HandleOptionsButtonPressed()
    {
        var optionsInstance = _optionsMenu.Instantiate() as OptionsMenu;
        AddChild(optionsInstance);
        optionsInstance.BackPressed += HandleBackPressed;

    }

    private void HandleBackPressed(Node node)
    {
        node.QueueFree();
    }


    private void HandleQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
