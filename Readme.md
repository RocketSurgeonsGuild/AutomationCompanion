# Rocket Surgeons - Automation Companion

Every good Rocket Surgeon likes to be able to test and integrate their tools together easily.  

This library lets you integrate automations so that when your application starts up in debug mode you can ensure that all the services that are required are brought up using `docker up`.


# Status
<!-- badges -->
[![github-release-badge]][github-release]
[![github-license-badge]][github-license]
[![codecov-badge]][codecov]
<!-- badges -->

<!-- history badges -->
| Azure Pipelines | AppVeyor |
| --------------- | -------- |
| [![azurepipelines-badge]][azurepipelines] | [![appveyor-badge]][appveyor] |
| [![azurepipelines-history-badge]][azurepipelines-history] | [![appveyor-history-badge]][appveyor-history] |
<!-- history badges -->

<!-- nuget packages -->
| Package | NuGet | MyGet |
| ------- | ----- | ----- |
| Rocket.Surgery.Automation.Azurite | [![nuget-version-gb3fjp0hc+ea-badge]![nuget-downloads-gb3fjp0hc+ea-badge]][nuget-gb3fjp0hc+ea] | [![myget-version-gb3fjp0hc+ea-badge]![myget-downloads-gb3fjp0hc+ea-badge]][myget-gb3fjp0hc+ea] |
| Rocket.Surgery.Automation.DockerCompose | [![nuget-version-07gg/vicexsa-badge]![nuget-downloads-07gg/vicexsa-badge]][nuget-07gg/vicexsa] | [![myget-version-07gg/vicexsa-badge]![myget-downloads-07gg/vicexsa-badge]][myget-07gg/vicexsa] |
| Rocket.Surgery.Automation.Postgres | [![nuget-version-0c6zed99pgjw-badge]![nuget-downloads-0c6zed99pgjw-badge]][nuget-0c6zed99pgjw] | [![myget-version-0c6zed99pgjw-badge]![myget-downloads-0c6zed99pgjw-badge]][myget-0c6zed99pgjw] |
<!-- nuget packages -->

# Whats next?
TBD

