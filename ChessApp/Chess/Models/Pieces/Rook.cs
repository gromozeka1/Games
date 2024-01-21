namespace Chess.Models.Pieces;

/// <summary>
/// Defines a rook figure.
/// </summary>
public class Rook : BasePiece
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Queen"/> class.
    /// </summary>
    /// <param name="color">Color of the figure.</param>
    /// <param name="square">Square where the figure is in.</param>
    public Rook(FigureColor color, Square? square = null)
        : base(color, square)
    {
        Figure = FigureType.Rook;
    }

    /// <inheritdoc/>
    public override BasePiece Clone(Square square) => new Rook(Color, square);
}
