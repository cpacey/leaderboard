namespace Leaderboard {
	public interface IConfiguration {
		string LeaderboardTableName { get; }
		string? DynamoEndpoint { get; }
	}
}
