namespace Tempo.Core;

public enum GameStatus {
    Ongoing = 0,
    Complete = 1,
}

public enum GameOutcome {
    Draw = 0,
    ComputerWin = 1,
    HumanWin = 2,
}

public enum PieceColor {
    White = 0,
    Black = 1,
}

public class Game
{
    public int Id { get; init; }
    public int BotElo { get; init; }
    public GameStatus Status { get; set; }
    public GameOutcome? Outcome { get; set; }
    public PieceColor HumanColor { get; init; }
}