using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace LogicMonitor.Cli.Config;

/// <summary>
/// A configuration exception
/// </summary>
[Serializable]
internal class ConfigurationException : Exception
{
	public ConfigurationException()
	{
	}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="issues">The issues</param>
	public ConfigurationException(List<ConfigurationIssue> issues)
	{
		Issues = issues.AsReadOnly();
	}

	public ConfigurationException(string message) : base(message)
	{
	}

	public ConfigurationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	/// <summary>
	/// The issues
	/// </summary>
	/// <returns></returns>
	public ReadOnlyCollection<ConfigurationIssue> Issues { get; }

	/// <inheritdoc />
	public override string ToString() => $"Configuration issues:\r\n{Issues.Select(i => i.Message + "\r\n")}";
}