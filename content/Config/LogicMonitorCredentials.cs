using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicMonitor.Cli.Config
{
	/// <summary>
	/// LogicMonitor credentials
	/// </summary>
	public class LogicMonitorCredentials
	{
		/// <summary>
		/// The LogicMonitor account
		/// For example, if your URL is https://example.logicmonitor.com/
		/// ... set to "example"
		/// </summary>
		public string Account { get; set; }

		/// <summary>
		/// The access Id.
		/// See https://www.logicmonitor.com/support/settings/users-and-roles/api-tokens/
		/// </summary>
		public string AccessId { get; set; }

		/// <summary>
		/// The access key.
		/// See https://www.logicmonitor.com/support/settings/users-and-roles/api-tokens/
		/// </summary>
		public string AccessKey { get; set; }

		/// <summary>
		/// Ensures that all values are set and are of the expected length
		/// Throws an exception if this is not the case
		/// </summary>
		internal void Validate()
		{
			// Create a list of issues
			var issues = new List<ConfigurationIssue>();

			// Account
			if (string.IsNullOrWhiteSpace(Account))
			{
				issues.Add(new ConfigurationIssue("Account is not set"));
			}

			// AccessId
			if (string.IsNullOrWhiteSpace(AccessId))
			{
				issues.Add(new ConfigurationIssue("AccessId is not set"));
			}

			// AccessKey
			if (string.IsNullOrWhiteSpace(AccessKey))
			{
				issues.Add(new ConfigurationIssue("AccessKey is not set"));
			}

			// Is everything OK?
			if (!issues.Any())
			{
				// Yes - return
				return;
			}
			// No

			throw new ConfigurationException(issues);
		}
	}
}