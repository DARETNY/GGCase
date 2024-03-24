using System;
using GG.Data;
using GG.Signals;
using UnityEngine;
using Zenject;

namespace GG.Commands.SignalCommand
{
    public class LevelCommand : IInitializable, IDisposable
    {
        [Inject]
        private SignalBus _signalBus;

        private Level level;
        private BoardData _boardData;
        public int Currentlevel;

        public void Initialize() => _signalBus.Subscribe<OnLevelLoadedSignal>(OnLevelData);

        private void OnLevelData(OnLevelLoadedSignal obj)
        {
            level = JsonUtility.FromJson<Level>(Resources.Load<TextAsset>($"Levels/level_{obj.Level}").text);
            _boardData = new BoardData(level);
            Currentlevel = obj.Level;
        }

        public Level GetLevelJs() => level;
        public BoardData GetBoardData() => _boardData;


        public void Dispose() => _signalBus.Unsubscribe<OnLevelLoadedSignal>(OnLevelData);


    }


}