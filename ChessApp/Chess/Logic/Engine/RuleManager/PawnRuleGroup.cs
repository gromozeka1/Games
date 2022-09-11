using Chess.Logic.Engine.Rules;
using Chess.Logic.Engine.Rules.Movements;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.RuleManager
{
    public class PawnRuleGroup : RuleGroup
    {
        public PawnRuleGroup()
        {
            Rules.Add(new PawnMoves());
            Rules.Add(new CanOnlyTakeEnemy());
            Rules.Add(new IsNotBeChecked());
        }

        protected override FigureType? Figure => FigureType.Pawn;
    }
}
