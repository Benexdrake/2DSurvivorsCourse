using Godot;
using System;

public partial class ExperienceVial : Node2D
{
	private Area2D _area;
	private CollisionShape2D _col;
	private Sprite2D _sprite;
	public override void _Ready()
	{
        _area = GetNode<Area2D>("Area2D");
        _col = _area.GetNode<CollisionShape2D>("CollisionShape2D");
        _sprite = GetNode<Sprite2D>("Sprite2D");

		_area.AreaEntered += HandleAreaEntered;
	}

    private void HandleAreaEntered(Area2D area)
    {
        Callable.From(DisableCollision).CallDeferred();
        var tween = CreateTween();
        tween.SetParallel();
        tween.TweenMethod(Callable.From<float>(percent => TweenCollect(percent,GlobalPosition)),0.0,1.0,.5)
        .SetEase(Tween.EaseType.In)
        .SetTrans(Tween.TransitionType.Circ);

        tween.TweenProperty(_sprite,"scale", Vector2.Zero, .15).SetDelay(.35);

        tween.Chain();
        
        tween.TweenCallback(Callable.From(Collect));
    }

    private void TweenCollect(float percent, Vector2 startPosition)
    {
        if (GetTree().GetFirstNodeInGroup(GameConstants.PLAYER) is not Player player)
            return;
        
        GlobalPosition = startPosition.Lerp(player.GlobalPosition, percent);
		var directionFromStart = player.GlobalPosition - startPosition;
		
		var targetRotation = directionFromStart.Angle() + Mathf.DegToRad(90);
		Rotation = Mathf.LerpAngle(Rotation, targetRotation, 1 - (float)Mathf.Exp(-2 * GetProcessDeltaTime()));
    }

    private void Collect()
    {
        GameEvents.EmitExperienceVialCollected(1);
		QueueFree();
    }

    private void DisableCollision()
    {
        _col.Disabled = true;
    }
}
