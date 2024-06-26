using Godot;
using System;

public partial class HealthComponent : Node
{
    [Export] public int MaxHealth { get; set; }
    private int _getDamage;
    public int CurrentHealth { get; set; }

    [Signal] public delegate void DiedEventHandler();
    [Signal] public delegate void HealthChangedEventHandler();

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(int damage)
    {
        _getDamage += damage;
        CurrentHealth = MaxHealth- _getDamage;
        //CurrentHealth = Mathf.Clamp(CurrentHealth - damage,0,MaxHealth);
        EmitSignal(SignalName.HealthChanged);
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

    public int GetHealthPercent()
    {
        if(MaxHealth <= 0)
            return 0;

        return MaxHealth;
    }
}
