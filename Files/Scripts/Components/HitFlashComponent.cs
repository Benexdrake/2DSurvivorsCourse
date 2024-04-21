using Godot;
using System;

public partial class HitFlashComponent : Node
{
    private HealthComponent _healthComponent;
    private Sprite2D _sprite;

    [Export] private ShaderMaterial _flashMaterial;

    private Tween _hitFlashTween;

    public override void _Ready()
    {
        _healthComponent = Owner.GetNode<HealthComponent>("HealthComponent");
        _sprite = Owner.GetNode<Sprite2D>("Visuals/Sprite2D");
        _sprite.Material = _flashMaterial;
        _healthComponent.HealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged()
    {
        if(_hitFlashTween != null && _hitFlashTween.IsValid())
            _hitFlashTween.Kill();
        (_sprite.Material as ShaderMaterial).SetShaderParameter("lerp_percent",1.0);
        
        _hitFlashTween = CreateTween();
        _hitFlashTween.TweenProperty(_sprite.Material, "shader_parameter/lerp_percent",0.0, .5)
        .SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Cubic);
    }
}
