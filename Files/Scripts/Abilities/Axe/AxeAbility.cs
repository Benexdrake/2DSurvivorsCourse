using Godot;
using System;

public partial class AxeAbility : Node2D
{
    public HitboxComponent HitboxComponent { get; private set; }

    private Vector2 _baseRotation = Vector2.Right;
    public override void _Ready()
    {
        _baseRotation = Vector2.Right.Rotated((float)GD.RandRange(0,Mathf.Tau));

        HitboxComponent = GetNode<HitboxComponent>("HitboxComponent");
        var tween = CreateTween();
        tween.TweenMethod(Callable.From<float>(RotateAxeTweenMethod),0.0,3.0,5);
        tween.TweenCallback(Callable.From(QueueFree));
    }

    private void RotateAxeTweenMethod(float rotations)
    {
        var currentRadius = rotations / 2 * GameConstants.SKILL_AXE_RADIUS;
        var currentDirection = _baseRotation.Rotated(rotations * Mathf.Tau);

        if (GetTree().GetFirstNodeInGroup(GameConstants.PLAYER) is not Player player)
            return;

        GlobalPosition = player.GlobalPosition + (currentDirection * currentRadius);
    }
}
