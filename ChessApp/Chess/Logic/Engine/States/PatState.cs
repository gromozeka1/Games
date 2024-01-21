using System.Collections.Generic;
using System.Linq;

using Chess.Logic.Engine.RuleManager;
using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.States;

internal class PatState : IState
{
    public bool IsInState(Board board, FigureColor color)
    {
        RuleGroup ruleGroup = new PawnRuleGroup();
        ruleGroup.AddGroup(new BishopRuleGroup());
        ruleGroup.AddGroup(new KingRuleGroup());
        ruleGroup.AddGroup(new KnightRuleGroup());
        ruleGroup.AddGroup(new QueenRuleGroup());
        ruleGroup.AddGroup(new RookRuleGroup());

        List<Square> possibleSquares = new List<Square>();
        foreach (Square square in board.Squares.OfType<Square>())
        {
            if (square.Piece is not null && square.Piece.Color == color)
            {
                possibleSquares = possibleSquares.Concat(ruleGroup.PossibleMoves(square.Piece)).ToList();
            }
        }

        return possibleSquares.Count == 0;
    }

    public string Explain() => "It's a pat...";
}
