using Leaderboard;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardAPI.Controllers {
	[ApiController]
	[Route( "[controller]" )]
	public class LeaderboardController : ControllerBase {
		private readonly ILogger<LeaderboardController> m_logger;

		public LeaderboardController( ILogger<LeaderboardController> logger ) {
			m_logger = logger;
		}

		[HttpGet( Name = "GetLeaders" )]
		public IEnumerable<LeaderboardEntry> Get() {

			return Enumerable.Empty<LeaderboardEntry>();
		}
	}
}
