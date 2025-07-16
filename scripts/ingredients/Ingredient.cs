using Godot;
using System;

public partial class Ingredient : Resource
{
    [Export] public string id = "unnamed";
    [Export] public string Name = "Unnamed";
    [Export] public Texture2D Icon;

}