using Leviathan.Alpha.Configuration;
using Leviathan.Alpha.Npgsql;
using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.RNG;
using Leviathan.System;
using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.Alpha.FactoryReset {

	public interface IFactoryResetService {
		Task<DestructCode> BeginSelfDestruct();
		Task FactoryReset(string destructCode);
		event EventHandler SelfDestructBegun;
		event EventHandler SelfDestructComplete;
	}

	public record DestructCode {
		public string Code { get; init; }
		public TimeSpan Countdown { get; init; }
	}

	public class FactoryResetService : IFactoryResetService {

		IRandom Random { get; }
		IDataSystemService<NpgsqlConnection> DataSystem { get; }
		ISystemConfigProvider<AlphaSystemConfiguration> ConfigProvider { get; }
		AlphaSystemConfiguration Config => ConfigProvider?.Config;

		DestructCode DestructCode { get; set; }
		Task SelfDestruct { get; set; }
		CancellationTokenSource cancelSource;
		NpgsqlConnection SystemConnection() => DataSystem.SystemConnection();
		
		public event EventHandler SelfDestructBegun;
		public event EventHandler SelfDestructComplete;

		public FactoryResetService(ISystemConfigProvider<AlphaSystemConfiguration> config, IDataSystemService<NpgsqlConnection> dataSystem, IRandom random) {
			this.ConfigProvider = config;
			this.DataSystem = dataSystem;
			this.Random = random;
		}

		public async Task FactoryReset(string destructCode) {
			if (this.DestructCode?.Code != null && this.DestructCode.Code == destructCode) {
				//delete da

				(_ = SelfDestructBegun)?.Invoke(this, new EventArgs());
				cancelSource?.Cancel();
				await (SelfDestruct ?? Task.CompletedTask);
				await SystemConnection().UsedAsync(c => c
					.CreateCommand(DROP)
					.WithTemplate("@db-name", Config.StartupInfo.DatabaseInfo.InstanceDbName)
					.ExecuteNonQueryAsync()
				);
				(_ = SelfDestructComplete)?.Invoke(this, new EventArgs());
				await Task.CompletedTask;

			}
			else {
				throw new Exception("Invald or expired descruct code.");
			}
		}

		public async Task<DestructCode> BeginSelfDestruct() {
			cancelSource?.Cancel();
			await (SelfDestruct ?? Task.CompletedTask);
			this.DestructCode = new DestructCode {
				Code = Random.GetString(24),
				Countdown = TimeSpan.FromSeconds(15)
			};
			this.SelfDestruct = BeginSelfDestructAsync();
			return this.DestructCode;
		}


		public async Task BeginSelfDestructAsync() {
			this.cancelSource?.Cancel();
			await (SelfDestruct ?? Task.CompletedTask);
			this.cancelSource = new CancellationTokenSource();
			try {
				await Task.Delay(DestructCode.Countdown, this.cancelSource.Token);
			}
			catch (TaskCanceledException) {
				;
			}
			DestructCode = null;
		}

		static readonly string DROP = LoadLocal("Queries.DropDB.sqlx");
	}
}