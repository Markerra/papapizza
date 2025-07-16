using Godot;
using System.Linq;
using System.Collections.Generic;

public class IngredientsList
{
    public List<Ingredient> Recipe { get; private set; }
    public string RecipeStr { get; private set; }
    public List<Ingredient> Added { get; private set; }
    public string AddedStr { get; private set; }


    public IngredientsList(List<Ingredient> recipe)
    {
        Recipe = recipe;
        Added = new List<Ingredient>();

        foreach (var item in Recipe)
        { RecipeStr += $"{item.Name}, "; }
    }

    // checks if the ingredient can be picked up in the correct order
    public bool IsValidOrder(Ingredient ingredient)
    {
        var remaining = GetRemainingIngredients();
        return remaining.Count > 0 && remaining[0].id == ingredient.id;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (IsValidOrder(ingredient))
        {
            Added.Add(ingredient);
            AddedStr += $"{ingredient.Name} ";
        }
    }

    public bool IsCorrectOrder()
    {
        if (Added.Count > Recipe.Count) return false;
        for (int i = 0; i < Added.Count; i++)
        { // checks order of each added ingredient and initial recipe order
            if (Added[i] != Recipe[i]) return false;
        }
        return true;
    }

    public List<Ingredient> GetRemainingIngredients()
    {
        return Recipe.Skip(Added.Count).ToList();
    }
    
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

    public float GetCompletionPercent()
    {
        return (float)(Ingredients.Added.Count / Ingredients.Recipe.Count) * 100;
    }
}

public partial class OrderManager : Node
{
    public static List<Order> CurrentOrders = [];
    public static List<Order> CompletedOrders = [];
    public static void CreateOrder(string[] ingredientsIDs, int time)
    {
        List<Ingredient> ingredients = new();
        foreach (string id in ingredientsIDs) { ingredients.Add(IngredientsDatabase.Get(id)); }
        Order order = new Order(new IngredientsList(ingredients), time);
        CurrentOrders.Add(order);
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < CurrentOrders.Count; i++) // for each order in orders list
        {
            Order order = CurrentOrders[i];
            order.Tick((float) delta);

            if (order.GetCompletionPercent() >= 100)
            {
                CurrentOrders.RemoveAt(i);
                CompletedOrders.Add(order);
            }

            if (order.RemainingTime <= 0)
            {
                CurrentOrders.RemoveAt(i); // removes order from current orders list
            }
        }
    }
}
