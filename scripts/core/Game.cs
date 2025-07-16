using Godot;
using System;

public partial class Game : Node2D
{
    public static GameState State { get; private set; } = new GameState();
    public static GameManager Manager { get; private set; } = new GameManager();
    [Export] public UI UI { get; private set; }
    public static SceneTree MainTree { get; private set; }

    public override void _Ready()
    {
        MainTree = GetTree();

        State.Set(GameStateEnum.Active);
    }

    public override void _Process(double delta)
    {
        UI.PrintIngredients();
    }


}