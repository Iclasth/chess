using Chess.Exceptions;
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

        public Piece piece(Position position) => Pieces[position.Rank, position.Column];
       

        public Piece piece(int rank, int column)
        {
            return Pieces[rank, column];
        }

        public void ValidatePosition(Position position)
        {
            if (!IsValidPosition(position))
            {
                throw new ChessBoardException("Invalid position!");
            }
        }

        public bool IsTherePiece(Position position)
        {
            ValidatePosition(position);
            return piece(position) != null;
        }

        public bool IsValidPosition(Position position)
        {
            if (position.Rank < 0 || position.Rank >= Ranks || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }
        public void PlacePiece(Piece piece, Position position)
        {
            if (IsTherePiece(position))
            {
                throw new ChessBoardException("There is already a piece on this position!");
            }
            
            Pieces[position.Rank, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if(piece(position) == null) return null;
            Piece aux = piece(position);
            aux.Position = null;
            Pieces[position.Rank, position.Column] = null;
            return aux;
            
        }


    }
}