using Godot;
using System;

public partial class ScreenTransition : CanvasLayer
{
    [Signal] public delegate void TransitionedHalfwayEventHandler();
    private bool _skipEmit = false;

    public async void Transition()
    {
        var ap = GetNode<AnimationPlayer>("AnimationPlayer");
        ap.Play(GameConstants.ANIM_IN);
        await ToSignal(ap, "animation_finished");
        _skipEmit = true;
        ap.PlayBackwards(GameConstants.ANIM_IN);
    }

    public async void TransitionToScene(string scenePath, bool unPause = false)
    {
        Transition();
        await ToSignal(this, "TransitionedHalfway");
        if (unPause)
        {
            GetTree().Paused = false;
        }
        GetTree().ChangeSceneToFile(scenePath);
    }

    public void EmitTransitionHalfway()
    {
        if(_skipEmit)
        {
            _skipEmit = false;
            return;
        }
        EmitSignal(SignalName.TransitionedHalfway);
    }
}
