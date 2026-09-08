[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

[![Codacy Badge](https://app.codacy.com/project/badge/grade/LogicMonitor.Cli)](https://app.codacy.com/gh/panoramicdata/LogicMonitor.Cli/dashboard)

# LogicMonitor.Cli

Nuget package for dotnet new logicmonitor

## Build instructions

To build, from the root directory, type:
> dotnet pack content\LogicMonitor.Cli.csproj --configuration Release

The package version is derived automatically from git history by [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning) (see `version.json`).

## Publishing

Run `.\Publish.ps1` from the root directory. It tags the current commit with the Nerdbank.GitVersioning-computed version and pushes the tag, which triggers the CI workflow to build, pack and push the package to nuget.org.

## Installation instructions
To install the template, use:
``` powershell
dotnet new install LogicMonitor.Cli
```

To create a new project using the template, use:

``` powershell
dotnet new logicmonitor --name MyProject.MyNameSpace
```

or

``` powershell
dotnet new logicmonitor
```