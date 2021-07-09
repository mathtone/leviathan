using CaseStudy.DynamicService.SDK;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CaseStudy.DynamicService.TestModule1 {
	public interface ITestService1 { }

	[SingletonService(typeof(ITestService1))]
	public class TestService1 : ITestService1 {

	}

	public interface ITestService2 { }

	[SingletonService(typeof(ITestService2))]
	public class TestService2 : ITestService2 {

	}

	[ApiComponent]
	public class TestService1Controller : Controller {
		public TestService1Controller(ITestService1 service) { }

		[HttpGet]
		public bool Get() => true;
	}

	[ApiComponent]
	public class TestService2Controller : Controller {
		public TestService2Controller(ITestService2 service) {
			;
		}

		[HttpGet]
		public bool Get() => true;
	}
}