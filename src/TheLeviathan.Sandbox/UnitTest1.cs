using Leviathan.Components.Data;
using Leviathan.Services.Data;
using System;
using Xunit;

namespace TheLeviathan.Sandbox {
	public class UnitTest1 {

		[Fact]
		public void Test1() {
			var data = new {
				Assemblies = new[] {
					new AssemblyRecord{Id=1},
				},
				Components = new[] {
					new ComponentRecord{Id=1, AssemblyId=1},
				},
				Services = new[] {
					new ServiceRecord{Id=1, ComponentId=1},
					new ServiceRecord{Id=1, ComponentId=1},
					new ServiceRecord{Id=1, ComponentId=1},
				}
			};
			;
		}
	}
}