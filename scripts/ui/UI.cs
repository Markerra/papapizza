using Godot;
using System.Linq;
using System.Collections.Generic;

public partial class UI : CanvasLayer
{
    public void PrintIngredients()
    {
        List<string> lines = new();
        string text = "Ингредиенты: \n";

        foreach (var ingredient in IngredientsDatabase.Ingredients.Values)
        {
            lines.Add(ingredient.Name);
        }

        text += string.Join("\n", lines);
        GetNode<Label>("IngredientsDatabase/Label").Text = text;
    }
}
