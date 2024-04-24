using Godot;
using System;

public partial class MetaUpgradeCard : PanelContainer
{
    private AnimationPlayer _animationPlayer;
    private Label _nameLabel;
    private Label _descriptionLabel;
    private Label _progressLabel;
    private Label _countLabel;
    private ProgressBar _progressBar;
    private MetaUpgrade _metaUpgrade;
    private Button _purchaseButton;


    [Signal] public delegate void SelectedEventHandler();

    public override void _Ready()
    {
        _nameLabel = GetNode<Label>("%NameLabel");    
        _descriptionLabel = GetNode<Label>("%DescriptionLabel");
        _progressLabel = GetNode<Label>("%ProgressLabel");
        _countLabel = GetNode<Label>("%CountLabel");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _progressBar = GetNode<ProgressBar>("%ProgressBar");
        _purchaseButton = GetNode<Button>("%PurchaseButton");
        _purchaseButton.Pressed += HandlePurchaseButtonPressed;
    }

    public async void SelectCard()
    {
        _animationPlayer.Play(GameConstants.ANIM_CARD_SELECTED);
    }

    private void UpdateProgress()
    {
        float a = GameStats.MetaUpgradeCurrency;
        float b = _metaUpgrade.ExperienceCost;

        float percent = a/b;

        bool isMax = _metaUpgrade.Quantity >= _metaUpgrade.MaxQuantity;

        _progressBar.Value = Mathf.Min(percent,1);
        _purchaseButton.Disabled = percent < 1.0 || isMax;
        if(isMax)
            _purchaseButton.Text = "Max";
        _progressLabel.Text = $"{a}/{b}";
        int q = _metaUpgrade.Quantity;
        _countLabel.Text = $"x{q}";
    }

    public void SetMetaUpgrade(MetaUpgrade upgrade)
    {
        _metaUpgrade = upgrade;
        _nameLabel.Text = upgrade.Title;
        _descriptionLabel.Text = upgrade.Description;
        UpdateProgress();
    }

    private void HandlePurchaseButtonPressed()
    {
        if(_metaUpgrade == null)
            return;
        var meta = GetTree().Root.GetNode<MetaProgression>("MetaProgression");
        meta.AddMetaUpgrade(_metaUpgrade);
        GameStats.MetaUpgradeCurrency -= _metaUpgrade.ExperienceCost;
        meta.Save();
        GetTree().CallGroup("meta_upgrade_card", "UpdateProgress");
        _animationPlayer.Play(GameConstants.ANIM_CARD_SELECTED);
    }
}