<!-- generated references -->
[github-release]: https://github.com/RocketSurgeonsGuild/Testing/releases/latest
[github-release-badge]: https://img.shields.io/github/release/RocketSurgeonsGuild/Testing.svg?logo=github&style=flat "Latest Release"
[github-license]: https://github.com/RocketSurgeonsGuild/Testing/blob/master/LICENSE
[github-license-badge]: https://img.shields.io/github/license/RocketSurgeonsGuild/Testing.svg?style=flat "License"
[codecov]: https://codecov.io/gh/RocketSurgeonsGuild/Testing
[codecov-badge]: https://img.shields.io/codecov/c/github/RocketSurgeonsGuild/Testing.svg?color=E03997&label=codecov&logo=codecov&logoColor=E03997&style=flat "Code Coverage"
[azurepipelines]: https://rocketsurgeonsguild.visualstudio.com/Libraries/_build/latest?definitionId=1&branchName=master
[azurepipelines-badge]: https://img.shields.io/azure-devops/build/rocketsurgeonsguild/Libraries/1.svg?color=98C6FF&label=azure%20pipelines&logo=azuredevops&logoColor=98C6FF&style=flat "Azure Pipelines Status"
[azurepipelines-history]: https://rocketsurgeonsguild.visualstudio.com/Libraries/_build?definitionId=1&branchName=master
[azurepipelines-history-badge]: https://buildstats.info/azurepipelines/chart/rocketsurgeonsguild/Libraries/1?includeBuildsFromPullRequest=false "Azure Pipelines History"
[appveyor]: https://ci.appveyor.com/project/RocketSurgeonsGuild/testing
[appveyor-badge]: https://img.shields.io/appveyor/ci/RocketSurgeonsGuild/testing.svg?color=00b3e0&label=appveyor&logo=appveyor&logoColor=00b3e0&style=flat "AppVeyor Status"
[appveyor-history]: https://ci.appveyor.com/project/RocketSurgeonsGuild/testing/history
[appveyor-history-badge]: https://buildstats.info/appveyor/chart/RocketSurgeonsGuild/testing?includeBuildsFromPullRequest=false "AppVeyor History"
[nuget-gb3fjp0hc+ea]: https://www.nuget.org/packages/Rocket.Surgery.Automation.Azurite/
[nuget-version-gb3fjp0hc+ea-badge]: https://img.shields.io/nuget/v/Rocket.Surgery.Automation.Azurite.svg?color=004880&logo=nuget&style=flat-square "NuGet Version"
[nuget-downloads-gb3fjp0hc+ea-badge]: https://img.shields.io/nuget/dt/Rocket.Surgery.Automation.Azurite.svg?color=004880&logo=nuget&style=flat-square "NuGet Downloads"
[myget-gb3fjp0hc+ea]: https://www.myget.org/feed/rocket-surgeons-guild/package/nuget/Rocket.Surgery.Automation.Azurite
[myget-version-gb3fjp0hc+ea-badge]: https://img.shields.io/myget/rocket-surgeons-guild/vpre/Rocket.Surgery.Automation.Azurite.svg?label=myget&color=004880&logo=nuget&style=flat-square "MyGet Pre-Release Version"
[myget-downloads-gb3fjp0hc+ea-badge]: https://img.shields.io/myget/rocket-surgeons-guild/dt/Rocket.Surgery.Automation.Azurite.svg?color=004880&logo=nuget&style=flat-square "MyGet Downloads"
[nuget-07gg/vicexsa]: https://www.nuget.org/packages/Rocket.Surgery.Automation.DockerCompose/
[nuget-version-07gg/vicexsa-badge]: https://img.shields.io/nuget/v/Rocket.Surgery.Automation.DockerCompose.svg?color=004880&logo=nuget&style=flat-square "NuGet Version"
[nuget-downloads-07gg/vicexsa-badge]: https://img.shields.io/nuget/dt/Rocket.Surgery.Automation.DockerCompose.svg?color=004880&logo=nuget&style=flat-square "NuGet Downloads"
[myget-07gg/vicexsa]: https://www.myget.org/feed/rocket-surgeons-guild/package/nuget/Rocket.Surgery.Automation.DockerCompose
[myget-version-07gg/vicexsa-badge]: https://img.shields.io/myget/rocket-surgeons-guild/vpre/Rocket.Surgery.Automation.DockerCompose.svg?label=myget&color=004880&logo=nuget&style=flat-square "MyGet Pre-Release Version"
[myget-downloads-07gg/vicexsa-badge]: https://img.shields.io/myget/rocket-surgeons-guild/dt/Rocket.Surgery.Automation.DockerCompose.svg?color=004880&logo=nuget&style=flat-square "MyGet Downloads"
[nuget-0c6zed99pgjw]: https://www.nuget.org/packages/Rocket.Surgery.Automation.Postgres/
[nuget-version-0c6zed99pgjw-badge]: https://img.shields.io/nuget/v/Rocket.Surgery.Automation.Postgres.svg?color=004880&logo=nuget&style=flat-square "NuGet Version"
[nuget-downloads-0c6zed99pgjw-badge]: https://img.shields.io/nuget/dt/Rocket.Surgery.Automation.Postgres.svg?color=004880&logo=nuget&style=flat-square "NuGet Downloads"
[myget-0c6zed99pgjw]: https://www.myget.org/feed/rocket-surgeons-guild/package/nuget/Rocket.Surgery.Automation.Postgres
[myget-version-0c6zed99pgjw-badge]: https://img.shields.io/myget/rocket-surgeons-guild/vpre/Rocket.Surgery.Automation.Postgres.svg?label=myget&color=004880&logo=nuget&style=flat-square "MyGet Pre-Release Version"
[myget-downloads-0c6zed99pgjw-badge]: https://img.shields.io/myget/rocket-surgeons-guild/dt/Rocket.Surgery.Automation.Postgres.svg?color=004880&logo=nuget&style=flat-square "MyGet Downloads"
<!-- generated references -->

<!-- nuke-data
github:
  owner: RocketSurgeonsGuild
  repository: Testing
azurepipelines:
  account: rocketsurgeonsguild
  teamproject: Libraries
  builddefinition: 1
appveyor:
  account: RocketSurgeonsGuild
  build: testing
myget:
  account: rocket-surgeons-guild
-->