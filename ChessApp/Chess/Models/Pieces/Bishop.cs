namespace Chess.Models.Pieces;

/// <summary>
/// Defines a bishop figure.
/// </summary>
public class Bishop : BasePiece
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Bishop"/> class.
    /// </summary>
    /// <param name="color">Color of the figure.</param>
    /// <param name="square">Square where the figure is in.</param>
    public Bishop(FigureColor color, Square? square = null)
        : base(color, square)
    {
        Figure = FigureType.Bishop;
    }

    /// <inheritdoc/>
    public override BasePiece Clone(Square square) => new Bishop(Color, square);
}
