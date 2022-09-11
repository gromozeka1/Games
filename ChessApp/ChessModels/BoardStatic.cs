namespace ChessModels
{
    public static class BoardStatic
    {
        public static int[] Square;
        static BoardStatic()
        {
            Square = new int[64];

            Square[0] = Piece.White | Piece.Bishop;
            Square[63] = Piece.Black | Piece.Queen;
            Square[7] = Piece.White | Piece.Knight;
        }
    }
}
