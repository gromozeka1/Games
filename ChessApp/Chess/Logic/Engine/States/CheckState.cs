using System.Collections.Generic;
using System.Linq;

using Chess.Logic.Engine.Rules.Movements;
using Chess.Logic.Engine.Rules;
using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.States;

public class CheckState : IState
{
    public bool IsInState(Board board, FigureColor color)
    {
        Board tempBoard = new Board(board);
        List<IRule> queenMovementCheckRules = new List<IRule>
        {
            new CanOnlyTakeEnemy(),
            new QueenMoves(),
        };

        List<IRule> pawnMovementCheckRules = new List<IRule>
        {
            new CanOnlyTakeEnemy(),
            new PawnMoves(),
        };

        List<IRule> kingMovementCheckRules = new List<IRule>
        {
            new KingMoves(),
            new CanOnlyTakeEnemy(),
            new Castling(),
        };

        List<IRule> knightMovementCheckRules = new List<IRule>
        {
            new CanOnlyTakeEnemy(),
            new KnightMoves(),
        };

        List<IRule> rookMovementCheckRules = new List<IRule>
        {
            new CanOnlyTakeEnemy(),
            new RookMoves(),
        };

        List<IRule> bishopMovementCheckRules = new List<IRule>
        {
            new CanOnlyTakeEnemy(),
            new BishopMoves(),
        };

        Dictionary<FigureType, List<IRule>> rulesGroup = new Dictionary<FigureType, List<IRule>>
        {
            {FigureType.Queen, queenMovementCheckRules},
            {FigureType.Pawn, pawnMovementCheckRules},
            {FigureType.Knight, knightMovementCheckRules},
            {FigureType.Rook, rookMovementCheckRules},
            {FigureType.Bishop, bishopMovementCheckRules},
            {FigureType.King, kingMovementCheckRules}
        };

        BasePiece? concernedKing = tempBoard.Squares.OfType<Square>()
            .First(x => (x?.Piece?.Figure == FigureType.King) && (x?.Piece?.Color == color)).Piece;

        bool res = false;
        foreach (KeyValuePair<FigureType, List<IRule>> rules in rulesGroup)
        {
            var possibleMoves = new List<Square>();
            concernedKing.Figure = rules.Key;
            possibleMoves = possibleMoves.Concat(rules.Value.First().PossibleMoves(concernedKing)).ToList();
            rules.Value.ForEach(
                x => possibleMoves = possibleMoves.Intersect(x.PossibleMoves(concernedKing)).ToList());

            if (possibleMoves.Any(x => x?.Piece?.Figure == rules.Key))
            {
                res = true;
            }
        }

        concernedKing.Figure = FigureType.King;
        return res;
    }

    public string Explain() => "The king is checked!";
}
