using Chess.Logic.Engine.Rules;
using Chess.Logic.Engine.Rules.Movements;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.RuleManager
{
    public class KnightRuleGroup : RuleGroup
    {
        public KnightRuleGroup()
        {
            Rules.Add(new CanOnlyTakeEnemy());
            Rules.Add(new KnightMoves());
            Rules.Add(new IsNotBeChecked());
        }

        protected override FigureType? Figure => FigureType.Knight;
    }
}
