using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;
using Leviathan.Services.SDK;
using Leviathan.WebApi.SDK;

namespace Leviathan.Alpha.Api {
	public class ModularControllerRouteConvention : IControllerModelConvention {
		public void Apply(ControllerModel controller) {
			var attr = controller.ControllerType.GetCustomAttribute<ApiComponentAttribute>();
			if (attr != null) {
				controller.Selectors.Add(new SelectorModel {
					AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{controller.ControllerName}")),
				});
			}
		}
	}
}