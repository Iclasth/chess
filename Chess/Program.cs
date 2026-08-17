using Chess.Screen;
using Chess.Entities.Board;
using Chess.Entities;
using Chess.Entities.Enums;
using Chess.Exceptions;

try
{
    ChessGame chessGame = new ChessGame();
    // --- PEÇAS BRANCAS ---

    // Peças maiores e menores (Linha 1)
    chessGame.PlaceNewPiece('a', 1, new Rook(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('b', 1, new Knight(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('c', 1, new Bishop(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('d', 1, new Queen(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('e', 1, new King(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('f', 1, new Bishop(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('g', 1, new Knight(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('h', 1, new Rook(chessGame.Board, Color.White));

    // Peões (Linha 2)
    chessGame.PlaceNewPiece('a', 2, new Pawn(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('b', 2, new Pawn(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('c', 2, new Pawn(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('d', 2, new Pawn(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('e', 2, new Pawn(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('f', 2, new Pawn(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('g', 2, new Pawn(chessGame.Board, Color.White));
    chessGame.PlaceNewPiece('h', 2, new Pawn(chessGame.Board, Color.White));


    // --- PEÇAS PRETAS ---

    // Peças maiores e menores (Linha 8)
    chessGame.PlaceNewPiece('a', 8, new Rook(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('b', 8, new Knight(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('c', 8, new Bishop(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('d', 8, new Queen(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('e', 8, new King(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('f', 8, new Bishop(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('g', 8, new Knight(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('h', 8, new Rook(chessGame.Board, Color.Black));

    // Peões (Linha 7)
    chessGame.PlaceNewPiece('a', 7, new Pawn(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('b', 7, new Pawn(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('c', 7, new Pawn(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('d', 7, new Pawn(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('e', 7, new Pawn(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('f', 7, new Pawn(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('g', 7, new Pawn(chessGame.Board, Color.Black));
    chessGame.PlaceNewPiece('h', 7, new Pawn(chessGame.Board, Color.Black));

    while (!chessGame.IsFinished)
    {

        try
        {
            Console.Clear();
            Screen.PrintGame(chessGame);

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

