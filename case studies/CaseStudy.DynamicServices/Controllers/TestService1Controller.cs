using CaseStudy.DynamicService.TestModule1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.DynamicServices.Controllers
{
    [Route("api/[controller]")]
    public class TestService1Controller : Controller
    {

        public TestService1Controller(ITestService1 service)
        {
            ;
        }

        [HttpGet]
        public bool Get() => true;
    }
}