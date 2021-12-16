using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Leaderboard {
	public sealed class LeaderboardManager : ILeaderboardManager {

		public static class DynamoAttributeNames {
			public const string DateTime = "datetime";
			public const string UserName = "username";
			public const string Score = "score";
			public const string ExpiryTime = "ttl";
		}

		private readonly IAmazonDynamoDB m_client;
		private readonly IConfiguration m_configuration;

		public LeaderboardManager(
			IAmazonDynamoDB client,
			IConfiguration configuration
		) {
			m_client = client;
			m_configuration = configuration;
		}

		async Task ILeaderboardManager.AddToLeaderboardAsync(
			string username,
			int score,
			CancellationToken cancellationToken
		) {
			if( score < 0 ) {
				throw new ArgumentException( "Score must be non-negative", nameof( score ) );
			}

			Dictionary<string, AttributeValue> attributes = new() {
				{ DynamoAttributeNames.DateTime, GetCurrentTimeAttributeValue() },
				{ DynamoAttributeNames.UserName, new AttributeValue( username ) },
				{ DynamoAttributeNames.Score, new AttributeValue() { N = score.ToString() } },
				{ DynamoAttributeNames.ExpiryTime, GetTtlAttributeValue( TimeSpan.FromDays( 30 ) ) }
			};

			PutItemRequest putItemRequest = new(
				tableName: m_configuration.LeaderboardTableName,
				item: attributes
			);

			await m_client.PutItemAsync(
				putItemRequest,
				cancellationToken
			);
		}

		async Task<IEnumerable<LeaderboardEntry>> ILeaderboardManager.GetLeadersAsync(
			int count,
			CancellationToken cancellationToken
		) {

			if( count < 0 ) {
				throw new ArgumentException( "Count must be non-negative", nameof( count ) );
			}

			if( count == 0 ) {
				return Enumerable.Empty<LeaderboardEntry>();
			}

			QueryRequest queryRequest = new() {
				TableName = m_configuration.LeaderboardTableName,
				ScanIndexForward = true
			};

			QueryResponse result = await m_client.QueryAsync(
				queryRequest,
				cancellationToken
			);

			IEnumerable<LeaderboardEntry> records = result.Items.Select( CreateLeaderboardEntry );

			return records;
		}

		private static LeaderboardEntry CreateLeaderboardEntry( Dictionary<string, AttributeValue> attributes ) {
			string username = attributes[DynamoAttributeNames.UserName].S;

			string scoreString = attributes[DynamoAttributeNames.Score].N;
			int score = int.Parse( scoreString );

			return new LeaderboardEntry(
				UserName: username,
				Score: score
			);
		}

		private static AttributeValue GetTtlAttributeValue( TimeSpan timeSpan ) {

			long epochSeconds = DateTimeOffset.Now.Add( timeSpan ).ToUnixTimeSeconds();

			return new AttributeValue { N = epochSeconds.ToString() };
		}

		private static AttributeValue GetCurrentTimeAttributeValue() {

			long epochSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();

			return new AttributeValue { N = epochSeconds.ToString() };
		}
	}
}
