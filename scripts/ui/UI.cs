using Godot;
using System.Linq;
using System.Collections.Generic;

public partial class UI : CanvasLayer
{
    public void PrintOrders()
    {
        List<string> lines = new();
        string text = $"Current orders [{OrderManager.CurrentOrders.Count}]: \n";
        for (int i = 0; i < OrderManager.CurrentOrders.Count; i++)
        {
            Order order = OrderManager.CurrentOrders[i];
            lines.Add($"#{i + 1} {order.Ingredients.AddedStr} / {order.Ingredients.RecipeStr} [{(int)order.RemainingTime}s]");
        }

        text += string.Join("\n", lines);
        GetNode<Label>("Orders/Label").Text = text;
    }

    public void ShowHint(string text)
    {
        Game.Player.GetNode<Label>("PlayerHint").Text = text;
        Game.Player.GetNode<Label>("PlayerHint").Visible = true;
    }
    public void HideHint()
    {
        Game.Player.GetNode<Label>("PlayerHint").Visible = false;
    }
}
