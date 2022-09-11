namespace Chess.GameLogic
{
    public enum Color
    {
        None,
        White,
        Black,
    }

    static class ColorMethods
    {
        public static Color FlipColor(this Color color) => color switch
        {
            Color.Black => Color.White,
            Color.White => Color.Black,
            _ => Color.None,
        };
    }
}
