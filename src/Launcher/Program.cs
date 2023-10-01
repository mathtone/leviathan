using System.Diagnostics;

Console.WriteLine("Starting the System Host");
var webApiProcess = StartProcess("../../../../Leviathan.SystemHost.Api/bin/debug/net8.0/Leviathan.SystemHost.Api.exe", "--urls http://localhost:5050;https://localhost:5051");

Console.WriteLine("Starting the System Ui...");
var webUiProcess = StartProcess("../../../../Leviathan.SystemHost.Ui/bin/debug/net8.0/Leviathan.SystemHost.Ui.exe", "--urls http://localhost:7060;https://localhost:7061");

Console.WriteLine("All components are running. Press any key to shut down.");
Console.ReadKey();

Console.WriteLine("Shutting down the Web API and Blazor UI...");
TerminateProcess(webApiProcess);
TerminateProcess(webUiProcess);

Console.WriteLine("All components have been shut down. Exiting launcher.");

static Process StartProcess(string filename, string arguments) {
	var processStartInfo = new ProcessStartInfo {
		FileName = filename,
		Arguments = arguments,
		RedirectStandardOutput = true,
		UseShellExecute = false,
		CreateNoWindow = true
	};

	var process = new Process { StartInfo = processStartInfo };
	process.OutputDataReceived += (sender, e) => {
		if (!string.IsNullOrEmpty(e.Data))
			Console.WriteLine(e.Data);
	};
	process.Start();
	process.BeginOutputReadLine();

	return process;
}

static void TerminateProcess(Process process) {
	if (!process.HasExited) {
		process.Kill(true);
		process.WaitForExit();
	}
}