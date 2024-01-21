using Chess.Commands.Interfaces;
using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Commands;

public class EnPassantCommand : ICompensableCommand
{
    private ICompensableCommand _firstMove;
    private ICompensableCommand _secondMove;


    public EnPassantCommand(Move move, Board board)
    {
        Move = move;

        bool isWhite = move.Color == FigureColor.White;
        bool isLeft = move.From.X > move.To.X;

        int x = move.From.X + (isLeft ? -1 : 1);
        int y = move.From.Y;

        Square startSquare = board.SquareAt(move.From);
        Square secondSquare = board.Squares[x, y];
        Square thirdSquare = board.Squares[x, y + (isWhite ? -1 : 1)];

        _firstMove = new MoveCommand(new Move(startSquare, secondSquare, Move.Figure, Move.Color), board);
        _secondMove = new MoveCommand(new Move(secondSquare, thirdSquare, Move.Figure, Move.Color), board);
    }

    private EnPassantCommand(EnPassantCommand command, Board board)
    {
        Move = command.Move;
        _firstMove = command._firstMove.Copy(board);
        _secondMove = command._secondMove.Copy(board);
    }

    public void Execute()
    {
        _firstMove.Execute();
        _secondMove.Execute();
    }

    public void Compensate()
    {
        _secondMove.Compensate();
        _firstMove.Compensate();
    }

    public bool TakePiece => true;

    public Move Move { get; }

    public FigureType? PieceType => Move.Figure;

    public FigureColor PieceColor => Move.Color;

    public ICompensableCommand Copy(Board board) => new EnPassantCommand(this, board);

    public override string ToString() => "En passant de " + Move.From + " vers " + Move.To;
}
