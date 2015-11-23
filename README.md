## Infrastructure
This repository contains a collection of _infrastructure_ projects that should be added as a _submodule_ inside of another repository.
## Setup
* Go do a Git repository where you'd like to add these _infrastructure_ projects.
* Add this repository as a submodule:
  * `git submodule add https://github.com/bcjobs/infrastructure.git Infrastructure`
* Open Solution in Visual Studio.
* Right-click solution name and select:
  * `Add` --> `New Solution Folder`
* Name the solution folder `Infrastructure`
* One by one, add all of the projects in the submodule into the solution.
* If you encounter any problems with Entity Framework dependencies while trying to build one of these added projects, uninstall and re-install Entity Framework Nuget packages.
* Add references too all projects in `Infrastructure` folder to any project that will need them (e.g. web project).
 
## Autofac Config
* Register all IHandlers in Infrastructure and web project with Autofac
* Example: https://github.com/bcjobs/prototype/blob/master/UI.Web/App_Start/AutofacConfig.cs
 
## Identity
* Create OwinConfig:
  * In `App_Start` folder of web project, add an OwinConfig class:
  * https://github.com/bcjobs/prototype/blob/master/UI.Web/App_Start/OwinConfig.cs
* Register OwinConfig:
  * Add the following to `appSettings` in `web.config`: `<add key="owin:AppStartup" value="{namespace}.OwinConfig" />`
  * Where `{namespace}` is the namespace to `OwinConfig`
* Add the following NuGet packages to the web project:
  * Microsoft.AspNet.Identity.EntityFramework
  * Microsoft.AspNet.Identity.OWIN
* Add a Handler for UserIdLookup
  * Example: https://github.com/bcjobs/prototype/blob/master/UI.Web/Services/Implementation/UserDirectory.cs
  * Ensure that the class namespace ends with `Services.Implementation` so that Autofac can find it (as defined above in `Autofac Config`).
* Add `Identity` connection string in `web.config`.
  * Example: https://github.com/bcjobs/prototype/blob/master/UI.Web/Web.config
* Run EF migrations from:
  * https://github.com/bcjobs/infrastructure/blob/master/Infrastructure.Security/Migrations/201508290037057_InitialCreate.cs 
  * To do this:
    * open Package Manager Console (Tools --> Nuget Package Manager --> Package Manager Console)
    * Make sure `Default project` is `Infrastructure\Infrastructure.Security`
    * Run the command `Update-Database`
