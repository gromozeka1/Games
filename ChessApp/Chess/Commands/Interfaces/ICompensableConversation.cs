namespace Chess.Commands.Interfaces
{
    public interface ICompensableConversation
    {
        void Execute(ICompensableCommand command);
        ICompensableCommand? Redo();
        ICompensableCommand? Undo();
    }
}
