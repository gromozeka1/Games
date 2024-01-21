using System;

using Chess.Models.Pieces;

namespace Chess.Models;

/// <summary>
/// Defines a movement of the figure.
/// </summary>
public class Move
{
    /// <summary>
    /// Gets or sets initial destination.
    /// </summary>
    public Coordinate From { get; private set; }
    
    /// <summary>
    /// Gets or sets target destination.
    /// </summary>
    public Coordinate To { get; private set; }
    
    /// <summary>
    /// Gets or sets type of moving figure.
    /// </summary>
    public FigureType? Figure { get; private set; }
    
    /// <summary>
    /// Gets or sets color of moving figure.
    /// </summary>
    public FigureColor Color { get; private set; } 

    /// <summary>
    /// Gets or sets type of promotion figure.
    /// </summary>
    public FigureType? PromotePieceType { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="Move"/> class.
    /// </summary>
    /// <param name="piece">Moving figure.</param>
    /// <param name="to">Target destination.</param>
    /// <exception cref="ArgumentNullException">Thrown when moving figure or its initial destination are null.</exception>
    public Move(BasePiece? piece, Square to)
    {
        piece = piece ?? throw new ArgumentNullException(nameof(piece));
        piece.Square = piece.Square ?? throw new ArgumentNullException(nameof(piece));

        Color = piece.Color;
        Figure = piece.Figure;
        From = piece.Square.Coordinate;
        To = to.Coordinate;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Move"/> class.
    /// </summary>
    /// <param name="from">Initial destination.</param>
    /// <param name="to">Target destination.</param>
    /// <param name="figure">Type of moving figure.</param>
    /// <param name="color">Color of moving figure.</param>
    public Move(Square from, Square to, FigureType? figure, FigureColor color)
    {
        Color = color;
        Figure = figure;
        From = from.Coordinate;
        To = to.Coordinate;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Move"/> class.
    /// </summary>
    /// <param name="from">Initial destination.</param>
    /// <param name="to">Target destination.</param>
    /// <param name="figure">Type of moving figure.</param>
    /// <param name="color">Color of moving figure.</param>
    /// <param name="promotePieceType">Type of promotional figure.</param>
    public Move(Square from, Square to, FigureType? figure, FigureColor color, FigureType promotePieceType)
    {
        Color = color;
        Figure = figure;
        From = from.Coordinate;
        To = to.Coordinate;
        PromotePieceType = promotePieceType;
    }
}