namespace Chess.Entities.Board
{
    public class Position
    {
        public int Rank { get; set; }
        public int Column { get; set; }

        public Position(int rank, int column)
        {
            Rank = rank;
            Column = column;
        }

        public override string ToString()
        {
            return $"({Rank}, {Column})";
        }
    }
}