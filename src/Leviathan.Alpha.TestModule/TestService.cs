using Leviathan.Services.SDK;
using Leviathan.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Leviathan.Alpha.TestModule
{

    //public interface ITestService
    //{
    //    Task<TestServiceCatalog> Catalog();
    //}

    //public class TestServiceCatalog
    //{

    //}

    //[ServiceComponent(typeof(ITestService))]
    //public class TestService : LeviathanService, ITestService
    //{

    //    public override Task Initialize { get; }

    //    public TestService()
    //    {
    //        Initialize = InitializeAsync();
    //    }

    //    async Task InitializeAsync()
    //    {
    //        await base.Initialize;
    //    }
        
    //    public async Task<TestServiceCatalog> Catalog()
    //    {
    //        await Initialize;
    //        return new TestServiceCatalog
    //        {

    //        };
    //    }
    //}

    //[ApiComponent, Route("[controller]")]
    //public class TestServiceController : ServiceController<ITestService>
    //{
    //    public TestServiceController(ITestService service) : base(service)
    //    {
    //    }

    //    [HttpGet]
    //    public async Task<TestServiceCatalog> Catalog() => await Service.Catalog();
    //}
}