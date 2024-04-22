using Godot;
using System;
using System.Collections.Generic;

public partial class GameEvents : Node
{
    public static event Action<int> ExperienceVialCollection;
    public static event Action<AbilityUpgrade,List<AbilityUpgrade>> AbilityUpgradeAdded;
    public static event Action PlayerDamaged;

    public delegate void PlayerDamagedEventHandler();

    public static void EmitExperienceVialCollected(int number) => ExperienceVialCollection?.Invoke(number);
    public static void EmitAbilityUpgradeAdded(AbilityUpgrade upgrade, List<AbilityUpgrade> currentUpgrades) => AbilityUpgradeAdded?.Invoke(upgrade, currentUpgrades);

    public static void EmitPlayerDamaged() => PlayerDamaged?.Invoke();
}
