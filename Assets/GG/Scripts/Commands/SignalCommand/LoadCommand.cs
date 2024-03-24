using System.Collections.Generic;
using System.IO;
using System.Linq;
using GG.Data;
using UnityEngine;

namespace GG.Commands.SignalCommand
{
    public class LoadCommand
    {

        public List<UserInfo> LoadData(int maxlevel)
        {
            List<UserInfo> userInfos = new List<UserInfo>();
            for (int i = 0; i <= maxlevel; i++)
            {
                string dataPath = Path.Combine(Application.persistentDataPath, $"UserData {i}.json");
                if (File.Exists(dataPath))
                {
                    var readAllText = File.ReadAllText(dataPath);
                    var userInfo = JsonUtility.FromJson<UserInfo>(readAllText);
                    userInfos.Add(userInfo);
                }
                else
                {

                    var userInfo = new UserInfo();
                    userInfos.Add(userInfo);
                }
            }
            userInfos.First().playedBefore = true;
            return userInfos;
        }

    }
}