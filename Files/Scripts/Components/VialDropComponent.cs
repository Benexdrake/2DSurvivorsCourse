using Godot;
using System;

public partial class VialDropComponent : Node
{
    [Export(PropertyHint.Range,"0,1,0.01")] private float _dropPercent = 1f;
    [Export] public PackedScene VialScene { get; private set; }
    [Export] private HealthComponent _healthComponent;


    public override void _Ready()
    {
        _healthComponent.Died += HandleDied;
    }

    private void HandleDied()
    {
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
