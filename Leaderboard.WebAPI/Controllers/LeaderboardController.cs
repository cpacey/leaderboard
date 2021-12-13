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
		public IEnumerable<LeaderboardEntry> Get(
			[FromQuery] int count = 10
		) {
			return m_leaderboardManager.GetLeaders( count );
		}

		[HttpPost( Name = "PostEntry" )]
		public void Post(
			[FromBody] LeaderboardEntry entry
		) {
			m_leaderboardManager.AddToLeaderboard( entry.UserName, entry.Score );
		}
	}
}
