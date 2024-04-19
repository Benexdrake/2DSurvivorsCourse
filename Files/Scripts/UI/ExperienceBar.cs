using Godot;
using System;

public partial class ExperienceBar : CanvasLayer
{
    [Export] public ExperienceManager ExperienceManager { get; private set; }
    private ProgressBar _progressBar;

    public override void _Ready()
    {
        ExperienceManager.ExperienceUpdate += HandleExperienceUpdate;
        _progressBar = GetNode<ProgressBar>("MarginContainer/ProgressBar");
        _progressBar.Value = 0;
    }

    private void HandleExperienceUpdate(float currentExperience, float targetExperience)
    {
        var percent = currentExperience / targetExperience;
        _progressBar.Value = percent;
    }

}
