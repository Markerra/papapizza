using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
    [Export] float Speed = 500f;

    public override void _Process(double delta)
    {

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 input = new Vector2(
            Input.GetActionStrength("MoveRight") - Input.GetActionStrength("MoveLeft"),
            Input.GetActionStrength("MoveDown") - Input.GetActionStrength("MoveUp"));

        Velocity = input.Normalized() * Speed;

        MoveAndSlide();
    }

    
}
