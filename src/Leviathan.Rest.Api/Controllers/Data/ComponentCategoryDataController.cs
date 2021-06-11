using Leviathan.DataAccess;
using Leviathan.Plugins;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Leviathan.Rest.Api.Controllers.Data {
	[Route("Data/[controller]")]
	[ApiController]
	public class ComponentCategoryDataController : CrudDataController<long, Category, IComponentCategoryData> {

		public ComponentCategoryDataController(IComponentCategoryData data) : base(data) { }
	}
}