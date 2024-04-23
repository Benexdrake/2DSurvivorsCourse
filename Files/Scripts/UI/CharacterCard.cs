using Godot;
using System;

public partial class CharacterCard : PanelContainer
{
    [Export] string _characterName;
    [Export] private int _hp;
    [Export] private int _atk;
    [Export] private int _speed;
    [Export] private Texture2D _texture;
    [Signal] public delegate void StartGameWithCharacterEventHandler(int atk, int speed, int hp, Texture2D texture);
    private AnimationPlayer _animationPlayer;
    private Label _nameLabel;
    private Label _descriptionLabel;

    private bool _disabled = false;

    public override void _Ready()
    {
        GetNode<TextureRect>("%TextureRect").Texture = _texture;
        GetNode<Label>("%NameLabel").Text = _characterName;   
        GetNode<Label>("%DescriptionLabel").Text = $"Attack: {_atk}\nSpeed: {_speed}\nHP: {_hp}";
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        GuiInput += HandleGuiInput;
        MouseEntered += HandleMouseEntered;
    }

    private void HandleMouseEntered()
    {
        if(_disabled)
            return;
        GetNode<AnimationPlayer>("HoverAnimationPlayer").Play(GameConstants.ANIM_CARD_HOVER);
    }

    public void PlayDiscard()
    {
        _animationPlayer.Play(GameConstants.ANIM_CARD_DISCARD);
    }

    public async void SelectCard()
    {
        _disabled = true;
        _animationPlayer.Play(GameConstants.ANIM_CARD_SELECTED);
        var charCards = Owner.GetNode<HBoxContainer>("%CardContainer").GetChildren();
        

        foreach(CharacterCard card in charCards)
        {
            if(card == this)
            {
                continue;
            }
            
            card.PlayDiscard();
        }
        await ToSignal(_animationPlayer, "animation_finished");
        
        EmitSignal(SignalName.StartGameWithCharacter, _atk, _speed, _hp, _texture);
    }

    public async void PlayIn(float delay = 0)
    {
        Modulate = Colors.Transparent;
        var timer = GetTree().CreateTimer(delay);
        await ToSignal(timer,"timeout");

        _animationPlayer.Play(GameConstants.ANIM_IN);
    }

    private void HandleGuiInput(InputEvent @event)
    {
        if(_disabled)
            return;
        if(@event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK))
        {
            SelectCard();
        }
    }
}
