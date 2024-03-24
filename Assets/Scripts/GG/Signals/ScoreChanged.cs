namespace GG.Signals
{
	public struct ScoreChanged
	{
		public int score;
		
		public ScoreChanged(int score)
		{
			this.score = score;
		}
	}
}