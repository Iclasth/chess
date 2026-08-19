using Chess.Entities.Board;
using Chess.Entities.Enums;

namespace Chess.Entities;

public class Pawn : Piece 
{
    private ChessGame ChessGame { get; set; }
    public Pawn(ChessBoard board, Color color, ChessGame chessGame) : base(board, color)
    {
        ChessGame = chessGame;
    }

    public override string ToString()
    {
        return "P";
    }

    private bool ExistAdversary(Position position)
    {
        Piece piece = Board.Piece(position);
        return piece != null && piece.Color != Color;
    }

    private bool IsFree(Position position)
    {
        return Board.Piece(position) == null;
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] moves = new bool[Board.Ranks, Board.Columns];

        Position position = new Position(0, 0);

        if (Color == Color.White)
        {
            position.SetValues(Position.Rank - 1, Position.Column);
            if (Board.IsValidPosition(position) && IsFree(position))
            {
                moves[position.Rank, position.Column] = true;
            }
            position.SetValues(Position.Rank - 2, Position.Column);
            Position position2 = new Position(Position.Rank - 1, Position.Column);
            if (Board.IsValidPosition(position) && IsFree(position) && Board.IsValidPosition(position2) && IsFree(position2) && MoveCount == 0)
            {
                moves[position.Rank, position.Column] = true;
            }
            position.SetValues(Position.Rank - 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && ExistAdversary(position))
            {
                moves[position.Rank, position.Column] = true;
            }
            position.SetValues(Position.Rank - 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && ExistAdversary(position))
            {
                moves[position.Rank, position.Column] = true;
            }

            // #special move en passant white
            if (Position.Rank == 3)
            {
                Position left = new Position(Position.Rank, Position.Column - 1);
                if (Board.IsValidPosition(left) && ExistAdversary(left) && Board.Piece(left) == ChessGame.EnPassantVulnerable)
                {
                    moves[left.Rank - 1, left.Column] = true;
                }
            }
            if (Position.Rank == 3)
            {
                Position right = new Position(Position.Rank, Position.Column + 1);
                if (Board.IsValidPosition(right) && ExistAdversary(right) && Board.Piece(right) == ChessGame.EnPassantVulnerable)
                {
                    moves[right.Rank - 1, right.Column] = true;
                }
            }


        }
        else
        {
            position.SetValues(Position.Rank + 1, Position.Column);
            if (Board.IsValidPosition(position) && IsFree(position))
            {
                moves[position.Rank, position.Column] = true;
            }
            position.SetValues(Position.Rank + 2, Position.Column);
            Position position2 = new Position(Position.Rank + 1, Position.Column);
            if (Board.IsValidPosition(position) && IsFree(position) && Board.IsValidPosition(position2) && IsFree(position2) && MoveCount == 0)
            {
                moves[position.Rank, position.Column] = true;
            }
            position.SetValues(Position.Rank + 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && ExistAdversary(position))
            {
                moves[position.Rank, position.Column] = true;
            }
            position.SetValues(Position.Rank + 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && ExistAdversary(position))
            {
                moves[position.Rank, position.Column] = true;
            }

             // #special move en passant black
            if (Position.Rank == 4)
            {
                Position left = new Position(Position.Rank, Position.Column - 1);
                if (Board.IsValidPosition(left) && ExistAdversary(left) && Board.Piece(left) == ChessGame.EnPassantVulnerable)
                {
                    moves[left.Rank + 1, left.Column] = true;
                }
            }
            if (Position.Rank == 4)
            {
                Position right = new Position(Position.Rank, Position.Column + 1);
                if (Board.IsValidPosition(right) && ExistAdversary(right) && Board.Piece(right) == ChessGame.EnPassantVulnerable)
                {
                    moves[right.Rank + 1, right.Column] = true;
                }
            }
        
        }

        return moves;
    }    

}
 

