namespace Leaderboard {
	public interface ILeaderboardManager {

		void AddToLeaderboard( string username, int score );
		IEnumerable<LeaderboardEntry> GetLeaders( int count );

	}
}