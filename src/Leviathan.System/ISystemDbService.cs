using System;
using System.Collections.Generic;

namespace Leviathan.System {

	//public interface ISystemDbService {
	//	ResetResult FactoryReset();
	//	void VerifyDatabase();
	//}


	//public class SystemService : ISystemDbService {
		
	//	protected SystemConfiguration Configuration { get; }
	//	protected ISystemDbData Data { get; }

	//	public SystemService(SystemConfiguration configuration, ISystemDbData data) {
	//		this.Configuration = configuration;
	//		this.Data = data;
	//	}

	//	public ResetResult FactoryReset() {
	//		if (Data.LocateDB(Configuration.DbName)) {
	//			Data.DropDB(Configuration.DbName);
	//		}
	//		//Data.CreateDB(Configuration.DbName);
	//		return new ResetResult();
	//	}

	//	public void VerifyDatabase() {
	//		if (!Data.LocateDB(Configuration.DbName)) {
	//			Data.CreateDB(Configuration.DbName);
	//		}
	//	}
	//}
}