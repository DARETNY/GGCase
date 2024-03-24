using System.Collections.Generic;
using GG.Data;

namespace GG.Signals
{
    public struct ReadData
    {
        public List<UserInfo> userInfos;
        public int MaxLevel;
        public ReadData(List<UserInfo> userInfos, int maxLevel)
        {
            this.userInfos = userInfos;
            this.MaxLevel = maxLevel;
        }
      
    }
}