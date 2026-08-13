using Chess.Entities.Board;
using Chess.Entities.Enums;
namespace Chess.Entities;
public class Rook : Piece
{
    public Rook(ChessBoard board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "R";
    }
}
