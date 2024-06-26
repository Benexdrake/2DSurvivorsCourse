using Godot;
using System;
using System.Linq;

public partial class VialDropComponent : Node
{
    [Export(PropertyHint.Range,"0,1,0.01")] private float _dropPercent = .5f;
    [Export] public PackedScene VialScene { get; private set; }
    private HealthComponent _healthComponent;


    public override void _Ready()
    {
        _healthComponent = Owner.GetNode<HealthComponent>("HealthComponent");
        _healthComponent.Died += HandleDied;
    }

    private void HandleDied()
    {
        var meta = GetTree().Root.GetNode<MetaProgression>("MetaProgression");
        
        var quantity = meta.GetUpgradeCount(GameConstants.META_VIAL_DROP);
        if(quantity >0)
            _dropPercent += .1f;

        if(GD.Randf() > _dropPercent)
            return;

        if(VialScene == null)
            return;

        if(Owner is not Node2D)
            return;
        var owner = Owner as Node2D;
        var spawnPosition = owner.GlobalPosition;

        var vialInstance = VialScene.Instantiate() as Node2D;

        var entitiesLayer = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_ENTITIES_LAYER);

		entitiesLayer.AddChild(vialInstance);

        vialInstance.GlobalPosition = spawnPosition;
    }
}
