namespace GG.Signals
{
    public struct OnLevelLoadedSignal
    {
        public int Level;

        public OnLevelLoadedSignal(int level)
        {
            this.Level = level;
        }
    }
}