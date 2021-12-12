namespace Leaderboard {
	public sealed class LeaderboardManager : ILeaderboardManager {
		void ILeaderboardManager.AddToLeaderboard( string username, int score ) {
			throw new NotImplementedException();
		}

		IEnumerable<LeaderboardEntry> ILeaderboardManager.GetLeaders( int count ) {
			throw new NotImplementedException();
		}
	}
}
