using Godot;
using System;

public partial class DeathComponent : Node2D
{
    private HealthComponent _healthComponent;
    private Sprite2D _sprite;
    private AnimationPlayer _animationPlayer;
    private GpuParticles2D _gpuParticle2D;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _gpuParticle2D = GetNode<GpuParticles2D>("GPUParticles2D");

        _healthComponent = Owner.GetNode<HealthComponent>("HealthComponent");

        _healthComponent.Died += HandleDied;
    }

    private void HandleDied()
    {
        if (Owner is not Node2D owner)
            return;
        _sprite = owner.GetNode<Sprite2D>("Visuals/Sprite2D");
        _gpuParticle2D.Texture = _sprite.Texture;

        if(Owner == null)
            return;
        var spawnPosition = owner.GlobalPosition;
        var entities = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_ENTITIES_LAYER);
        GetParent().RemoveChild(this);
        entities.AddChild(this);
        GlobalPosition = spawnPosition;
        _animationPlayer.Play(GameConstants.ANIM_DEATH);
    }

}
