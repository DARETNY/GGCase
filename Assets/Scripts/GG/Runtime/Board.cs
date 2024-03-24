using System.Collections.Generic;
using GG.Commands;
using GG.Commands.SignalCommand;
using GG.Signals;
using UnityEngine;
using Zenject;

namespace GG.Runtime
{
    public class Board : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> letterHolders = new();

        [SerializeField]
        private Transform _holderParent, panel;

        private SignalBus _signalBus;
        private LevelCommand _levelCommand;
        private Factory _factory;
        private StringChecker _stringChecker;
        private List<WordSample> _wordSamples = new();
        private readonly List<WordSample> tableLetters = new();
        private string _wordString;
        public Level level;
        private Score _score;
        private BillBoard _billBoard;

        public int Size => letterHolders.Count;
        public List<WordSample> Samples => _wordSamples;
        private List<GameObject> _wordSamplesGO = new();

        [Inject]
        public void Construct(SignalBus signalBus,
                              LevelCommand levelCommand,
                              Factory factory,
                              StringChecker stringChecker,
                              Score score,
                              BillBoard billBoard)
        {
            _signalBus = signalBus;
            _levelCommand = levelCommand;
            _factory = factory;
            _stringChecker = stringChecker;
            _score = score;
            _billBoard = billBoard;
        }

        private void OnEnable() => SubscribeToSignals();
        private void OnDisable() => UnsubscribeFromSignals();

        private void SubscribeToSignals()
        {
            _signalBus.Subscribe<ConfirmButtonClicked>(HandleCompletion);
            _signalBus.Subscribe<UndoButtonClicked>(Undo);
            _signalBus.Subscribe<CallLevel>(LevelSignal);
            _signalBus.Subscribe<LevelEndClick>(LevelEndClick);
            _signalBus.Subscribe<OnClearBoard>(ClearBoar);
            _signalBus.Subscribe<UndoAllButton>( OnholdTrigger);
        }
       

        private void UnsubscribeFromSignals()
        {
            _signalBus.Unsubscribe<ConfirmButtonClicked>(HandleCompletion);
            _signalBus.Unsubscribe<UndoButtonClicked>(Undo);
            _signalBus.Unsubscribe<CallLevel>(LevelSignal);
            _signalBus.Unsubscribe<LevelEndClick>(LevelEndClick);
            _signalBus.Unsubscribe<OnClearBoard>(ClearBoar);
            _signalBus.Unsubscribe<UndoAllButton>(OnholdTrigger);
        }

        private void LevelEndClick()
        {
            int activeChildrenCount = 0;
            foreach (Transform child in this.transform.GetChild(1))
                if (child.gameObject.activeSelf)
                    activeChildrenCount++;
            _score.LevelEndScore(activeChildrenCount);
        }

        private void HandleCompletion()
        {
            _signalBus.Fire(new OnStringComplate(_wordString));
            if (_stringChecker.IsValid())
            {
                _score.AddToScore(_wordString);
                _billBoard.WriteToBillBoard(_wordString, panel);
                CompleteTable();
            }
            else
            {
                ClearTable();
            }
        }

        private void ClearTable()
        {
            _wordString = string.Empty;
            tableLetters.Clear();
        }
        
        private void OnholdTrigger()
        {
            
           tableLetters.Clear();
           _wordString = string.Empty;
           
           _signalBus.Fire(new UndoActive(tableLetters.Count > 0));
           // _signalBus.Fire(new OnStringComplate(_wordString));
           _signalBus.Fire(new OnConfirmActivate(false));
        }

        private void ClearBoar()
        {
            foreach (var o in _wordSamplesGO)
                Destroy(o);
            tableLetters.Clear();
            _wordSamples.Clear();
            _wordSamplesGO.Clear();
            _wordString = string.Empty;
            _holderParent.localPosition = Vector3.zero;
            _billBoard.ResetBillBoard(panel);
        }

        private void CompleteTable()
        {
            _wordString = string.Empty;
            _stringChecker._isvalid = true;
            foreach (var letter in tableLetters)
            {
                _wordSamples.Remove(letter);
                letter.gameObject.SetActive(false);
            }
            tableLetters.Clear();
        }

        private void Undo()
        {
            int wordSize = _wordString.Length;
            if (wordSize == 0)
                return;
            _wordString = _wordString.Remove(wordSize - 1);
            var lastLetter = tableLetters[^1];
            tableLetters.Remove(lastLetter);
            _signalBus.Fire(new UndoActive(tableLetters.Count > 0));
            _signalBus.Fire(new OnStringComplate(_wordString));
            _signalBus.Fire(new OnConfirmActivate(_stringChecker.IsValid()));

            
        }

        private void LevelSignal(CallLevel level)
        {
            _signalBus.Fire(new OnLevelLoadedSignal(level.Level));
            this.level = _levelCommand.GetLevelJs();
            CreateSamples();
        }

        public Vector3 GetHolderPos(int index) => letterHolders[index].position;

        public void SendToTable(WordSample wordSample)
        {
            _wordString += wordSample.dataHolder.CharValue;
            tableLetters.Add(wordSample);
            _signalBus.Fire(new OnStringComplate(_wordString));
            _signalBus.Fire(new OnConfirmActivate(_stringChecker.IsValid()));
            _signalBus.Fire(new UndoActive(tableLetters.Count > 0));

        }

        private void CreateSamples()
        {
            var boardData = _levelCommand.GetBoardData();
            var ids = boardData.GetAllId();
            var positions = boardData.GetAllPositions();
            var characters = boardData.GetAllCharacter();
            foreach (int id in ids)
            {
                var instance = _factory.Create(positions[id] * .1f, characters[id], _holderParent);
                var _word = instance.GetComponent<WordSample>();
                if (instance == null)
                    continue;
                _wordSamplesGO.Add(instance);
                _word.SetID(id);
                _word.name = id.ToString();
                _word.SetLetter(characters[id]);
                var children = level.tiles[id].children;
                _word.SetEachChildren(children);
                _wordSamples.Add(_word);
                _word.Refresh();
            }
            _holderParent.localPosition = new Vector3(-2.5f, 1);
        }

        public class Factory : PlaceholderFactory<Vector3, char, Transform, GameObject>
        {
        }
    }
}