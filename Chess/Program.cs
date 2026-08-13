using Chess.Screen;
using Chess.Entities.Board;
using Chess.Exceptions;

try
{
    ChessBoard board = new ChessBoard(8, 8);
    Screen.PrintBoard(board);
}
catch (ChessBoardException ex)
{
    Console.WriteLine(ex.Message);
}
