using LogicMonitor.Api;
using LogicMonitor.Cli.Config;
using Xunit;

namespace LogicMonitor.Cli.Tests;

public class ConfigurationValidatorTests
{
	private static Configuration CreateValidConfiguration() => new()
	{
		LogicMonitorClientOptions = new LogicMonitorClientOptions
		{
			Account = "account",
			AccessId = "access-id",
			AccessKey = "access-key"
		},
		Setting1 = "value"
	};

	[Fact]
	public void Validate_WithAllFieldsPopulated_DoesNotThrow()
	{
		var configuration = CreateValidConfiguration();

		var exception = Record.Exception(configuration.Validate);

		Assert.Null(exception);
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	public void Validate_WithMissingAccount_ThrowsValidationException(string? account)
	{
		var configuration = CreateValidConfiguration();
		configuration.LogicMonitorClientOptions.Account = account!;

		Assert.Throws<FluentValidation.ValidationException>(configuration.Validate);
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	public void Validate_WithMissingSetting1_ThrowsValidationException(string? setting1)
	{
		var configuration = CreateValidConfiguration();
		configuration.Setting1 = setting1!;

		Assert.Throws<FluentValidation.ValidationException>(configuration.Validate);
	}
}
