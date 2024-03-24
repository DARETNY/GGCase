using System;
using Coffee.UIExtensions;
using GG.Commands;
using GG.Commands.SignalCommand;
using GG.Signals;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GG.Runtime
{
    public class UIManager : MonoBehaviour
    {

        #region Variables

        [SerializeField] private Transform _highScorePanel;
        [SerializeField] private Button _confirmButton, _undoButton, _finishLevelButton, _levels;
        [SerializeField] private UIParticle clickParticle;
        [SerializeField] private TextMeshProUGUI _scoreText, _highScoreText;

        #endregion

        [Inject]
        private SignalBus _signalBus;
        [Inject]
        private Score _score;
        [Inject]
        private SaveCommand _saveCommand;
        [Inject]
        private LevelCommand _levelCommand;

        private void Start()
        {
            _signalBus.Subscribe<OnConfirmActivate>(ConfirmCallback);
            _signalBus.Subscribe<UndoActive>(UndoCallback);
            _levels.OnClickAsObservable().Subscribe(_ => _signalBus.Fire(new InıtLevels())).AddTo(this);
            _confirmButton.OnClickAsObservable().Subscribe(_ => ConfirmButtonClicked()).AddTo(this);
            _undoButton.OnClickAsObservable().Subscribe(_ => _signalBus.Fire(new UndoButtonClicked())).AddTo(this);
            _undoButton.OnPointerDownAsObservable()
                    .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(2)).TakeUntil(_undoButton.OnPointerUpAsObservable())
                                        .Take(1)).Subscribe(_ => _signalBus.Fire(new UndoAllButton())).AddTo(this);
            _finishLevelButton.OnClickAsObservable().Subscribe(_ => FinishLevelButtonClicked()).AddTo(this);
        }
        private void UndoCallback(UndoActive obj) => _undoButton.interactable = obj.IsActive;
        private void ConfirmCallback(OnConfirmActivate obj) => _confirmButton.interactable = obj.IsActivate;


        private void ConfirmButtonClicked()
        {
            _signalBus.Fire(new ConfirmButtonClicked());
            _scoreText.SetText($"Score: {_score.GetScore()}");
            _finishLevelButton.interactable = true;
            clickParticle.Play();
            _confirmButton.interactable = false;


        }


        private void FinishLevelButtonClicked()
        {
            _signalBus.Fire<LevelEndClick>();
            _scoreText.SetText($"Score: {string.Empty}");
            // _scoreText.SetText($"Score: {_score.GetScore()}");
            _finishLevelButton.interactable = false;
            _signalBus.Fire<OnClearBoard>();
            _signalBus.Fire(new SaveScore(_levelCommand.Currentlevel, _score.GetScore()));

            if (_levelCommand.Currentlevel < _score.GetScore())
            {
                _highScorePanel.gameObject.SetActive(true);
                _highScoreText.SetText($"High Score: {_score.GetScore()}");
            }
            if (_saveCommand.CompareScore(_levelCommand.Currentlevel, _score.GetScore()) is true)
            {
                _highScoreText.SetText($"High Score: {_score.GetScore()}");
            }

            _signalBus.Fire(new InıtLevels());
        }
    }
}