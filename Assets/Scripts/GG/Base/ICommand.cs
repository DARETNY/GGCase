namespace GG.Base
{
	public interface ICommand
	{
		void Execute();

		void Undo();
	}
}