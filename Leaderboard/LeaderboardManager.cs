namespace Leaderboard {
	public sealed class LeaderboardManager : ILeaderboardManager {
		void ILeaderboardManager.AddToLeaderboard( string username, int score ) {
			throw new NotImplementedException();
		}

		IEnumerable<LeaderboardEntry> ILeaderboardManager.GetLeaders( int count ) {

			if( count < 0 ) {
				throw new ArgumentException( "Count must be non-negative", nameof( count ) );
			}

			if( count == 0 ) {
				return Enumerable.Empty<LeaderboardEntry>();
			}

			throw new NotImplementedException();
		}
	}
}
