namespace GG.Signals
{
    public struct OnSaveData
    {
        public int level;
        public string score;

        public OnSaveData(int level, string score)
        {
            this.level = level;
            this.score = score;
        }
    }
}