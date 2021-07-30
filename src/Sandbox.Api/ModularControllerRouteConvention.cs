using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace Sandbox.Api {
	public class ModularControllerRouteConvention : IControllerModelConvention {
		public void Apply(ControllerModel controller) {
			var type = controller.ControllerType;
			var attr = type.GetCustomAttribute<ApiComponentAttribute>();
			if (attr != null) {
				controller.Selectors.Add(new SelectorModel {
					AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{attr.ModuleName}/{controller.ControllerName}")),
				});
			}
		}
	}
}
