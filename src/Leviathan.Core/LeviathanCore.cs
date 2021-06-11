using Leviathan.Hardware;
using Leviathan.Plugins;
using Leviathan.System;
using System;

namespace Leviathan.Core {

	public interface ILeviathanCore : ILeviathanCorePartners {
		void Start();
		void Stop();
		void Restart();
		CoreStatus GetStatus();
	}

	public interface ILeviathanCorePartners {
		SystemConfiguration Config { get; }
		ISystemDbData SystemDbData { get; }
		IComponentData PluginData { get; }
		IComponentCategoryData PluginCategoryData { get; }
		//IHardwareService Hardware { get; }
	}

	public record CoreStatus {
		public bool DbCreated { get; set; }
		public bool Initialized { get; set; }
		public bool Running { get; set; }
	}

	public class LeviathanCore : ILeviathanCore {

		CoreStatus Status { get; } = new CoreStatus();

		public SystemConfiguration Config { get; }
		public ISystemDbData SystemDbData { get; }
		public IComponentData PluginData { get; }
		public IComponentCategoryData PluginCategoryData { get; }

		public LeviathanCore(
			SystemConfiguration config,
			ISystemDbData systemDb,
			IComponentData pluginData,
			IComponentCategoryData pluginCategoryData
			) {

			this.Config = config;
			this.SystemDbData = systemDb;
			this.PluginData = pluginData;
			this.PluginCategoryData = pluginCategoryData;
		}

		public void Start() {
			this.Status.DbCreated = SystemDbData.LocateDB(Config.DbName);
			this.Status.Running = true;
		}

		public void Stop() {
			this.Status.Running = true;
		}

		public void Restart() {
			Stop();
			Start();
		}

		public CoreStatus GetStatus() => this.Status with
		{
			DbCreated = SystemDbData.LocateDB(Config.DbName)
		};
	}
}