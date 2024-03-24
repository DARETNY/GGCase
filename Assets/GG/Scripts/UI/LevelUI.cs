using System;
using System.Collections.Generic;
using GG.Commands.SignalCommand;
using GG.Signals;
using UnityEngine;
using Zenject;

namespace GG.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField]
        private Transform content;

        [Inject]
        private LoadCommand _loadCommand;

        [Inject] SignalBus _signalBus;
        [Inject] private LevelLabel _levelLabel;
        [Inject] private List<Level> _levels;

        [SerializeField] private List<GameObject> _labelObjects;

        [Inject]
        private void Construct(SignalBus signalBus, LevelLabel levelLabel, List<Level> levels)
        {
            _signalBus = signalBus;
            this._levelLabel = levelLabel;
            this._levels = levels;
            // _signalBus.Subscribe<InıtLevels>(Callback);
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<InıtLevels>(Callback);
        }
        private void Callback()
        {
            ResetLevels();
            var userInfos = _loadCommand.LoadData(_levels.Count);
            for (int i = 0; i < _levels.Count; i++)
            {
                var level = _levels[i];
                var label = Instantiate(_levelLabel, content);
                _labelObjects.Add(label.gameObject);
                label.Initialize(_signalBus);
                var userinfo = userInfos[i];

                label.SetLabel(level.title, level: i, userinfo.score, userinfo.playedBefore);
                if (userinfo.score != 0 && i + 1 < _levels.Count)
                {
                    var nextlevel = userInfos[i + 1];


                    nextlevel.playedBefore = true;
                }

            }
        }

        private void ResetLevels()
        {
            foreach (var item in _labelObjects)
            {
                Destroy(item);
            }
            _labelObjects.Clear();
        }
        private void OnDisable()
        {
            _signalBus.TryUnsubscribe<InıtLevels>(Callback);
        }


    }
}