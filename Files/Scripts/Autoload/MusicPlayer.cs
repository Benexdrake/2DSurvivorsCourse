using Godot;
using System;

public partial class MusicPlayer : AudioStreamPlayer
{
    public override void _Ready()
    {
        Finished += HandleFinished;
        GetNode<Timer>("Timer").Timeout += HandleTimeout;
    }
    private void HandleTimeout()
    {
        Play();
    }
    private void HandleFinished()
    {
        GetNode<Timer>("Timer").Start();
    }
}
