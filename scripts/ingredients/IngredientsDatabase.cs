using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public static class IngredientsDatabase
{
    private const string DIR = "res://resources/ingredients/";
    public static Dictionary<string, Ingredient> Ingredients = new();

    // initialaize and add every ingredient to the database
    public static void Init()
    {
        GD.Print("Ingredients Db initialization");
        foreach (var file in DirAccess.Open(DIR).GetFiles())
        {
            if (file.EndsWith(".tres"))
            {
                var ingredient = ResourceLoader.Load<Ingredient>(DIR + file);
                if (ingredient != null) { Ingredients[ingredient.id] = ingredient; }
            }
        }
    }

    public static Ingredient Get(string id) { return Ingredients[id]; }
}