using Godot;
using System;

public partial class Game : Node2D
{
    public static GameState State { get; private set; } = new GameState();
    public static GameManager Manager { get; private set; } = new GameManager();
    public static SceneTree MainTree { get; private set; }
    public static CharacterBody2D Player { get; private set; }
    public static UI UI { get; private set; }

    public override void _Ready()
    {
        MainTree = GetTree();

        Player = GetTree().Root.GetNode<CharacterBody2D>("GameRoot/Player");
        UI = GetTree().Root.GetNode<UI>("GameRoot/UI");

        State.Set(GameStateEnum.Active);
        OrderManager.CreateOrder(["cheese"], 55);
        OrderManager.CreateOrder(["cheese"], 15);
        OrderManager.CreateOrder(["cheese"], 22);
        OrderManager.CreateOrder(["cheese"], 33);
        OrderManager.CreateOrder(["tomato", "cheese"], 55);
    }

    public override void _Process(double delta)
    {
        UI.PrintOrders();
    }


}