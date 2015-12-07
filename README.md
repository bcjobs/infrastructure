## Infra
This repository contains a collection of _infrastructure_ projects that should be added as a _submodule_ inside of another repository.
## Setup
* Go do a Git repository where you'd like to add these _infrastructure_ projects.
* Add this repository as a submodule:
  * `git submodule add https://github.com/bcjobs/infra.git Infra`
* Open Solution in Visual Studio.
* Right-click solution name and select:
  * `Add` --> `New Solution Folder`
* Name the solution folder `Infra`
* One by one, add all of the projects in the submodule into the solution.
* If you encounter any problems with Entity Framework dependencies while trying to build one of these added projects, uninstall and re-install Entity Framework Nuget packages.
* Add references too all projects in `Infra` folder to any project that will need them (e.g. web project).
 
## Autofac Config
* Register all IHandlers in Infra and web project with Autofac
* Example: https://github.com/bcjobs/demo/blob/master/Demo.Web/App_Start/AutofacConfig.cs
 
## Identity
* Create OwinConfig:
  * In `App_Start` folder of web project, add an OwinConfig class:
  * https://github.com/bcjobs/demo/blob/master/Demo.Web/App_Start/OwinConfig.cs
* Register OwinConfig:
  * Add the following to `appSettings` in `web.config`: `<add key="owin:AppStartup" value="{namespace}.OwinConfig" />`
  * Where `{namespace}` is the namespace to `OwinConfig`
* Optionally add the following options in `web.config` `appSettings`:
  * `<add key="authentications:LoginPath" value="/authentication/login" />`
  * `<add key="authentications:IgnoreLoginPathIfRequestUrlStartsWithSegment" value="/api" />`<br />
    _This is very important to add if `authentications:LoginPath` is added, otherwise 401 responses from the API will result in undesirable 302 redirects._
  * `<add key="authentications:CookieName" value="auth-token" />`
* Add the following NuGet packages to the web project:
  * Microsoft.AspNet.Identity.EntityFramework
  * Microsoft.AspNet.Identity.OWIN
* Implement Infra.Authentications.IUserLookup
  * Example: https://github.com/bcjobs/demo/blob/master/Demo.Web/Services/UserLookup.cs
* Implement Infra.Authentications.IAuthenticator
  * Example: https://github.com/bcjobs/demo/blob/master/Demo.Web/Services/Authenticator.cs
* Add `AuthenticationsIdentity` connection string in `web.config`.
  * Example: https://github.com/bcjobs/demo/blob/master/Demo.Web/Web.config

## Project Properties
These are recommendations only and not required to use this library.<br />
Every time you add a new project, these changes to the project properties are recommended:<br />

* `Build` tab:
  * `Configurations`: `All Configurations`
  * `Supress warnings`: `1998`
If Code Contracts is installed:
* `Code Contracts` tab:
  * `Configurations`: `All Configurations`
  * `Perform Runtime Contract Checking`: select checkbox and select `Full` from the dropdown
  * `Contract Reference Assembly`: `Build`
  * `Emit contracts into XML doc file`: select checkbox

## IoC Autodiscovery
To ensure that all implementations of interfaces in a project are auto-discovered, do the following:

* Open `AssemblyInfo.cs` file (nested inside `Properties` in Solution Explorer)
* Add this to the bottom:
  * `[assembly: IoC]`
  * Don't forget the _using_ statement at the top:<br />`using Infra.IoC;`