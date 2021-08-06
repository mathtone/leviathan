using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.ModuleAlpha
{
	public class AlphaService
	{

	}

	public class AlphaController : ControllerBase
	{
		public AlphaController(AlphaService service)
		{
			;
		}

		[HttpGet]
		public void Test() { }

	}
}