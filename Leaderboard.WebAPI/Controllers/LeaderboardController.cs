using Leaderboard;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardAPI.Controllers {
	[ApiController]
	[Route( "[controller]" )]
	public class LeaderboardController : ControllerBase {
		private readonly ILogger<LeaderboardController> m_logger;
		private readonly ILeaderboardManager m_leaderboardManager;

		public LeaderboardController(
			ILogger<LeaderboardController> logger,
			ILeaderboardManager leaderboardManager
		) {
			m_logger = logger;
			m_leaderboardManager = leaderboardManager;
		}

		[HttpGet( Name = "GetLeaders" )]
		public async Task<IEnumerable<LeaderboardEntry>> GetAsync(
			[FromQuery] int count = 10
		) {
			return await m_leaderboardManager.GetLeadersAsync( count );
		}

		[HttpPost( Name = "PostEntry" )]
		public async Task PostAsync(
			[FromBody] LeaderboardEntry entry
		) {
			await m_leaderboardManager.AddToLeaderboardAsync( entry.UserName, entry.Score );
		}
	}
}
