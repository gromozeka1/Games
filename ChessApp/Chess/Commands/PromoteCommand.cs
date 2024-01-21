using System;

using Chess.Commands.Interfaces;
using Chess.Models.Pieces;
using Chess.Models;

namespace Chess.Commands;

public class PromoteCommand : ICompensableCommand
{
    private readonly Board _board;
    private readonly ICompensableCommand _moveCommand;
    private readonly BasePiece? _oldPawn;

    public PromoteCommand(Move move, Board board)
    {
        if (move?.PromotePieceType == null)
        {
            throw new NullReferenceException("Can't build a promote command with null Move.PromotedPieceType");
        }

        _board = board;
        Move = move;

        _moveCommand = new MoveCommand(move, board);
        _oldPawn = board.FigureAt(move.From);
    }

    private PromoteCommand(PromoteCommand promoteCommand, Board board)
    {
        Move = promoteCommand.Move;
        _board = board;
        _moveCommand = promoteCommand._moveCommand.Copy(board);
        _oldPawn = board.FigureAt(Move.From);
    }

    public void Execute()
    {
        _moveCommand.Execute();

        Square square = _board.SquareAt(Move.To);
        BasePiece piece;
        switch (Move.PromotePieceType)
        {
            case FigureType.Bishop:
                piece = new Bishop(Move.Color, square);
                break;
            case FigureType.King:
                piece = new King(Move.Color, square);
                break;
            case FigureType.Queen:
                piece = new Queen(Move.Color, square);
                break;
            case FigureType.Pawn:
                piece = new Pawn(Move.Color, square);
                break;
            case FigureType.Knight:
                piece = new Knight(Move.Color, square);
                break;
            case FigureType.Rook:
                piece = new Rook(Move.Color, square);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        square.Piece = piece;
    }

    public void Compensate()
    {
        _board.SquareAt(Move.To).Piece = _oldPawn;
        _moveCommand.Compensate();
    }

    public bool TakePiece => _moveCommand.TakePiece;

    public Move Move { get; }

    public FigureType? PieceType => Move.Figure;

    public FigureColor PieceColor => Move.Color;

    public ICompensableCommand Copy(Board board) => new PromoteCommand(this, board);

    public override string ToString() =>
        "Promotion en " + Move.PromotePieceType + " en " + Move.To;
}
