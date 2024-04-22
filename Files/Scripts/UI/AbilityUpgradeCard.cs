using Godot;
using System;

public partial class AbilityUpgradeCard : PanelContainer
{
    private AnimationPlayer _animationPlayer;
    private Label _nameLabel;
    private Label _descriptionLabel;

    private bool _disabled = false;

    [Signal] public delegate void SelectedEventHandler();

    public override void _Ready()
    {
        _nameLabel = GetNode<Label>("%NameLabel");    
        _descriptionLabel = GetNode<Label>("%DescriptionLabel");
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

        foreach(AbilityUpgradeCard card in GetTree().GetNodesInGroup(GameConstants.GROUP_UPGRADE_CARD))
        {
            if(card == this)
            {
                GD.Print("TEST");
                continue;
            }
            
            card.PlayDiscard();
        }
        await ToSignal(_animationPlayer, "animation_finished");
        EmitSignal(SignalName.Selected);
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

    public void SetAbilityUpgrade(AbilityUpgrade upgrade)
    {
        _nameLabel.Text = upgrade.Name;
        _descriptionLabel.Text = upgrade.Description;
        EmitSignal(SignalName.Selected);
    }
}
