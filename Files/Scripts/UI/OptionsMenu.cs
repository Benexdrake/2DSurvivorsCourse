using Godot;
using System;

public partial class OptionsMenu : CanvasLayer
{
    private Button _windowButton;
    private HSlider _sfxSlider;
    private HSlider _musicSlider;

    [Signal] public delegate void BackPressedEventHandler(Node node);
    public override void _Ready()
    {
        GetNode<Button>("%BackButton").Pressed += HandleBackButtonPressed;
        _windowButton = GetNode<Button>("%WindowModeButton");

        _sfxSlider = GetNode<HSlider>("%SfxSlider");
        _sfxSlider.ValueChanged += HandleSfxSliderValueChanged;

        _musicSlider =GetNode<HSlider>("%MusicSlider");
        _musicSlider.ValueChanged += HandleMusicSliderValueChanged;

        _windowButton.Text = DisplayServer.WindowMode.Fullscreen.ToString();

        _windowButton.Pressed += HandleWindowButtonPressed;

        UpdateDisplay();
    }

    private void HandleBackButtonPressed()
    {
        EmitSignal(SignalName.BackPressed, this);
    }

    private void HandleMusicSliderValueChanged(double value)
    {
        SetBusVolumePercent("music", (float)value);
    }

    private void HandleSfxSliderValueChanged(double value)
    {
        SetBusVolumePercent("sfx", (float)value);
    }

    private void HandleWindowButtonPressed()
    {
        var mode = DisplayServer.WindowGetMode();

        if(mode != DisplayServer.WindowMode.Fullscreen)
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
        else
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        }
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _windowButton.Text = "Fullscreen";
        if(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen)
            _windowButton.Text = "Windowed";
        _sfxSlider.Value = GetBusVolumePercent("sfx");
        _musicSlider.Value = GetBusVolumePercent("music");
    }

    private float GetBusVolumePercent(string busName)
    {
        var busIndex = AudioServer.GetBusIndex(busName: busName);
        var volumeDb = AudioServer.GetBusVolumeDb(busIndex);
        return Mathf.DbToLinear(volumeDb);
    }
    private void SetBusVolumePercent(string busName, float value)
    {
        var busIndex = AudioServer.GetBusIndex(busName: busName);
        var db = Mathf.LinearToDb(value);
        AudioServer.SetBusVolumeDb(busIndex, db);
    }
}
