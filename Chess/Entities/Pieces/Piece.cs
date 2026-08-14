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

        public abstract bool[,] PossibleMoves();
    }
}