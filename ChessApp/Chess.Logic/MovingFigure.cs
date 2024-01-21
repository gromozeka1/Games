namespace Chess.GameLogic;

public class MovingFigure
{
    public int DeltaX => To.X - From.X;
    public int DeltaY => To.Y - From.Y;

    public int AbsDeltaX => Math.Abs(DeltaX);
    public int AbsDeltaY => Math.Abs(DeltaY);

    public int SignX => Math.Sign(DeltaX);
    public int SignY => Math.Sign(DeltaY);

    public Figure Figure { get; private set; }

    public Square From { get; private set; }
    
    public Square To { get; private set; }
    
    public Figure Promotion { get; private set; }

    public MovingFigure(FigureOnSquare figureOnSquare, Square to, Figure promotion = Figure.None)
    {
        Figure = figureOnSquare.Figure;
        From = figureOnSquare.Square;
        To = to;
        Promotion = promotion;
    }

    public MovingFigure(string move) // Pe2e4   Pe7e8Q
    {
        Figure = GetFigure(move[0]);
        From = GetSquare(move.Substring(1, 2));
        To = GetSquare(move.Substring(3, 2));
        Promotion = move.Length == 6 ? (Figure)move[5] : Figure.None;
    }

    private static Figure GetFigure(char figure) => (Figure)figure;

    private static Square GetSquare(string square) => new(square);

    public override string ToString()
        => (char)Figure
        + From.Name
        + To.Name
        + (Promotion == Figure.None ? "" : (char)Promotion);
}