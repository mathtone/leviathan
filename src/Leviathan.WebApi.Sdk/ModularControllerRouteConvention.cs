using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace Leviathan.WebApi.Sdk {
	public class ModularControllerRouteConvention : IControllerModelConvention {
		public void Apply(ControllerModel controller) {
			controller.Selectors.Clear();
			var attr = controller.ControllerType.GetCustomAttribute<ApiComponentAttribute>();
			if (attr != null) {
				controller.Selectors.Add(new SelectorModel {
					AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{attr.ModuleName}/{controller.ControllerName}")),
				});
			}
		}
	}
	//public class ControllerRouteConvention : IControllerModelConvention {

	//	public ControllerRouteConvention(IConfiguration configuration) {
	//		;
	//	}

	//	public void Apply(ControllerModel controller) {
	//		controller.Selectors.Clear();
	//		controller.Selectors.Add(new SelectorModel {
	//			AttributeRouteModel = new AttributeRouteModel(new RouteAttribute("api/[controller]")),
	//		});
	//	}
	//}

}