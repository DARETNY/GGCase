namespace GG.Signals
{
    public struct SaveScore
    {
        public int Level;
        public int Score;
        
        public SaveScore(int level, int score)
        {
            Level = level;
            Score = score;
        }
    }
}