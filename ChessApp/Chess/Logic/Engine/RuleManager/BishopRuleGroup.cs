using Chess.Logic.Engine.Rules;
using Chess.Logic.Engine.Rules.Movements;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.RuleManager
{
    public class BishopRuleGroup : RuleGroup
    {
        public BishopRuleGroup()
        {
            Rules.Add(new CanOnlyTakeEnemy());
            Rules.Add(new BishopMoves());
            Rules.Add(new IsNotBeChecked());
        }

        protected override FigureType? Figure => FigureType.Bishop;
    }
}
