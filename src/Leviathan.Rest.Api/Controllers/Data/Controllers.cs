using Leviathan.DataAccess;
using Leviathan.Plugins;
using Leviathan.SDK;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Leviathan.Rest.Api.Controllers.Data {

	[ApiController, Route("Data/[controller]")]
	public class ComponentCategoriesController : CrudListDataController<long, Category, IComponentCategoryData> {
		public ComponentCategoriesController(IComponentCategoryData data) : base(data) { }
	}

	[ApiController, Route("Data/[controller]")]
	public class DeviceDriversController : CrudListDataController<long, ComponentRecord, IComponentData> {
		public DeviceDriversController(IComponentData data) : base(data) { }
	}
}