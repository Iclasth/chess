
namespace Chess.Entities.Board;

public class ChessPosition
{
    public char Column { get; protected set; }
    public int Rank { get; protected set; }

    public ChessPosition(char column, int rank)
    {
        Column = column;
        Rank = rank;
    }

    public Position ToPosition()
    {
        return new Position(8 - Rank, Column - 'a');
    }
    public override string ToString()
    {
        return $"{Column}{Rank}";
    }
}
