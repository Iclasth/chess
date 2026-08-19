using Chess.Entities.Enums;
using Chess.Exceptions;
namespace Chess.Entities.Board;

public class ChessGame
{
    public ChessBoard Board { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }

    public bool IsFinished { get; private set; }

    public HashSet<Piece> Pieces { get; private set; }
    public HashSet<Piece> CapturedPieces { get; private set; }

    public bool Check { get; set; } = false;

    public Piece? EnPassantVulnerable { get; private set; }

    public ChessGame()
    {
        Board = new ChessBoard(8,8);
        Turn = 1;
        CurrentPlayer = Color.White;
        IsFinished = false;
        Pieces = new HashSet<Piece>();
        CapturedPieces = new HashSet<Piece>();
        EnPassantVulnerable = null;
    }

    public void PlaceNewPiece(char column, int rank, Piece piece)
    {
        Board.PlacePiece(piece, new ChessPosition(column, rank).ToPosition());
        Pieces.Add(piece);
    }

    public Piece ExecuteMove(Position origin, Position destiny)
    {
        // Evitar que o rei seja capturado diretamente, pois isso não é permitido no xadrez.
        if(Board.Piece(origin) is King)
        {
            throw new ChessBoardException("A King cannot be captured directly!");
        }

        Piece piece = Board.RemovePiece(origin);
        piece.IncrementMoveCount();
        Piece capturedPiece = Board.RemovePiece(destiny);
        Board.PlacePiece(piece, destiny);
        if (capturedPiece != null)
        {
            CapturedPieces.Add(capturedPiece);
        }

        // Special move: Castling kingside
        if (piece is King && destiny.Column == origin.Column + 2)
        {
            Position originRook = new Position(origin.Rank, origin.Column + 3);
            Position destinyRook = new Position(origin.Rank, origin.Column + 1);
            Piece rook = Board.RemovePiece(originRook);
            rook.IncrementMoveCount();
            Board.PlacePiece(rook, destinyRook);    
        }

        // Special move: Castling queenside
        if (piece is King && destiny.Column == origin.Column - 2)
        {
            Position originRook = new Position(origin.Rank, origin.Column - 4);
            Position destinyRook = new Position(origin.Rank, origin.Column - 1);
            Piece rook = Board.RemovePiece(originRook);
            rook.IncrementMoveCount();
            Board.PlacePiece(rook, destinyRook);    
        }

        // Special move: En passant
        if (piece is Pawn)
        {
            if (origin.Column != destiny.Column && capturedPiece == null)
            {
                Position pawnPosition;
                if (piece.Color == Color.White)
                {
                    pawnPosition = new Position(destiny.Rank + 1, destiny.Column);
                }
                else
                {
                    pawnPosition = new Position(destiny.Rank - 1, destiny.Column);
                }
                capturedPiece = Board.RemovePiece(pawnPosition);
                CapturedPieces.Add(capturedPiece);
            }
        }

        return capturedPiece;
    }

    public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
    {
        Piece piece = Board.RemovePiece(destiny);
        piece.DecrementMoveCount();
        if (capturedPiece != null)
        {
            Board.PlacePiece(capturedPiece, destiny);
            CapturedPieces.Remove(capturedPiece);
        }
        Board.PlacePiece(piece, origin);

        // Special move: Castling kingside
        if (piece is King && destiny.Column == origin.Column + 2)
        {
            Position originRook = new Position(origin.Rank, origin.Column + 3);
            Position destinyRook = new Position(origin.Rank, origin.Column + 1);
            Piece rook = Board.RemovePiece(destinyRook);
            rook.DecrementMoveCount();
            Board.PlacePiece(rook, originRook);    
        }

        // Special move: Castling queenside
        if (piece is King && destiny.Column == origin.Column - 2)
        {
            Position originRook = new Position(origin.Rank, origin.Column - 4);
            Position destinyRook = new Position(origin.Rank, origin.Column - 1);
            Piece rook = Board.RemovePiece(destinyRook);
            rook.DecrementMoveCount();
            Board.PlacePiece(rook, originRook);
        }

        // Special move: En passant
        if (piece is Pawn)
        {
            if (origin.Column != destiny.Column && capturedPiece == EnPassantVulnerable)
            {
                Piece pawn = Board.RemovePiece(destiny);
                Position pawnPosition;
                if (piece.Color == Color.White)
                {
                    pawnPosition = new Position(3, destiny.Column);
                }
                else
                {
                    pawnPosition = new Position(4, destiny.Column);
                }
                Board.PlacePiece(pawn, pawnPosition);
            }
        }
    }

