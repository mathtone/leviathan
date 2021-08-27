using Leviathan.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace TheLeviathan.ApiSystem {
	public class ModularControllerRouteConvention : IControllerModelConvention {
		public void Apply(ControllerModel controller) {
			var attr = controller.ControllerType.GetCustomAttribute<ApiComponentAttribute>();
			if (attr != null) {
				controller.Selectors.Add(new SelectorModel {
					AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{attr.ModuleName}/{controller.ControllerName}")),
				});
			}
		}
	}
}
