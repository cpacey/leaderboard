namespace Leaderboard {
	public class Configuration : IConfiguration {
		string IConfiguration.LeaderboardTableName => Environment.GetEnvironmentVariable( "DYNAMO_TABLE" );
	}
}
