using Godot;
using System;
using System.Threading;
using System.Collections.Generic;

public class IngredientsList
{
    
}

public class Order
{
    public int Number;
    public IngredientsList Ingredients;
    public int Time; // initial seconds
    public float RemainingTime; // remaining seconds

    public Order(IngredientsList ingredientsList, int time)
    {
        Ingredients = ingredientsList;
        Number = OrderManager.CurrentOrders.Count;
        Time = time; RemainingTime = time;
    }

    public void Tick(float delta)
    {
        if (RemainingTime > 0) { RemainingTime -= delta; }
    }

}

public partial class OrderManager : Node
{
    public static List<Order> CurrentOrders = [];
    public static void CreateOrder(IngredientsList ingredientsList, int time)
    {
        Order order = new Order(ingredientsList, time);
        CurrentOrders.Add(order);
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < CurrentOrders.Count; i++) // for each order in orders list
        {
            CurrentOrders[i].Tick((float) delta);

            if (CurrentOrders[i].RemainingTime <= 0)
            {
                CurrentOrders.RemoveAt(i); // removes order from current orders list
            }
        }
    }
}
