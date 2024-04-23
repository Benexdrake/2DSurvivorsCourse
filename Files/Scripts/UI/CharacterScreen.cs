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
        var entitiesLayer = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_ENTITIES_LAYER);
		entitiesLayer.AddChild(_player.Instantiate());

        // var player = GetTree().GetFirstNodeInGroup(GameConstants.PLAYER) as Player;
        // player.SetCharacter(atk,speed,hp,texture);
        // GetTree().Paused = false;
        // QueueFree();
    }

}
