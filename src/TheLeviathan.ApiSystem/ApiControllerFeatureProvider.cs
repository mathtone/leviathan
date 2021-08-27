using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using System.Reflection;

namespace TheLeviathan.ApiSystem {
	public class ApiControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature> {

		IApiControllersService _controllers;

		public ApiControllerFeatureProvider(IApiControllersService controllers) {
			_controllers = controllers;
		}

		public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
			foreach (var type in _controllers.ControllerTypes()) {
				feature.Controllers.Add(type.Type.GetTypeInfo());
			}
		}
	}
}
