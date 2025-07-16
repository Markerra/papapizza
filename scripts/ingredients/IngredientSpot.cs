using Godot;
using System;
using System.Linq;

public partial class IngredientSpot : Node
{
    [Export] public Ingredient Ingredient;
    private bool playerEntered = false;
    private bool hintShown = false;

    public override void _Ready()
    {
        GetNode<Sprite2D>("Icon").Texture = Ingredient.Icon;
    }

    public override void _Process(double delta)
    {
        if (playerEntered)
        {
            Order order = null;
            for (int i = 0; i < OrderManager.CurrentOrders.Count; i++)
            {
                if (OrderManager.CurrentOrders[i].Ingredients.GetRemainingIngredients().Any(i => i.id == Ingredient.id))
                { order = OrderManager.CurrentOrders[i]; break; }
            }

            if (order != null && order.Ingredients.IsValidOrder(Ingredient))
            {
                if (!hintShown)
                {
                    Game.UI.ShowHint($"Press {Utils.GetKeyName("Interact")} to pickup {Ingredient.Name}");
                    hintShown = true;
                }

                // using 'IsActionJustPressed' instead of 'IsActionPressed' so that the player doesn't hold the spacebar
                if (Input.IsActionJustPressed("Interact"))
                {
                    Game.UI.HideHint();
                    order.Ingredients.AddIngredient(Ingredient);
                    QueueFree();
                }
            }
            else if (hintShown)
            {
                Game.UI.HideHint();
                hintShown = false;
            }
        }

        else // if player far away from spot
        {
            if (hintShown)
            {
                Game.UI.HideHint();
                hintShown = false;
            }
        }
    }
    private void OnSpotAreaEntered(Node body)
    {
        //GD.Print($"{body.Name} Entered");
        if (body is CharacterBody2D && body.Name == "Player")
            playerEntered = true;
    }

    private void OnSpotAreaExited(Node body)
    {
        //GD.Print($"{body.Name} Exited");
        if (body is CharacterBody2D && body.Name == "Player")
            playerEntered = false;
    }
}