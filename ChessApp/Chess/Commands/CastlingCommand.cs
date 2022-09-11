using Chess.Commands.Interfaces;
using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Commands
{
    public class CastlingCommand : ICompensableCommand
    {
        private ICompensableCommand _kingCommand;
        private ICompensableCommand _rookCommand;

        public CastlingCommand(Move move, Board board)
        {
            Move = move;

            bool isLeftCastling = move.To.X < move.From.X;

            _kingCommand =
                new MoveCommand(
                    new Move(board.FigureAt(move.From),
                        board.Squares[isLeftCastling ? 2 : 6, move.From.Y]), board);
            _rookCommand =
                new MoveCommand(
                    new Move(board.FigureAt(new Coordinate(isLeftCastling ? 0 : 7, move.From.Y)),
                        board.Squares[isLeftCastling ? 3 : 5, move.To.Y]), board);
        }

        private CastlingCommand(CastlingCommand command, Board board)
        {
            Move = command.Move;

            _rookCommand = command._rookCommand.Copy(board);
            _kingCommand = command._kingCommand.Copy(board);
        }

        public bool TakePiece => false;

        public Move Move { get; }

        public FigureType? PieceType => Move.Figure;

        public FigureColor PieceColor => Move.Color;

        public void Compensate()
        {
            _kingCommand.Compensate();
            _rookCommand.Compensate();
        }

        public ICompensableCommand Copy(Board board)
        {
            return new CastlingCommand(this, board);
        }

        public void Execute()
        {
            _rookCommand.Execute();
            _kingCommand.Execute();
        }

        public override string ToString() => "Roc vers tour " + Move.To;
    }
}
