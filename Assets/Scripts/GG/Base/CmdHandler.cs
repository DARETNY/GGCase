using System.Collections.Generic;

namespace GG.Base
{
    public class CmdHandler
    {

        private readonly Stack<ICommand> _commands = new Stack<ICommand>();

        public void Execute(ICommand command)
        {
            _commands.Push(command);
            command.Execute();
        }

        public void Reverse()
        {
            if (_commands.Count <= 0)
                return;
            _commands.Pop().Undo();
        }

        public void ReverseAll()
        {
            while (_commands.Count > 0)
            {
                // Reverse();
                _commands.Pop().Undo();
            }
        }
    }
}