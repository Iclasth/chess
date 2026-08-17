using Chess.Entities.Board;
using Chess.Entities.Enums;

namespace Chess.Entities;

public class Pawn : Piece 
{
    public Pawn(ChessBoard board, Color color) : base(board, color)
    {
        
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
        }

        return moves;
    }    

}
 

