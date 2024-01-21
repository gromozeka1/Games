namespace Chess.Models.Pieces;

/// <summary>
/// Defines a king figure.
/// </summary>
public class King : BasePiece
{
    /// <summary>
    /// Initializes a new instance of the <see cref="King"/> class.
    /// </summary>
    /// <param name="color">Color of the figure.</param>
    /// <param name="square">Square where the figure is in.</param>
    public King(FigureColor color, Square? square = null)
        : base(color, square)
    {
        Figure = FigureType.King;
    }

    /// <inheritdoc/>
    public override BasePiece Clone(Square square) => new King(Color, square);
}
