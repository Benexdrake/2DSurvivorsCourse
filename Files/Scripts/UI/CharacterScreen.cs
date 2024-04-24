using Godot;
using System;

public partial class CharacterScreen : CanvasLayer
{
    [Export] PackedScene _player;
    private ScreenTransition _transition;
    public override void _Ready()
    {
        _transition = GetTree().Root.GetNode<ScreenTransition>("ScreenTransition");
        var charCards = GetNode<HBoxContainer>("%CardContainer").GetChildren();
        foreach (CharacterCard card in charCards)
        {
            card.StartGameWithCharacter += HandleStartGameWithCharacter;
        }
        GetTree().Paused = true;
    }

    private void HandleStartGameWithCharacter(int atk, int speed, int hp, Texture2D texture)
    {
        GetTree().Paused = false;
        GameStats.attack = atk;
        GameStats.hp = hp;
        GameStats.speed = speed;
        GameStats.texture = texture;

        _transition.TransitionToScene(GameConstants.MAIN_SCENE);
    }
}
