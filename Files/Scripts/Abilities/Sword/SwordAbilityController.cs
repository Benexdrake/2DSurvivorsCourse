using Godot;
using System;
using System.Collections;
using System.Linq;

public partial class SwordAbilityController : Node
{
	[Export] public PackedScene SwordAbility { get; private set; }
	[Export] public int Range { get; private set; } = 150;

	[Export] public int Damage { get; set; } = 1;
	
	public override void _Ready()
	{

	}

    public override void _PhysicsProcess(double delta)
    {
        if(Input.IsActionJustPressed("attack"))
			Attack();
    }

    private void Attack()
	{
		var mousePosition = GetViewport().GetCamera2D().GetLocalMousePosition();

		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;

		if (player == null)
			return;

		var swordAbilityInstance = SwordAbility.Instantiate() as SwordAbility;

		player.GetParent().AddChild(swordAbilityInstance);

		swordAbilityInstance.HitboxComponent.Damage = Damage;

		swordAbilityInstance.GlobalPosition = mousePosition;
		swordAbilityInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * 4;
	}

    public override void _Process(double delta)
	{

	}
}
