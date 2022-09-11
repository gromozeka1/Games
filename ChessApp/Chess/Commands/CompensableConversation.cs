using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.Commands.Interfaces;

namespace Chess.Commands
{
    /// <summary>
    ///     Conversation keeping track of commands,
    ///     making it able to undo and redo commands
    /// </summary>
    public class CompensableConversation : ICompensableConversation
    {
        private readonly ObservableCollection<ICompensableCommand> _moveList;
        private readonly Stack<ICompensableCommand> _redoCommands = new();
        private readonly Stack<ICompensableCommand> _undoCommands = new();

        public CompensableConversation(ObservableCollection<ICompensableCommand> moveList)
        {
            _moveList = moveList;
            foreach (ICompensableCommand command in _moveList)
                _undoCommands.Push(command);
        }

        public void Execute(ICompensableCommand command)
        {
            command.Execute();
            _undoCommands.Push(command);
            _redoCommands.Clear();
        }

        public ICompensableCommand? Redo()
        {
            if (_redoCommands.Count == 0)
            {
                return null;
            }

            ICompensableCommand command = _redoCommands.Pop();
            command.Execute();
            _undoCommands.Push(command);

            return command;
        }

        public ICompensableCommand? Undo()
        {
            if (_undoCommands.Count == 0)
            {
                return null;
            }

            ICompensableCommand command = _undoCommands.Pop();
            command.Compensate();
            _redoCommands.Push(command);

            return command;
        }
    }
}
