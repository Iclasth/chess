using Chess.Entities.Enums;
namespace Chess.Entities.Board
{
    public abstract class Piece
    {
        public ChessBoard Board { get; protected set; }
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }

        public Piece(ChessBoard board, Color color)
        {
            Board = board;
            Position = null;
            Color = color;
            MoveCount = 0;
        }

        public void IncrementMoveCount()
        {
            MoveCount++;
        }

        public void DecrementMoveCount()
        {
            MoveCount--;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMoves()[position.Rank, position.Column];
        }
        public abstract bool[,] PossibleMoves();

        public bool ExistsPossibleMoves()
        {
            bool[,] moves = PossibleMoves();
            for (int i = 0; i < Board.Ranks; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (moves[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}