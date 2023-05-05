# LogicMonitor.Cli

Nuget package for dotnet new logicmonitor

## Build instructions

To build, from the root directory (and already having installed the latest version of nuget), type:
> nuget pack .\LogicMonitor.Cli.nuspec -NoDefaultExcludes -Exclude .vs -Exclude .suo

## Upload instructions
You can then upload it to [https://www.nuget.org/packages/manage/upload](https://www.nuget.org/packages/manage/upload)

## Installation instructions
To install the template, use:
> dotnet new install LogicMonitor.Cli

To create a new project using the template, use:
> dotnet new LogicMonitor

or

> dotnet new LogicMonitor --name MyProject.MyNameSpace
