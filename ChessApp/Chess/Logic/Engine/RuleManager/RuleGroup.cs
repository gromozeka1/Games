using System;
using System.Collections.Generic;
using System.Linq;

using Chess.Logic.Engine.Rules;
using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.RuleManager;

public abstract class RuleGroup
{
    public RuleGroup? Next { get; internal set; }
    protected List<IRule> Rules { get; set; } = new List<IRule>();
    protected abstract FigureType? Figure { get; }

    public void AddGroup(RuleGroup ruleGroup)
    {
        if (Next is null)
        {
            Next = ruleGroup;
        }
        else
        {
            Next.AddGroup(ruleGroup);
        }
    }

    public bool Handle(Move move, Board board) => Next switch
    {
        _ when move.Figure == Figure => Rules.All(rule => rule.IsMoveValid(move, board)),
        not null => Next.Handle(move, board),
        _ => throw new Exception("NOBODY TREATS THIS PIECE !!! " + move.Figure)
    };

    public List<Square> PossibleMoves(BasePiece piece)
    {
        List<Square> result = new List<Square>();
        
        if (piece.Figure == Figure)
        {
            result = result.Concat(Rules.First().PossibleMoves(piece)).ToList();
            Rules.ForEach(x => result = result.Intersect(x.PossibleMoves(piece)).ToList());
            return result;
        }

        return Next is not null
            ? Next.PossibleMoves(piece)
            : throw new Exception("NOBODY TREATS THIS PIECE !!! " + piece);
    }
}
