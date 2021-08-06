using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.ModuleBeta
{
	public class BetaService
	{
	}
	[ApiController]
	public class BetaController : ControllerBase
	{
		public BetaController(BetaService service)
		{
		}

		[HttpGet]
		public void Test() { }
	}
}