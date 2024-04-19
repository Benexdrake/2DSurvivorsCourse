using Godot;
using System;

public partial class GameEvents : Node
{
    public static event Action<int> ExperienceVialCollection;
    public static void EmitExperienceVialCollected(int number) => ExperienceVialCollection?.Invoke(number);
}
