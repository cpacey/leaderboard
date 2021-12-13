using System;
using System.Collections.Generic;
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

			Assert.Throws<ArgumentException>( () => {
				m_leaderboardManager.GetLeaders( count );
			} );
		}

		[Test]
		public void LeaderboardManager_GetLeaders_WhenCountIsZero_ReturnsEmpty() {

			IEnumerable<LeaderboardEntry>? results = m_leaderboardManager.GetLeaders( 0 );

			CollectionAssert.IsEmpty( results );
		}

		[SetUp]
		public void SetUp() {
			m_leaderboardManager = new LeaderboardManager();
		}
	}
}
