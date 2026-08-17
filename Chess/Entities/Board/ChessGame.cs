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

    public ChessGame()
    {
        Board = new ChessBoard(8,8);
        Turn = 1;
        CurrentPlayer = Color.White;
        IsFinished = false;
        Pieces = new HashSet<Piece>();
        CapturedPieces = new HashSet<Piece>();
    }

    public void PlaceNewPiece(char column, int rank, Piece piece)
    {
        Board.PlacePiece(piece, new ChessPosition(column, rank).ToPosition());
        Pieces.Add(piece);
    }

    public Piece ExecuteMove(Position origin, Position destiny)
    {
        Piece piece = Board.RemovePiece(origin);
        piece.IncrementMoveCount();
        Piece capturedPiece = Board.RemovePiece(destiny);
        Board.PlacePiece(piece, destiny);
        if (capturedPiece != null)
        {
            CapturedPieces.Add(capturedPiece);
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
    }

    public void RealizePlay(Position origin, Position destiny)
    {
        Piece capturedPiece = ExecuteMove(origin, destiny);

        if (IsInCheck(CurrentPlayer))
        {
            UndoMove(origin, destiny, capturedPiece);
            throw new ChessBoardException("You cannot put yourself in check!");
        }
        if (IsInCheck(Adversary(CurrentPlayer)))
        {
            Check = true;
        }
        else
        {
            Check = false;
        }

        Turn++;
        ChangePlayer();
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
            if (possibleMoves[king.Position.Row, king.Position.Column])
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
        if (!Board.Piece(origin).CanMoveTo(destiny))
        {
            throw new ChessBoardException("Invalid destiny position!");
        }
    }


}
