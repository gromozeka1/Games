using Chess.Commands.Interfaces;
using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Commands
{
    public class MoveCommand : ICompensableCommand
    {
        private Board _board;

        private bool _hasChangedState;
        private BasePiece? _piece;
        private BasePiece? _removedPiece;
        private Square? _startSquare;
        private Square? _targetSquare;

        /// <summary>
        ///     MoveCommand constructor
        /// </summary>
        /// <param name="move">The move to do</param>
        /// <param name="board">The board the command executes on</param>
        public MoveCommand(Move move, Board board)
        {
            _board = board;
            Move = move;

            TakePiece = board.FigureAt(Move.To) is not null;
        }

        private MoveCommand(MoveCommand command, Board board)
        {
            _board = board;
            Move = command.Move;

            TakePiece = board.FigureAt(Move.To) is not null;
        }

        /// <summary>
        ///     Execute the move on the Board
        /// </summary>
        public void Execute()
        {
            _targetSquare = _board.SquareAt(Move.To);
            _startSquare = _board.SquareAt(Move.From);
            _piece = _startSquare.Piece;

            //Has moved update
            if (!_piece.HasMoved)
            {
                _piece.HasMoved = true;
                _hasChangedState = true;
            }

            //Square is empty of piece
            if (_targetSquare.Piece == null)
            {
                _startSquare.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
            //There is a taken piece
            else
            {
                _removedPiece = _targetSquare.Piece;
                _targetSquare.Piece = null;
                _piece.Square.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
        }

        /// <summary>
        /// Undo the move
        /// </summary>
        public void Compensate()
        {
            if (_hasChangedState)
            {
                _piece.HasMoved = false;
            }

            _targetSquare.Piece = _removedPiece;
            _startSquare.Piece = _piece;
            _piece.Square = _startSquare;
        }

        public bool TakePiece { get; }

        public Move Move { get; }

        public FigureType? PieceType => Move.Figure;

        public FigureColor PieceColor => Move.Color;

        public ICompensableCommand Copy(Board board) => new MoveCommand(this, board);

        public override string ToString() => _piece + " de " + Move.From + " vers " + Move.To;
    }
}
