using LogicMonitor.Api;
using LogicMonitor.Api.Collectors;
using LogicMonitor.Api.Settings;
using LogicMonitor.Cli.Config;
using LogicMonitor.Api.Filters;
using Microsoft.Extensions.Options;

namespace LogicMonitor.Cli
{
	/// <summary>
	/// The main application
	/// </summary>
	internal class Application : BackgroundService
	{
		/// <summary>
		/// Configuration
		/// </summary>
		private readonly Configuration _config;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<Application> _logger;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="options"></param>
		/// <param name="loggerFactory"></param>
		public Application(
			IOptions<Configuration> options,
			ILoggerFactory loggerFactory)
		{
			// Store the config
			_config = options.Value;

			_config.LogicMonitorClientOptions.Logger = loggerFactory.CreateLogger<LogicMonitorClient>();

			// Create a logger
			_logger = loggerFactory.CreateLogger<Application>();
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			// Use _logger for logging
			_logger.LogInformation("Application start.  Setting1 is set to {setting1}", _config.Setting1);

			using var logicMonitorClient = new LogicMonitorClient(_config.LogicMonitorClientOptions);

			// Use asynchronous calls to _portalClient to interact with the portal
			var accountSettings = await logicMonitorClient
				.GetAsync<AccountSettings>(cancellationToken)
				.ConfigureAwait(false);
			_logger.LogInformation("{accountName} has {deviceCount} devices.", logicMonitorClient.AccountName, accountSettings.DeviceCount);

			// Use GetAllAsync with filters to query down collectors
			var collectors = await logicMonitorClient.GetAllAsync(new Filter<Collector>
			{
				FilterItems = new List<FilterItem<Collector>>
				{
					new Eq<Collector>(nameof(Collector.IsDown), true)
				}
			}, cancellationToken).ConfigureAwait(false);

			// Write some information about down collectors
			if (collectors.Count == 0)
			{
				_logger.LogInformation("You have no down collectors.");
			}
			else
			{
				_logger.LogWarning("The following collectors are down:\r\n{downCollectorString}", string.Join("\r\n", collectors.Select(c => $" - {c.Description}")));
			}
		}
	}
}