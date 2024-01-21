using Chess.Commands.Interfaces;
using Chess.Commands;
using Chess.Models;
using Chess.Models.Pieces;
using Chess.Logic.Engine.States;

namespace Chess.Logic.Engine.Rules;

public class IsNotBeChecked : IRule
{
    public bool IsMoveValid(Move move, Board board)
    {
        IState checkState = new CheckState();
        Board tempBoard = new Board(board);

        bool castling = new Castling().IsMoveValid(move, board) && (move.Figure == FigureType.King) &&
                        (tempBoard.FigureAt(move.To)?.Figure == FigureType.Rook) &&
                        (move.Color == tempBoard.FigureAt(move.To)?.Color);

        if (!castling)
        {
            if (move.Color == tempBoard.FigureAt(move.To)?.Color)
            {
                return true;
            }
        }

        ICompensableCommand command = castling
            ? new CastlingCommand(move, tempBoard)
            : new MoveCommand(move, tempBoard);

        command.Execute();

        return !checkState.IsInState(tempBoard, move.Color);
    }
}
