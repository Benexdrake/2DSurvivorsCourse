using Godot;
using System;

public partial class CharacterScreen : CanvasLayer
{
    [Export] PackedScene _player;
    public override void _Ready()
    {
        var charCards = GetNode<HBoxContainer>("%CardContainer").GetChildren();
        GD.Print(charCards);
        foreach (CharacterCard card in charCards)
        {
            card.StartGameWithCharacter += HandleStartGameWithCharacter;
        }
        GetTree().Paused = true;
    }

    private void HandleStartGameWithCharacter(int atk, int speed, int hp, Texture2D texture)
    {
        GetTree().Paused = false;
        PlayerStats.attack = atk;
        PlayerStats.hp = hp;
        PlayerStats.speed = speed;
        PlayerStats.texture = texture;

        GetTree().ChangeSceneToFile(GameConstants.MAIN_SCENE);
    }

}
