using Godot;

public partial class Ability : AbilityUpgrade
{
    [Export] public PackedScene AbilityControllerScene { get; private set; }
}