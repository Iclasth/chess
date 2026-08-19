using Chess.Entities.Board;
using Chess.Entities.Enums;

namespace Chess.Entities;

public class King : Piece
{
    private ChessGame ChessGame { get; set; }
    public King(ChessBoard board, Color color, ChessGame chessGame) : base(board, color)
    {
        ChessGame = chessGame;
    }

    public override string ToString()
    {
        return "K";
    }

    public bool CanMove(Position position)
    {
        Piece piece = Board.Piece(position);
        return piece == null || piece.Color != Color;
    }

    public bool CanCastle(Position position)
    {
        Piece piece = Board.Piece(position);
        return piece != null && piece is Rook && piece.Color == Color && piece.MoveCount == 0;
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] moves = new bool[Board.Ranks, Board.Columns];

        Position position = new Position(0, 0);

        // Up
        position.SetValues(Position.Rank - 1, Position.Column);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Down
        position.SetValues(Position.Rank + 1, Position.Column);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Left
        position.SetValues(Position.Rank, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Right
        position.SetValues(Position.Rank, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Up-Left
        position.SetValues(Position.Rank - 1, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Up-Right
        position.SetValues(Position.Rank - 1, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Down-Left
        position.SetValues(Position.Rank + 1, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Down-Right
        position.SetValues(Position.Rank + 1, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            moves[position.Rank, position.Column] = true;
        }

        // Special Move: Castle king-side
        if (MoveCount == 0 && !ChessGame.Check)
        {
            Position rookPosition = new Position(Position.Rank, Position.Column + 3);
            if (CanCastle(rookPosition))
            {
                Position position1 = new Position(Position.Rank, Position.Column + 1);
                Position position2 = new Position(Position.Rank, Position.Column + 2);
                if (Board.Piece(position1) == null && Board.Piece(position2) == null)
                {
                    moves[Position.Rank, Position.Column + 2] = true;
                }
            }
        }

        // Special Move: Castle queen-side
        if (MoveCount == 0 && !ChessGame.Check)
        {
            Position rookPosition = new Position(Position.Rank, Position.Column - 4);
            if (CanCastle(rookPosition))
            {
                Position position1 = new Position(Position.Rank, Position.Column - 1);
                Position position2 = new Position(Position.Rank, Position.Column - 2);
                Position position3 = new Position(Position.Rank, Position.Column - 3);
                if (Board.Piece(position1) == null && Board.Piece(position2) == null && Board.Piece(position3) == null)
                {
                    moves[Position.Rank, Position.Column - 2] = true;
                }
            }
        }

        return moves;
    }
}
