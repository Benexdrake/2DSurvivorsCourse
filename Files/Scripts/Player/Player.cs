using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export(PropertyHint.Range,"0,200,0.5")] public int Speed { get; private set; } = 5;
    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        var movementVector = GetMovementVector();
        var direction = movementVector.Normalized();
        Velocity = direction * Speed;
        MoveAndSlide();
    }

    private Vector2 GetMovementVector()
    {
        var xMovement = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        var yMovement = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

        return new Vector2(xMovement, yMovement);
    }
}