    public void RealizePlay(Position origin, Position destiny)
    {
        Piece capturedPiece = ExecuteMove(origin, destiny);

        if (IsInCheck(CurrentPlayer))
        {
            UndoMove(origin, destiny, capturedPiece);
            throw new ChessBoardException("You cannot put yourself in check!");
        }

        Piece movedPiece = Board.Piece(destiny);

        // Special move: Promotion
        if (movedPiece is Pawn && (destiny.Rank == 0 || destiny.Rank == 7))
        {
            movedPiece = Board.RemovePiece(destiny);
            Pieces.Remove(movedPiece);
            Piece queen = new Queen(Board, movedPiece.Color);
            Board.PlacePiece(queen, destiny);
            Pieces.Add(queen);
        }

        if (IsInCheck(Adversary(CurrentPlayer)))
        {
            Check = true;
        }
        else
        {
            Check = false;
        }

        if (IsCheckMate(Adversary(CurrentPlayer)))
        {
            IsFinished = true;
        }
        else
        {
            Turn++;
            ChangePlayer();
        }

        

        if (movedPiece is Pawn && (destiny.Rank == origin.Rank - 2  || destiny.Rank == origin.Rank + 2))
        {
            EnPassantVulnerable = movedPiece;
        }
        else
        {
            EnPassantVulnerable = null;
        }
        
        
    }

    public bool IsCheckMate(Color color)
    {
        if (!IsInCheck(color))
        {
            return false;
        }
        foreach (Piece piece in PiecesInGameByColor(color))
        {
            bool[,] possibleMoves = piece.PossibleMoves();
            for (int i = 0; i < Board.Ranks; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        Position origin = piece.Position;
                        Position destiny = new Position(i, j);
                        Piece capturedPiece = ExecuteMove(origin, destiny);
                        bool checkTest = IsInCheck(color);
                        UndoMove(origin, destiny, capturedPiece);
                        if (!checkTest)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    public void ChangePlayer()
    {
        if (CurrentPlayer == Color.White)
        {
            CurrentPlayer = Color.Black;
        }
        else
        {
            CurrentPlayer = Color.White;
        }
    }

    public Color Adversary(Color color)
    {
        if (color == Color.White)
        {
            return Color.Black;
        }
        else
        {
            return Color.White;
        }
    }

    private Piece? King(Color color)
    {
        foreach (Piece piece in PiecesInGameByColor(color))
        {
            if (piece is King)
            {
                return piece;
            }
        }
        return null;
    }

    public bool IsInCheck(Color color)
    {
        Piece? king = King(color);
        if (king == null)
        {
            throw new ChessBoardException("There is no " + color + " king on the board!");
        }
        foreach (Piece piece in PiecesInGameByColor(Adversary(color)))
        {
            bool[,] possibleMoves = piece.PossibleMoves();
            if (possibleMoves[king.Position.Rank, king.Position.Column])
            {
                return true;
            }
        }
        return false;
    }

    public HashSet<Piece> CapturedPiecesByColor(Color color)
    {
        HashSet<Piece> aux = new HashSet<Piece>();
        foreach (Piece piece in CapturedPieces)
        {
            if (piece.Color == color)
            {
                aux.Add(piece);
            }
        }
        return aux;
    }

    public HashSet<Piece> PiecesInGameByColor(Color color)
    {
        HashSet<Piece> aux = new HashSet<Piece>();
        foreach (Piece piece in Pieces)
        {
            if (piece.Color == color)
            {
                aux.Add(piece);
            }
        }
        aux.ExceptWith(CapturedPiecesByColor(color));
        return aux;
    }

    public void ValidateOriginPosition(Position position)
    {
        Board.ValidatePosition(position);

        if (Board.Piece(position) == null)
        {
            throw new ChessBoardException("There is no piece on the chosen origin position!");
        }
        if (CurrentPlayer != Board.Piece(position).Color)
        {
            throw new ChessBoardException("The chosen piece is not yours!");
        }
        if (!Board.Piece(position).ExistsPossibleMoves())
        {
            throw new ChessBoardException("There are no possible moves for the chosen piece!");
        }
    }

    public void ValidateDestinyPosition(Position origin, Position destiny)
    {
        Board.ValidatePosition(origin);
        Board.ValidatePosition(destiny);
        if (!Board.Piece(origin).CanMoveTo(destiny))
        {
            throw new ChessBoardException("Invalid destiny position!");
        }
    }


}
