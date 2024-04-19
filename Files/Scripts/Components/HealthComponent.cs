using Godot;
using System;

public partial class HealthComponent : Node
{
    [Export] public int MaxHealth { get; private set; } = 2;
    public int CurrentHealth { get; private set; }

    [Signal] public delegate void DiedEventHandler();

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage,0,MaxHealth);

        Callable.From(CheckDeath).CallDeferred();
    }

    private void CheckDeath()
    {
        if(CurrentHealth <= 0)
        {
            EmitSignal(SignalName.Died);
            Owner.QueueFree();
        }
    }
}
