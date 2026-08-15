using Chess.Screen;
using Chess.Entities.Board;
using Chess.Entities;
using Chess.Entities.Enums;
using Chess.Exceptions;

try
{
    ChessGame chessGame = new ChessGame();
    chessGame.PlaceNewPiece('c', 1, new Rook(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('c', 8, new Rook(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('e', 8, new King(chessGame.Board, Color.Black));

    while (!chessGame.IsFinished)
    {

        try
        {
            Console.Clear();
            Screen.PrintBoard(chessGame.Board);
            Console.WriteLine();

            Console.WriteLine("Turn: " + chessGame.Turn);
            Console.WriteLine("Current Player: " + chessGame.CurrentPlayer);

            Console.WriteLine();
            Console.Write("Origin: ");
            Position origin = Screen.ReadChessPosition().ToPosition();
            chessGame.ValidateOriginPosition(origin);

            bool[,] possibleMoves = chessGame.Board.Piece(origin).PossibleMoves();
            Console.Clear();

            Screen.PrintBoard(chessGame.Board, possibleMoves);
            Console.WriteLine();

            Console.Write("Destiny: ");
            Position destiny = Screen.ReadChessPosition().ToPosition();
            chessGame.ValidateDestinyPosition(origin, destiny);
            chessGame.RealizePlay(origin, destiny);
        }
        catch (ChessBoardException ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }
    //Screen.PrintBoard(chessGame.Board);
}
catch (ChessBoardException ex)
{
    Console.WriteLine(ex.Message);
}

