using Chess.Screen;
using Chess.Entities.Board;
using Chess.Entities;
using Chess.Entities.Enums;
using Chess.Exceptions;

try
{
    ChessBoard board = new ChessBoard(8, 8);
    board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));
    board.PlacePiece(new Rook(board, Color.Black), new Position(1, 4));
    board.PlacePiece(new King(board, Color.Black), new Position(0, 2));
    Screen.PrintBoard(board);
}
catch (ChessBoardException ex)
{
    Console.WriteLine(ex.Message);
}
