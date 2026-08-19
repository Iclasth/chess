using Chess.Entities.Board;
using Chess.Entities.Enums;
namespace Chess.Entities;

public class Queen : Piece
{
    public Queen(ChessBoard board, Color color) : base(board, color)
    {
    }

    public override string ToString()
    {
        return "Q";
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

        // Up
        position.SetValues(Position.Rank - 1, Position.Column);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Rank - 1, position.Column);
        }

        // Down
        position.SetValues(Position.Rank + 1, Position.Column);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Rank + 1, position.Column);
        }

        // Left
        position.SetValues(Position.Rank, Position.Column - 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Rank, position.Column - 1);
        }
    
        // Right
        position.SetValues(Position.Rank, Position.Column + 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Rank, position.Column + 1);
        }

        // Up-Left
        position.SetValues(Position.Rank - 1, Position.Column - 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Rank - 1, position.Column - 1);
        }

        // Up-Right
        position.SetValues(Position.Rank - 1, Position.Column + 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Rank - 1, position.Column + 1);
        }

        // Down-Right
        position.SetValues(Position.Rank + 1, Position.Column + 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Rank + 1, position.Column + 1);
        }

        // Down-Left
        position.SetValues(Position.Rank + 1, Position.Column - 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Rank + 1, position.Column - 1);
        }

        return moves;
    }
}
