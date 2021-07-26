using Leviathan.Alpha.FileDataSystem;
using Leviathan.GPIO;
using Leviathan.OneWire;
using Leviathan.PCA9685;
using Leviathan.SystemConfiguration.SDK;
using Leviathan.SystemProfiles.Basic;
using Leviathan.SystemProfiles.PostgreSQL;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.RoboTank {

	[SystemProfile, RequireProfile(typeof(BasicProfile))]
	public class RoboTankProfile : SystemProfileComponent {

		ILogger<RoboTankProfile> _log;
		IFileDataSystemService _fileData;
		IServiceProvider _services;

		static readonly int[] GPIOPins = new[] { 9, 10, 22, 15, 14, 23, 24, 25, 16, 6, 5, 7, 11, 8, 12, 13, 17, 27, 19, 20, 26, 21 };
		static readonly int[] PWMPins = new[] { 15, 14, 13, 12, 11, 10, 9, 8, 0, 1, 2, 3, 4, 5, 6, 7 };

		public RoboTankProfile(ILogger<RoboTankProfile> log, IFileDataSystemService fileDataSystem, IServiceProvider services) {
			(_log, _fileData, _services) = (log, fileDataSystem, services);
		}

		public override async Task Apply() {
			await Task.CompletedTask;

			//var acPorts = GPIOPins[0..16].Select(i => CreateChannel(typeof(GpioOnOffChannel), new {
			//	Pin = i,
			//	Mode = 1
			//})).ToArray();
		}

		//static object CreateChannel(Type type, object channelData) {
		//	var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

		//	foreach (var c in constructors) {

		//		var p = c.GetParameters();
		//		var t = p[0].ParameterType;
		//		var json = JsonConvert.SerializeObject(channelData);
		//		var input = JsonConvert.DeserializeObject(json, t);

		//		return Activator.CreateInstance(type, input);
		//	}

		//	throw new Exception("Could not create channel");
		//}
	}
}