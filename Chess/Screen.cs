using Chess.Entities;

namespace Chess.Screen
{
    public static class Screen
    {
        ChessBoard board = new ChessBoard(8, 8);
        public static void PrintBoard(ChessBoard board)
        {
            for (int i = 0; i < board.Ranks; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(ChessBoard.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}