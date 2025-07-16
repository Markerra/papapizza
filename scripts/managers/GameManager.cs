using Godot;

public partial class GameManager: Node
{
    public int Seed;

    public void Init()
    {
        GD.Print("Game Manager initialization");

        // Random system initialization
        Seed = GD.RandRange(0, 99999999);
    }

}
