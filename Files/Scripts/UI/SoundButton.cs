using Godot;
using System;

public partial class SoundButton : Button
{
    public override void _Ready()
    {
        Pressed += HandlePressed;
    }

    private void HandlePressed()
    {
        GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
    }

}
