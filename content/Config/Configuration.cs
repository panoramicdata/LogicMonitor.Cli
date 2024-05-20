using FluentValidation;
using LogicMonitor.Api;

namespace LogicMonitor.Cli.Config;

/// <summary>
/// Application configuration, loaded from an appsettings.json file upon execution
/// You can modify/extend this class and provide your own settings
/// </summary>
public class Configuration
{
	/// <summary>
	/// LogicMonitor credentials
	/// </summary>
	public required LogicMonitorClientOptions LogicMonitorClientOptions { get; set; } = new();

	/// <summary>
	/// Remove or modify this setting as needed.
	/// Provided as a first example
	/// </summary>
	public required string Setting1 { get; set; } = string.Empty;

	public void Validate()
	{
		var validator = new ConfigurationValidator();
		var result = validator.Validate(this);

		if (!result.IsValid)
		{
			throw new ValidationException($"Validation failed: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");
		}
	}
}
