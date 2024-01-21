namespace ChessModels.Figures;

/// <summary>
/// Defines a queen figure.
/// </summary>
public class Queen : BaseFigure
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Queen"/> class.
    /// </summary>
    /// <param name="color">Color of the figure.</param>
    /// <param name="square">Square where the figure is in.</param>
    public Queen(Color color, Square? square = null)
        : base(color, square)
    {
        Type = Type.Queen;
    }

    /// <inheritdoc/>
    public override BaseFigure Clone(Square square)
        => new Queen(Color, square);
}
