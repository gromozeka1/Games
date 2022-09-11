using Chess.Logic.Engine.Rules;
using Chess.Logic.Engine.Rules.Movements;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.RuleManager
{
    public class KingRuleGroup : RuleGroup
    {
        public KingRuleGroup()
        {
            Rules.Add(new KingMoves());
            Rules.Add(new CanOnlyTakeEnemyRuleKing());
            Rules.Add(new Castling());
            Rules.Add(new IsNotBeChecked());
        }

        protected override FigureType? Figure => FigureType.King;
    }
}
