using Godot;
using System;

public partial class AbilityUpgradeCard : PanelContainer
{
    private Label _nameLabel;
    private Label _descriptionLabel;

    [Signal] public delegate void SelectedEventHandler();

    public override void _Ready()
    {
        _nameLabel = GetNode<Label>("%NameLabel");    
        _descriptionLabel = GetNode<Label>("%DescriptionLabel");
        GuiInput += HandleGuiInput;
    }

    private void HandleGuiInput(InputEvent @event)
    {
        if(@event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK))
        {
            EmitSignal(SignalName.Selected);
        }
    }


    public void SetAbilityUpgrade(AbilityUpgrade upgrade)
    {
        _nameLabel.Text = upgrade.Name;
        _descriptionLabel.Text = upgrade.Description;
        EmitSignal(SignalName.Selected);
    }
}
