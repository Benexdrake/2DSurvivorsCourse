using Godot;
using System;

public partial class MetaUpgrade : Resource
{
    [Export] public string Id { get; set; }
    [Export] public int ExperienceCost { get; set; }
    [Export] public int MaxQuantity { get; set; }
    [Export] public int Quantity { get; set; }
    [Export] public string Title { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
    
}
