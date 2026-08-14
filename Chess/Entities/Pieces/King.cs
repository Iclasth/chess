using Chess.Entities.Board;
using Chess.Entities.Enums;

namespace Chess.Entities;

public class King : Piece
{
    public King(ChessBoard board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "K";
    }
}
