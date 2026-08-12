namespace Chess.Entities.Board
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }

        public Piece(Position position, Color color)
        {
            Position = position;
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
    }
}