namespace Chess.Models.Pieces;

/// <summary>
/// Defines a knight figure.
/// </summary>
public class Knight : BasePiece
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Knight"/> class.
    /// </summary>
    /// <param name="color">Color of the figure.</param>
    /// <param name="square">Square where the figure is in.</param>
    public Knight(FigureColor color, Square? square = null)
        : base(color, square)
    {
        Figure = FigureType.Knight;
    }

    /// <inheritdoc/>
    public override BasePiece Clone(Square square) => new Knight(Color, square);
}
