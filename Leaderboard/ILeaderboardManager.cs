namespace Leaderboard {
	public interface ILeaderboardManager {

		Task AddToLeaderboardAsync( string username, int score, CancellationToken cancellation = default );
		Task<IEnumerable<LeaderboardEntry>> GetLeadersAsync( int count, CancellationToken cancellation = default );

	}
}
