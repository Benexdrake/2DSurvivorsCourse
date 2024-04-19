using Godot;
using System;

public partial class SwordAbility : Node2D
{
    public HitboxComponent HitboxComponent { get; private set; }

    public override void _Ready()
    {
        HitboxComponent = GetNode<HitboxComponent>("HitboxComponent");
    }
}
