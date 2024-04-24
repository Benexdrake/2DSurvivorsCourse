using Godot;
using System;

public partial class MetaMenu : CanvasLayer
{
    private MetaUpgrade[] _upgrades;
    [Export] private PackedScene _metaUpgradeCardScene;
    private GridContainer _gridContainer;
    public override void _Ready()
    {
        GetNode<Button>("%BackButton").Pressed += () => GetTree().Root.GetNode<ScreenTransition>("ScreenTransition").TransitionToScene(GameConstants.MAIN_MENU_SCENE);
        foreach (var child in GetNode<GridContainer>("%GridContainer").GetChildren())
            child.QueueFree();

        _upgrades = GetTree().Root.GetNode<MetaProgression>("MetaProgression").MetaUpgrades.ToArray();
        _gridContainer = GetNode<GridContainer>("%GridContainer");
        foreach (var upgrade in _upgrades)
        {
            var instance = _metaUpgradeCardScene.Instantiate() as MetaUpgradeCard;
            _gridContainer.AddChild(instance);   
            instance.SetMetaUpgrade(upgrade);
        }
    }
}
