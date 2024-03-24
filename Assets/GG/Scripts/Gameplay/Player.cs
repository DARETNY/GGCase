using GG.Base;
using GG.Commands;
using GG.Commands.SignalCommand;
using GG.Signals;
using UnityEngine;
using Zenject;

namespace GG.Runtime
{
	public class Player : MonoBehaviour
	{
		private CmdHandler _cmdHandler;
		private InputHandler _inputHandler;
		private SignalBus _signalBus;
		private Board _board;
		private StringChecker _stringChecker;

		private int _clicker = 0;

		[Inject]
		private void Construct(CmdHandler cmdHandler, InputHandler inputHandler, SignalBus signalBus,
			Board board, StringChecker stringChecker)
		{
			_cmdHandler = cmdHandler;
			_inputHandler = inputHandler;
			_signalBus = signalBus;
			_board = board;
			_stringChecker = stringChecker;
		}

		private void OnEnable()
		{
			_signalBus.Subscribe<ConfirmButtonClicked>(HandleCompletion);
			_signalBus.Subscribe<UndoButtonClicked>(Undo);
			_signalBus.Subscribe<UndoAllButton>(UndoAll);
		}
		
		private void OnDisable()
		{
			_signalBus.Unsubscribe<ConfirmButtonClicked>(HandleCompletion);
			_signalBus.Unsubscribe<UndoButtonClicked>(Undo);
			_signalBus.Unsubscribe<UndoAllButton>(UndoAll);
		}

		private void HandleInput()
		{
			if (Input.GetMouseButtonDown(0) && _inputHandler.GetInput())
				MoveObstacleByClick();
		}
		
		private void Update()
		{
			HandleInput();
		}

		#region Event

		private void Undo()
		{
			if (_clicker <= 0)
				return;
			_clicker--;
			_cmdHandler.Reverse();
		}

		private void UndoAll()
		{
			_clicker = 0;
			_cmdHandler.ReverseAll();
		}

		#endregion // Event

		private void HandleCompletion()
		{
			_clicker = 0;
			
		}

		private void MoveObstacleByClick()
		{
			var hit = _inputHandler.Cast();

			if (!hit.collider.TryGetComponent(out WordSample wordSample))
				return;
			
			int boardSize = _board.Size;

			if (_clicker == boardSize)
				return;

			var targetPos = _board.GetHolderPos(_clicker % boardSize);

			_clicker++;

			var samples = _board.Samples;
			var obstacle = new GoToTableCommand(wordSample, ref samples, hit.transform, targetPos);
			_cmdHandler.Execute(obstacle);

			_board.SendToTable(wordSample);
		}
	}
}