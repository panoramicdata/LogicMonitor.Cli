[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

[![Codacy Badge](https://app.codacy.com/project/badge/grade/LogicMonitor.Cli)](https://app.codacy.com/gh/panoramicdata/LogicMonitor.Cli/dashboard)

# LogicMonitor.Cli

Nuget package for dotnet new logicmonitor

## Build instructions

To build, from the root directory (and already having installed the latest version of nuget), type:
> nuget pack .\LogicMonitor.Cli.nuspec -NoDefaultExcludes -Exclude .vs -Exclude .suo

## Upload instructions
You can then upload it to [https://www.nuget.org/packages/manage/upload](https://www.nuget.org/packages/manage/upload)

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