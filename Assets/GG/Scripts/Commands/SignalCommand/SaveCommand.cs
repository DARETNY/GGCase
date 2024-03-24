using System;
using System.IO;
using GG.Data;
using GG.Signals;
using UnityEngine;
using Zenject;

namespace GG.Commands
{
    public class SaveCommand : IInitializable, IDisposable
    {

        [Inject]
        private SignalBus _signalBus;
        private UserInfo _userInfo;
        public void Initialize() => _signalBus.Subscribe<SaveScore>(SaveScore);

        private void SaveScore(SaveScore obj)
        {
            string dataPath = Path.Combine(Application.persistentDataPath, $"UserData {obj.Level}.json");


            if (File.Exists(dataPath))
            {
                var readAllText = File.ReadAllText(dataPath);
                _userInfo = JsonUtility.FromJson<UserInfo>(readAllText);
                _userInfo.score = obj.Score;

                string json = JsonUtility.ToJson(_userInfo);


                File.WriteAllText(dataPath, json);
                Debug.Log($"Score saved for level {obj.Level}");
            }
            else
            {
                _userInfo = new UserInfo();
                _userInfo.score = obj.Score;
                var json = JsonUtility.ToJson(_userInfo);
                File.WriteAllText(dataPath, json);

            }
        }


        public bool CompareScore(int level, int score)
        {
            var dataPath = Path.Combine(Application.persistentDataPath, $"UserData {level}.json");
            

            if (File.Exists(dataPath))
            {
                var loadedData = File.ReadAllText(dataPath);
                _userInfo = JsonUtility.FromJson<UserInfo>(loadedData);
                return score > _userInfo.score;
            }
            Debug.LogError($"User info file not found: {dataPath}");
            return false;
        }
        public void Dispose() => _signalBus.Unsubscribe<SaveScore>(SaveScore);
    }
}