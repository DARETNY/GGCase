using System.Collections.Generic;

namespace GG.Runtime
{
    public class Score
    {
        private int _score;

        private readonly Dictionary<char, int> _letterScores = new Dictionary<char, int>
        {
                { 'E', 1 }, { 'A', 1 }, { 'O', 1 }, { 'N', 1 }, { 'R', 1 }, { 'T', 1 }, { 'L', 1 }, { 'S', 1 }, { 'U', 1 },
                { 'I', 1 },
                { 'D', 2 }, { 'G', 2 },
                { 'B', 3 }, { 'C', 3 }, { 'M', 3 }, { 'P', 3 },
                { 'F', 4 }, { 'H', 4 }, { 'V', 4 }, { 'W', 4 }, { 'Y', 4 },
                { 'K', 5 },
                { 'J', 8 }, { 'X', 8 },
                { 'Q', 10 }, { 'Z', 10 }
        };


        public void AddToScore(string score)
        {
            this._score += score.Length * 100;
            foreach (char c in score.ToUpper())
            {
                if (_letterScores.TryGetValue(c, out int letterScore))
                {
                    this._score += letterScore * 10;
                }

            }
        }

        public void LevelEndScore(int leftWords) => this._score -= leftWords * 100;
        public int GetScore() => _score;
    }
}