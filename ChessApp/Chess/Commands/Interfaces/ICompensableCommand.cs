using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Commands.Interfaces
{
    /// <summary>
    /// Provides functionality for commands that can be done and undone.
    /// </summary>
    public interface ICompensableCommand
    {
        /// <summary>
        /// Take the enemy piece.
        /// </summary>
        /// <value>
        /// True if this command takes an enemy piece.
        /// </value>
        bool TakePiece { get; }

        /// <summary>
        /// Move.
        /// </summary>
        /// <value>
        /// Move corresponding to the command.
        /// </value>
        Move Move { get; }

        /// <summary>
        /// Piece type.
        /// </summary>
        /// <value>
        /// The type of the piece that is concerned by the move.
        /// </value>
        FigureType? PieceType { get; }
        
        /// <summary>
        /// Piece color.
        /// </summary>
        /// <value>
        /// The color of the piece that is concerned by the move.
        /// </value>
        FigureColor PieceColor { get; }
        
        /// <summary>
        /// Executes command.
        /// </summary>
        void Execute();
        
        /// <summary>
        /// Undo the command.
        /// </summary>
        void Compensate();

        /// <summary>
        /// Copy constructor to change the acting model.
        /// </summary>
        /// <param name="board">The new board to apply the command on.</param>
        /// <returns>The new command that apply on the given board.</returns>
        ICompensableCommand Copy(Board board);
    }
}
