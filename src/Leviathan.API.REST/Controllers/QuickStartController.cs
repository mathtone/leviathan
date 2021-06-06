using Leviathan.Services.Core.QuickStart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.API.REST.Controllers {
	
	[Route("[controller]")]
	[ApiController]
	public class QuickStartController : LeviathanController {
		
		IQuickStartService service;

		public QuickStartController(IQuickStartService service) {
			this.service = service;
		}

		[HttpGet("Profiles")]
		public IEnumerable<QuickStartInfo> Profiles() => service.QuickStartProfiles();

		[HttpPost("ApplyProfile")]
		public void ApplyProfile(string profileId) => service.RunQuickStart(profileId);
	}
}
