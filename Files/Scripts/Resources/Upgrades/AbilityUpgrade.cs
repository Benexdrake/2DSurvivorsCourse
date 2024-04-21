using Godot;

public partial class AbilityUpgrade : Resource
{
    [Export] public string Id { get; set; }
    [Export] public string Name { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
    [Export] public int MaxQuantity { get; set; }
    public int Quantity { get; set; }
    public int Weight { get; set; }
}