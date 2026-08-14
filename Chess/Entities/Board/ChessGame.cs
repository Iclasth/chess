using Chess.Entities.Enums;
namespace Chess.Entities.Board;

public class ChessGame
{
    public ChessBoard Board { get; set; }
    public int Turn { get; set; }
    public Color CurrentPlayer { get; set; }

    public bool IsFinished { get; set; }

    public ChessGame()
    {
        Board = new ChessBoard(8,8);
        Turn = 1;
        CurrentPlayer = Color.White;
        IsFinished = false;
    }

    public void ExecuteMove(Position origin, Position destiny)
    {
        Piece piece = Board.RemovePiece(origin);
        piece.IncrementMoveCount();
        Piece capturedPiece = Board.RemovePiece(destiny);
        Board.PlacePiece(piece, destiny);
    }


}
