using Chess.Screen;
using Chess.Entities.Board;
using Chess.Entities;
using Chess.Entities.Enums;
using Chess.Exceptions;

try
{
    ChessGame chessGame = new ChessGame();
    chessGame.Board.PlacePiece(new Rook(chessGame.Board, Color.White), new ChessPosition('c', 1).ToPosition());
    chessGame.Board.PlacePiece(new Rook(chessGame.Board, Color.Black), new Position(1, 4));
    chessGame.Board.PlacePiece(new King(chessGame.Board, Color.Black), new Position(0, 2));

    while (!chessGame.IsFinished)
    {
        Console.Clear();
        Screen.PrintBoard(chessGame.Board);
        Console.WriteLine();
        Console.Write("Origin: ");
        Position origin = Screen.ReadChessPosition().ToPosition();
        Console.Write("Destiny: ");
        Position destiny = Screen.ReadChessPosition().ToPosition();
        chessGame.ExecuteMove(origin, destiny);
    }
    Screen.PrintBoard(chessGame.Board);
}
catch (ChessBoardException ex)
{
    Console.WriteLine(ex.Message);
}

