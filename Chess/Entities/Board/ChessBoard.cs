namespace Chess.Entities.Board
{
    public class ChessBoard
    {
        public int Ranks { get; protected set; }
        public int Columns { get; protected set; }

        protected Piece[,] Pieces;

        public ChessBoard(int ranks, int columns)
        {
            Ranks = ranks;
            Columns = columns;
            Pieces = new Piece[ranks, columns];
        }

        public Piece piece(int rank, int column)
        {
            return Pieces[rank, column];
        }
    }
}