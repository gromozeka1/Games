namespace ChessModels.Figures;

/// <summary>
/// Defines a knight figure.
/// </summary>
public class Knight : BaseFigure
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Knight"/> class.
    /// </summary>
    /// <param name="color">Color of the figure.</param>
    /// <param name="square">Square where the figure is in.</param>
    public Knight(Color color, Square? square = null)
        : base(color, square)
    {
        Type = Type.Knight;
    }

    /// <inheritdoc/>
    public override BaseFigure Clone(Square square)
        => new Knight(Color, square);
}