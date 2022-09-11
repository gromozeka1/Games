using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chess.Commands.Interfaces;
using Chess.Commands;

using Chess.Models.Pieces;

using Chess.Models;
using Chess.Logic.Engine.RuleManager;
using Chess.Logic.Engine.States;

namespace Chess.Logic.Engine
{
    public class RealEngine : IEngine
    {
        private readonly Container container;
        private readonly CompensableConversation conversation;
        private Pawn? enPassantPawnBlack;
        private Pawn? enPassantPawnWhite;
        private readonly ObservableCollection<ICompensableCommand> moves;
        private readonly RuleGroup ruleGroups;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealEngine"/> class.
        /// </summary>
        /// <param name="container">Container.</param>
        public RealEngine(Container container)
        {
            Board = container.Board;
            this.container = container;
            moves = container.Moves;

            conversation = new CompensableConversation(container.Moves);

            ruleGroups = new PawnRuleGroup();
            ruleGroups.AddGroup(new BishopRuleGroup());
            ruleGroups.AddGroup(new KingRuleGroup());
            ruleGroups.AddGroup(new KnightRuleGroup());
            ruleGroups.AddGroup(new QueenRuleGroup());
            ruleGroups.AddGroup(new RookRuleGroup());
        }

        /// <summary>
        /// Gets the board.
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// Ask the engine to do a move.
        /// </summary>
        /// <param name="move">The move to do</param>
        /// <returns>True if the move was done, otherwise - false.</returns>
        public bool DoMove(Move move)
        {
            //No reason to move if it's the same square
            if (move.From == move.To)
            {
                return false;
            }

            BasePiece? piece = Board.FigureAt(move.From);
            BasePiece? targetPiece = Board.FigureAt(move.To);

            //TODO gérer exception
            if (ruleGroups.Handle(move, Board))
            {
                ICompensableCommand command;
                if ((move.Figure == FigureType.King) &&
                    (((targetPiece?.Figure == FigureType.Rook) && (move.Color == targetPiece.Color))
                     || (Math.Abs(move.To.X - move.From.X) == 2)))
                    command = new CastlingCommand(move, Board);
                else if ((move.Figure == FigureType.Pawn) && (targetPiece is null) &&
                         (move.From.X != move.To.X))
                {
                    command = new EnPassantCommand(move, Board);
                }
                else if ((move.Figure == FigureType.Pawn) &&
                         (move.To.Y == (move.Color == FigureColor.White ? 0 : 7)))
                {
                    command = new PromoteCommand(move, Board);
                }
                else
                {
                    command = new MoveCommand(move, Board);
                }

                //En passant
                if (move.Color == FigureColor.White)
                {
                    if (enPassantPawnWhite is not null)
                    {
                        enPassantPawnWhite.EnPassant = false;
                        enPassantPawnWhite = null;
                    }
                }
                else
                {
                    if (enPassantPawnBlack is not null)
                    {
                        enPassantPawnBlack.EnPassant = false;
                        enPassantPawnBlack = null;
                    }
                }

                if ((move.Figure == FigureType.Pawn) && (Math.Abs(move.From.Y - move.To.Y) == 2))
                    if (move.Color == FigureColor.White)
                    {
                        enPassantPawnWhite = piece as Pawn;
                        enPassantPawnWhite.EnPassant = true;
                    }
                    else
                    {
                        enPassantPawnBlack = piece as Pawn;
                        enPassantPawnBlack.EnPassant = true;
                    }

                if (Board.FigureAt(move.To) is null)
                {
                    container.HalfMoveSinceLastCapture++;
                }
                else
                {
                    container.HalfMoveSinceLastCapture = 0;
                }

                conversation.Execute(command);
                moves.Add(command);

                return true;
            }

            return false;
        }

        public BoardState GetCurrentState()
        {
            var color = moves.Count == 0
                ? FigureColor.White
                : moves[^1].PieceColor;

            var stateColor = color == FigureColor.Black
                    ? FigureColor.White
                    : FigureColor.Black;

            return IsCheckmate(stateColor)
                ? new BoardState { Color = stateColor, State = Statement.Checkmate }
                : IsPat(stateColor)
                ? new BoardState { Color = stateColor, State = Statement.Pat }
                : IsCheck(stateColor)
                ? new BoardState { Color = stateColor, State = Statement.Check }
                : new BoardState { State = Statement.Normal };
        }

        private bool IsPat(FigureColor stateColor)
            => new PatState().IsInState(Board, stateColor);

        private bool IsCheck(FigureColor stateColor)
            => new CheckState().IsInState(Board, stateColor);

        private bool IsCheckmate(FigureColor stateColor)
            => IsCheck(stateColor) && IsPat(stateColor);

        public List<Square> PossibleMoves(BasePiece piece)
            => ruleGroups.PossibleMoves(piece);

        /// <summary>
        ///     Undo the last command that has been done
        /// </summary>
        /// <returns>True if anything has been done</returns>
        public Move? Undo()
        {
            ICompensableCommand? command = conversation.Undo();
            if (command is null)
            {
                return null;
            }

            if (container.HalfMoveSinceLastCapture != 0)
            {
                container.HalfMoveSinceLastCapture--;
            }
            else
            {
                int count = 0;
                for (int i = moves.Count - 1; i > 0; i--)
                {
                    if (moves[i].TakePiece)
                    {
                        break;
                    }

                    count++;
                }

                container.HalfMoveSinceLastCapture = count;
            }

            moves.Remove(command);
            return command.Move;
        }

        /// <summary>
        /// Redo the last command that has been undone.
        /// </summary>
        /// <returns>True if anything has been done.</returns>
        public Move? Redo()
        {
            ICompensableCommand? command = conversation.Redo();
            if (command is null)
            {
                return null;
            }

            //Number of moves since last capture
            if (!command.TakePiece)
            {
                container.HalfMoveSinceLastCapture++;
            }
            else
            {
                container.HalfMoveSinceLastCapture = 0;
            }

            moves.Add(command);
            return command.Move;
        }
    }
}
