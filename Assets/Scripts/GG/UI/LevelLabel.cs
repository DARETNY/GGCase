using DG.Tweening;
using GG.Signals;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GG.UI
{
    public class LevelLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;

        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button playButton;
        private SignalBus _signalBus;

        public void Initialize(SignalBus signalBus) => _signalBus = signalBus;

        public void SetLabel(string title, int level, int score, bool enable)
        {
            SetTexts(title, level, score);
            SetButtonColor(enable);
            SetButtonOnClick(level, score);

        }

        private void SetTexts(string title, int level, int score)
        {
            this.title.text = $"Level {level} - {title}";
            _scoreText.SetText($"Score: {score}");
        }
        public string GetTitle() => title.text;

        private void SetButtonColor(bool enable)
        {
            playButton.interactable = enable;
            var targetColor = enable ? Color.green : Color.gray;

            playButton.image.DOColor(targetColor, 0.5f);
        }

        private void SetButtonOnClick(int level, int score)
        {
            playButton.OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        // _signalBus.Fire(new InıtLevels());
                        _signalBus.Fire(new CallLevel(level));
                        GetComponentInParent<LevelUI>().gameObject.SetActive(false);

                    })
                    .AddTo(this);
        }
    }
}