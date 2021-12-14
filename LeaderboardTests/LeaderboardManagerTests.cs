using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Leaderboard;
using NUnit.Framework;

namespace LeaderboardTests {

	[TestFixture]
	public class LeaderboardTests {

		private ILeaderboardManager m_leaderboardManager;

		[Test]
		public void LeaderboardManager_GetLeaders_WhenCountIsNegative(
			[Values( -1, -52345, int.MinValue )] int count
		) {

			Assert.ThrowsAsync<ArgumentException>( async () => {
				await m_leaderboardManager.GetLeadersAsync( count );
			} );
		}

		[Test]
		public async Task LeaderboardManager_GetLeaders_WhenCountIsZero_ReturnsEmpty() {

			IEnumerable<LeaderboardEntry>? results = await m_leaderboardManager.GetLeadersAsync( 0 );

			CollectionAssert.IsEmpty( results );
		}

		[SetUp]
		public void SetUp() {
			m_leaderboardManager = new LeaderboardManager(
				null,
				null
			);
		}
	}
}
