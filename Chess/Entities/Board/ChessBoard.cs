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

        public ChessBoard(Position position) => Pieces[position.Rank, position.Column];
       

        public Piece piece(int rank, int column)
        {
            return Pieces[rank, column];
        }

        public void PlacePiece(Piece piece, Position position)
        {
            Pieces[position.Rank, position.Column] = piece;
            piece.Position = position;
        }
    }
}