using Chess.Logic.Engine.Rules;
using Chess.Logic.Engine.Rules.Movements;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.RuleManager;

public class QueenRuleGroup : RuleGroup
{
    public QueenRuleGroup()
    {
        Rules.Add(new QueenMoves());
        Rules.Add(new CanOnlyTakeEnemy());
        Rules.Add(new IsNotBeChecked());
    }

    protected override FigureType? Figure => FigureType.Queen;
}
