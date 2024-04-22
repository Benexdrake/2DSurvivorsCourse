using Godot;
using System;

public partial class Vignette : CanvasLayer
{
    public override void _Ready()
    {
        GameEvents.PlayerDamaged += HandlePlayerDamaged;
    }

    private void HandlePlayerDamaged()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play(GameConstants.ANIM_HIT);
    }

}
