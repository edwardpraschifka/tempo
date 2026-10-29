namespace Tempo.Core;

public class Evaluation
{
    public int Id { get; init; }
    public required string BestMove { get; init; }
    public int Score { get; init; }
}
