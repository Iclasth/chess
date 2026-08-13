using Chess.Entities.Board;

namespace Chess.Screen
{
    public static class Screen
    {
        
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
                        Console.Write(board.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}