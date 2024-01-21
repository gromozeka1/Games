namespace ChessModels.Figures;

/// <summary>
/// Defines a pawn figure.
/// </summary>
public class Pawn : BaseFigure
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pawn"/> class.
    /// </summary>
    /// <param name="color">Color of the figure.</param>
    /// <param name="square">Square where the figure is in.</param>
    public Pawn(Color color, Square? square = null)
        : base(color, square)
    {
        Type = Type.Pawn;
    }

    /// <summary>
    /// En passant status.
    /// </summary>
    public bool EnPassant { get; set; }

    /// <inheritdoc/>
    public override BaseFigure Clone(Square square)
        => new Pawn(Color, square);
}
