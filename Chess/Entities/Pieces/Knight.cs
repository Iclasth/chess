using Chess.Entities.Board;
using Chess.Entities.Enums;

namespace Chess.Entities;

public class Knight : Piece
{
    public Knight(ChessBoard board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "N";
    }

    public bool CanMove(Position position)
    {
        Piece piece = Board.Piece(position);
        return piece == null || piece.Color != Color;
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] moves = new bool[Board.Ranks, Board.Columns];

        Position position = new Position(0, 0);

        // Up-Left
        position.SetValues(Position.Rank - 2, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Up-Right
        position.SetValues(Position.Rank - 2, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Down-Right
        position.SetValues(Position.Rank + 2, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Down-Left
        position.SetValues(Position.Rank + 2, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        return moves;
    }
}
