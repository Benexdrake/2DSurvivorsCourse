using Godot;
using System;

public partial class AbilityUpgradeCard : PanelContainer
{
    private Label _nameLabel;
    private Label _descriptionLabel;
    public override void _Ready()
    {
        // Maybe VBoxContainer/NameLabel
        _nameLabel = GetNode<Label>("%NameLabel");    
        _descriptionLabel = GetNode<Label>("%DescriptionLabel");    
    }

    public void SetAbilityUpgrade(AbilityUpgrade upgrade)
    {
        _nameLabel.Text = upgrade.Name;
        _descriptionLabel.Text = upgrade.Description;
    }
}
