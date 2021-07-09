using CaseStudy.DynamicService.TestModule1;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.DynamicServices.Controllers
{
    [Route("api/[controller]")]
    public class TestService2Controller : Controller
    {

        public TestService2Controller(ITestService2 service)
        {
            ;
        }

        [HttpGet]
        public bool Get() => true;
    }
}