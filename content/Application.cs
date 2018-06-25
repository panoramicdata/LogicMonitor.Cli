using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogicMonitor.Api;
using LogicMonitor.Api.Collectors;
using LogicMonitor.Api.Settings;
using LogicMonitor.Cli.Config;
using LogicMonitor.Api.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LogicMonitor.Cli
{
	/// <summary>
	/// The main application
	/// </summary>
	internal class Application
	{
		/// <summary>
		/// Configuration
		/// </summary>
		private readonly Configuration _config;

		/// <summary>
		/// The PortalClient to use for LogicMonitor interaction
		/// </summary>
		private readonly PortalClient _portalClient;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<Application> _logger;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="options"></param>
		public Application(
			IOptions<Configuration> options,
			ILogger<Application> logger)
		{
			// Store the config
			_config = options.Value;

			// Validate the credentials
			_config.LogicMonitorCredentials.Validate();

			// Create a portal client
			_portalClient = new PortalClient(
				_config.LogicMonitorCredentials.Account,
				_config.LogicMonitorCredentials.AccessId,
				_config.LogicMonitorCredentials.AccessKey
			);

			// Create a logger
			_logger = logger;
		}

		public async Task Run()
		{
			// Use _logger for logging
			_logger.LogInformation($"Application start.  Setting1 is set to {_config.Setting1}");

			// Use asynchronous calls to _portalClient to interact with the portal
			var accountSettings = await _portalClient.GetAsync<AccountSettings>();
			_logger.LogInformation($"{_portalClient.AccountName} has {accountSettings.DeviceCount} devices.");

			// Use GetAllAsync with filters to query down collectors
			var collectors = await _portalClient.GetAllAsync(new Filter<Collector>
			{
				FilterItems = new List<FilterItem<Collector>>
				{
					new Eq<Collector>(nameof(Collector.IsDown), true)
				}
			});

			// Write some information about down collectors
			if (collectors.Count == 0)
			{
				_logger.LogInformation("You have no down collectors.");
			}
			else
			{
				_logger.LogWarning($"The following collectors are down:\r\n{string.Join("\r\n", collectors.Select(c => $" - {c.Description}"))}");
			}

		}
	}
}