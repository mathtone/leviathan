using Leviathan.DataAccess;
using Leviathan.Plugins;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Leviathan.Rest.Api.Controllers.Data {

	[ApiController, Route("Data/[controller]")]
	public class ComponentCategoriesController : CrudListDataController<long, Category, IComponentCategoryData> {
		public ComponentCategoriesController(IComponentCategoryData data) : base(data) { }
	}

	[ApiController, Route("Data/[controller]")]
	public class ComponentsController : CrudListDataController<long, ComponentRecord, IComponentData> {
		public ComponentsController(IComponentData data) : base(data) { }
	}
}