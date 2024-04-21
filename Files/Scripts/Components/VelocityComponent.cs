using Godot;
using System;

public partial class VelocityComponent : Node
{
    
    private int MaxSpeed = 40;
    private float Acceleration = 5;
    
    private Vector2 _velocity = Vector2.Zero;
    private Sprite2D _sprite2D;
    private bool _flip;

    public void Ready()
    {
        _sprite2D = Owner.GetNode<Sprite2D>("Visuals/Sprite2D");
        var owner = Owner as Enemy;

        MaxSpeed = owner.MaxSpeed;
        Acceleration = owner.Acceleration;

        _flip = !_sprite2D.FlipH;
    }

    public void AccelerateToPlayer()
    {
        if (Owner is not Node2D ownerNode2D)
            return;

    
        if (GetTree().GetFirstNodeInGroup(GameConstants.PLAYER) is not Player player)
            return;


        var direction = (player.GlobalPosition - ownerNode2D.GlobalPosition).Normalized();
        AccelerateInDirection(direction);
    }

    private void AccelerateInDirection(Vector2 direction)
    {
        var desiredVelocity = direction * MaxSpeed;
        _velocity = _velocity.Lerp(desiredVelocity, 1- Mathf.Exp(-Acceleration * (float)GetProcessDeltaTime()));
    }

    public void Decelerate()
    {
        AccelerateInDirection(Vector2.Zero);
    }

    public void Move(CharacterBody2D characterBody2D)
    {
        characterBody2D.Velocity = _velocity;

        if(_velocity.X > 0)
			_sprite2D.FlipH = !_flip;
		else
			_sprite2D.FlipH = _flip;

        characterBody2D.MoveAndSlide();
        _velocity = characterBody2D.Velocity;
    }
}
