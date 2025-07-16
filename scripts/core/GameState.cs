using Godot;

public enum GameStateEnum
{
    None,
    MainMenu,
    Active,
    Paused
}

public partial class GameState : Node
{
    public GameStateEnum CurrentState { get; private set; } = GameStateEnum.None;

    public void Set(GameStateEnum state)
    {
        if (CurrentState == state) return;

        CurrentState = state; // update current state
        Game.MainTree.Paused = false; // unpause game on every state change

        GD.Print($"Game state updated: {CurrentState}");
        
        switch (state)
        {

            case GameStateEnum.Active:
                IngredientsDatabase.Init();
                Game.Manager.Init();
                break;

            case GameStateEnum.Paused:
                Game.MainTree.Paused = true;
                break;

        }
        
    }
}
