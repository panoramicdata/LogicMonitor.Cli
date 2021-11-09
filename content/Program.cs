using LogicMonitor.Cli.Config;
using Serilog;
using Serilog.Debugging;

namespace LogicMonitor.Cli
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			SelfLog.Enable(msg => Console.Error.WriteLine(msg));

			try
			{
				await Host.CreateDefaultBuilder(args)
					.ConfigureServices((context, services) =>
					{
						services
							.AddHostedService<Application>()
							.AddOptions()
							.Configure<Configuration>(context.Configuration.GetSection("Configuration"))
							;
					})
					.UseSerilog((context, _, config) => config.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext())
					.Build()
					.RunAsync();
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.ToString());
				var dumpPath = Path.GetTempPath();
				var dumpFile = $"{ThisAssembly.AssemblyName}-Error-{Guid.NewGuid()}.txt";
				var dumpFileFullPath = Path.Combine(dumpPath, dumpFile);
				File.WriteAllText(dumpFileFullPath, ex.ToString());
				Console.WriteLine($"Exception written to file: {dumpFileFullPath}");
			}
		}
	}
}