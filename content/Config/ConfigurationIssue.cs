namespace LogicMonitor.Cli.Config
{
	/// <summary>
	/// A configuration issue
	/// </summary>
	internal class ConfigurationIssue
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">The message</param>
		public ConfigurationIssue(string message) => Message = message;

		/// <summary>
		/// The message
		/// </summary>
		public string Message { get; set; }
	}
}