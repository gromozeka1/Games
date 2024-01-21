namespace ChessModels.Figures;

/// <summary>
/// Defines a rook figure.
/// </summary>
public class Rook : BaseFigure
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Queen"/> class.
    /// </summary>
    /// <param name="color">Color of the figure.</param>
    /// <param name="square">Square where the figure is in.</param>
    public Rook(Color color, Square? square = null)
        : base(color, square)
    {
        Type = Type.Rook;
    }

    /// <inheritdoc/>
    public override BaseFigure Clone(Square square)
        => new Rook(Color, square);
}