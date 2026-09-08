using FluentValidation;

namespace LogicMonitor.Cli.Config;

/// <summary>
/// Validates a <see cref="Configuration"/>
/// </summary>
public class ConfigurationValidator : AbstractValidator<Configuration>
{
	/// <summary>
	/// Constructor
	/// </summary>
	public ConfigurationValidator()
	{
		RuleFor(x => x.LogicMonitorClientOptions)
			.NotNull().WithMessage($"{nameof(Configuration.LogicMonitorClientOptions)} must not be null");

		RuleFor(x => x.LogicMonitorClientOptions.Account)
			.NotEmpty().WithMessage($"{nameof(Configuration.LogicMonitorClientOptions.Account)} must not be empty");

		RuleFor(x => x.LogicMonitorClientOptions.AccessId)
			.NotEmpty().WithMessage($"{nameof(Configuration.LogicMonitorClientOptions.AccessId)} must not be empty");

		RuleFor(x => x.LogicMonitorClientOptions.AccessKey)
			.NotEmpty().WithMessage($"{nameof(Configuration.LogicMonitorClientOptions.AccessKey)} must not be empty");

		RuleFor(x => x.Setting1)
			.NotEmpty().WithMessage($"{nameof(Configuration.Setting1)} must not be empty");
	}
}
