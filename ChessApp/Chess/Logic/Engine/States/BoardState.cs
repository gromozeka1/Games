using Chess.Models.Pieces;

namespace Chess.Logic.Engine.States;

public class BoardState
{
    public Statement State { get; set; }
    public FigureColor? Color { get; set; }
}
