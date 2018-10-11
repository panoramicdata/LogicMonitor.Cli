using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LogicMonitor.Cli
{
	public static class Program
	{
		public static async Task<int> Main(string[] args)
		{
			try
			{
				// Create the service collection passing in the configuration
				var serviceCollection = new ServiceCollection();
				var configurationRoot = BuildConfig(args);
				ConfigureServices(serviceCollection, configurationRoot);
				var serviceProvider = serviceCollection.BuildServiceProvider();

				var application = serviceProvider.GetService<Application>();
				await application.Run().ConfigureAwait(false);
				return 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return 1;
			}
		}

		private static IConfigurationRoot BuildConfig(string[] args)
		{
			var appsettingsFilename = "appsettings.json";
			// If specifying any command line arguments, the first argument must be the path to the appsettings.json file ConfigFile: xxx
			if (args.Length > 0)
			{
				appsettingsFilename = args[0];
			}

			// Convert appsettingsFilename to absolute path for the ConfigurationBuilder to be able to find it
			appsettingsFilename = Path.GetFullPath(appsettingsFilename);

			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(appsettingsFilename, false, false)
				.Build();
		}

		private static void ConfigureServices(IServiceCollection services, IConfiguration configuration) =>
			// Add logging
			services.AddSingleton(
				new LoggerFactory()
					.AddConsole()
					.AddDebug(LogLevel.Debug)
			)
			.AddLogging()
			.AddOptions()
			.Configure<Config.Configuration>(c => configuration.GetSection("Configuration").Bind(c))
			.AddTransient<Application>();
	}
}
