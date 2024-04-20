using Godot;
using System;

public partial class DeathComponent : Node2D
{
    [Export] private HealthComponent _healthComponent;
    [Export] private Sprite2D _sprite;
    private AnimationPlayer _animationPlayer;
    private GpuParticles2D _gpuParticle2D;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _gpuParticle2D = GetNode<GpuParticles2D>("GPUParticles2D");

        _gpuParticle2D.Texture = _sprite.Texture;

        _healthComponent.Died += HandleDied;
    }

    private void HandleDied()
    {
        var owner = Owner as Node2D;
        var spawnPosition = owner.GlobalPosition;
        var entities = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_ENTITIES_LAYER);
        GetParent().RemoveChild(this);
        entities.AddChild(this);
        GlobalPosition = spawnPosition;
        _animationPlayer.Play(GameConstants.ANIM_DEATH);
    }

}
