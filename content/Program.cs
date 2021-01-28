using LogicMonitor.Cli.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogicMonitor.Cli
{
	public static class Program
	{
		private static readonly CancellationTokenSource _cancellationTokenSource = new();

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

				// Establish an event handler to process key press events.
				Console.CancelKeyPress += CancelEventHandler;

				await (application ?? throw new ConfigurationException("Could not create an application.  Check dependency injection."))
					.RunAsync(_cancellationTokenSource.Token)
					.ConfigureAwait(false);
				return (int)ExitCode.Ok;
			}
			catch (OperationCanceledException)
			{
				// Cleanly handled.
				Console.WriteLine("Quitting.");
				return (int)ExitCode.Cancelled;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return (int)ExitCode.UnhandledException;
			}
		}

		private static void CancelEventHandler(object? sender, ConsoleCancelEventArgs args)
		{
			Console.WriteLine($"Key pressed: {args.SpecialKey}");

			// Set the Cancel property to true to prevent the process from terminating.
			args.Cancel = true;

			_cancellationTokenSource?.Cancel();
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
			services
			.AddLogging(b =>
				{
					b.AddConsole();
					b.AddDebug();
				})
			.AddOptions()
			.Configure<Configuration>(c => configuration.GetSection("Configuration").Bind(c))
			.AddTransient<Application>();
	}
}
