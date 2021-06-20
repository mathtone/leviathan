using System;
using System.Reflection;
using System.Threading.Tasks;


namespace Leviathan.Components {
	public interface ISystemProfileComponent {
		Task Apply();
	}

	public abstract class SystemProfileComponent : ISystemProfileComponent {
		
		IComponentsService Components { get; }
		
		public SystemProfileComponent(IComponentsService components) {
			this.Components = components;
		}

		protected async virtual Task ApplyRequired() {
			var required = this.GetType().GetCustomAttribute<RequireProfileAttribute>();
			if(required != null) {
				foreach (var type in required.Types) {
					await this.Components.Activate<ISystemProfileComponent>(type).Apply();
				};
			}
		}

		public abstract Task Apply();
	}

	public class Label {
		public string Name { get; set; }
		public string Description { get; set; }
		public Label(string name, string description=default) {
			this.Name = name;
			this.Description = description;
		}
	}


	[AttributeUsage(AttributeTargets.Assembly)]
	public class LeviathanPluginAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Class)]
	public class RequireProfileAttribute : Attribute {
		public Type[] Types { get; init; }
		public RequireProfileAttribute(params Type[] profileTypes) {
			this.Types = profileTypes;
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class SystemProfileAttribute : LeviathanComponentAttribute {
		public SystemProfileAttribute(string name, string description) : base(name, description, ComponentCategory.SystemProfile) {
		}
	}

}
