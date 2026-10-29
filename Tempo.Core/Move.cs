namespace Tempo.Core;

public class Move
{
    public int Id { get; init; }
    public int GameId { get; init; }
    public Game Game { get; init; } = null!;
    public int MoveNumber { get; init; }
    public required string AlgNotation { get; init; }
    public int? EvaluationId { get; set; }
    public Evaluation? Evaluation { get; set; }
}
