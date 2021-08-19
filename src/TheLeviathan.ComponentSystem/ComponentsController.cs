using Leviathan.Components.Sdk;
using Leviathan.WebApi;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLeviathan.ComponentSystem {

	[ApiComponent("system")]
	public class ComponentsController : ControllerBase {

		IComponentsService _service;
		public ComponentsController(IComponentsService service) {
			_service = service;
		}
		[HttpGet]
		public IEnumerable<ComponentListing> List() => CreateListing(
			_service.GetLeviathanComponents<LeviathanComponentAttribute>,
			c => new ComponentListing {
				Id = c.Id,
				AssemblyName = c.Type.Assembly.FullName,
				TypeName = c.Type.Name,
				ComponentTypes = c.ComponentAttributes.Select(a => a.ComponentTypeDescription).ToArray()
			}
		);

		IEnumerable<L> CreateListing<T, L>(IEnumerable<T> source, Func<T, L> transform) =>
			source.Select(transform);

		IEnumerable<L> CreateListing<T, L>(Func<IEnumerable<T>> source, Func<T, L> transform) =>
			CreateListing(source(), transform);
	}

	public class ComponentListing {
		public int Id { get; init; }
		public string TypeName { get; init; }
		public string AssemblyName { get; init; }
		public string[] ComponentTypes { get; init; }
	}
}