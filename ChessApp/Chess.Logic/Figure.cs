namespace Chess.GameLogic;

public class ComplexFigure
{
    public Figure Figure { get; set; }

    public Color Color { get; set; }

    public Square Square { get; set; }
}

public enum Figure
{
    None = '1',

    WhiteKing = 'K',
    WhiteQueen = 'Q',
    WhiteRook = 'R',
    WhiteBishop = 'B',
    WhiteKnight = 'N',
    WhitePawn = 'P',

    BlackKing = 'k',
    BlackQueen = 'q',
    BlackRook = 'r',
    BlackBishop = 'b',
    BlackKnight = 'n',
    BlackPawn = 'p',
}

static class FigureMethods
{
    public static Color GetColor(this Figure figure) => figure switch
    {
        Figure.None => Color.None,

        Figure.WhiteKing => Color.White,
        Figure.WhiteQueen => Color.White,
        Figure.WhiteRook => Color.White,
        Figure.WhiteBishop => Color.White,
        Figure.WhiteKnight => Color.White,
        Figure.WhitePawn => Color.White,

        Figure.BlackKing => Color.Black,
        Figure.BlackQueen => Color.Black,
        Figure.BlackRook => Color.Black,
        Figure.BlackBishop => Color.Black,
        Figure.BlackKnight => Color.Black,
        Figure.BlackPawn => Color.Black,

        _ => Color.None,
    };
}
