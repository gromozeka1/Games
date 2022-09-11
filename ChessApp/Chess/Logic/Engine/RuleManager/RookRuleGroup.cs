using Chess.Logic.Engine.Rules;
using Chess.Logic.Engine.Rules.Movements;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.RuleManager
{
    public class RookRuleGroup : RuleGroup
    {
        public RookRuleGroup()
        {
            Rules.Add(new RookMoves());
            Rules.Add(new CanOnlyTakeEnemy());
            Rules.Add(new IsNotBeChecked());
        }

        protected override FigureType? Figure => FigureType.Rook;
    }
}
