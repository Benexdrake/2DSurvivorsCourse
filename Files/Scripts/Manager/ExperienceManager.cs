using Godot;
using System;

public partial class ExperienceManager : Node
{
	public int CurrentExperience { get; set; } = 0;
	public int CurrentLevel { get; set; } = 1;
	public int TargetExperience { get; set; } = 5;
  [Export] private int _targetExperienceGrowth = 5;

  [Signal] public delegate void ExperienceUpdateEventHandler(float currentExperience, float targetExperience);
  [Signal] public delegate void LevelUpEventHandler(int newLevel);


  public override void _Ready()
  {
      GameEvents.ExperienceVialCollection += HandleExperienceVialCollection;
  }

    public override void _ExitTree()
    {
        GameEvents.ExperienceVialCollection -= HandleExperienceVialCollection;
    }

    private void HandleExperienceVialCollection(int number)
  {
		IncrementExperience(number);
  }
  public void IncrementExperience(int number)
	{

		CurrentExperience = Mathf.Min(CurrentExperience + number, TargetExperience);
		EmitSignal(SignalName.ExperienceUpdate, CurrentExperience, TargetExperience);

    if(CurrentExperience == TargetExperience)
    {
      CurrentLevel++;
      TargetExperience += _targetExperienceGrowth;
      CurrentExperience = 0;
		  EmitSignal(SignalName.ExperienceUpdate, CurrentExperience, TargetExperience);
		  EmitSignal(SignalName.LevelUp, CurrentLevel);
    }
	}
}
