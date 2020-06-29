# First things to do

Run 'setup.sh' or:

Perform a case sensitive find for each of the keywprds below  and perform a replace-all:
  * Replace '**projectName**' -> the name of the project
  * Replace '**projectRootNamespace**' -> the name of the project root namespace
  * Replace '**projectAssemblyName**' -> the name of the project assembly
  * Replace '**dbName**' -> the name of the database

Examples:

  * Replace '**projectName**' with '**Activities**'
  * Replace '**projectRootNamespace**' with '**Focus.Activities**'
  * Replace '**projectAssemblyName**' -> with '**focus.activities**'
  * Replace '**dbName**' with '**activities**'

You can remove this section of the readme once the changes are applied.

Next you should look for 'TODO:' markers to see what else needs to be done and where.

# projectName Api

[INSERT DESCRIPTION]

## Index
 * [Setup](#setup)
  * [How to run the application](#how-to-run-the-application)
  * [How to test the application](#how-to-test-the-application)
  * [How to configure the application](#how-to-configure-the-application)
  * [How to configure the swagger UI](#how-to-configure-swagger-ui)
  * [How to configure Authentication/Authorization](#how-to-configure-authentication-authorization)
    + [Enable or switch to the LIS auth provider](#enable-or-switch-to-the-lis-auth-provider)
    + [Enable or switch to the Custom auth provider](#enable-or-switch-to-the-custom-auth-provider)
    + [Disable auth](#disable-auth)
  * [How to work with database (using database code-first)](#how-to-work-with-database--using-database-code-first-)
  * [How to create a migration](#how-to-create-a-migration)
  * [How to apply database changes automatically](#how-to-apply-database-changes-automatically)
  * [How to apply database changes manually](#how-to-apply-database-changes-manually)
  * [How to recreate the database](#how-to-recreate-the-database)
  * [Useful commands](#useful-commands)
## Setup

In order to be able to compile, run and test this component the following requirements need to be installed:

* Docker

  ~~~~
  Go to "https://docs.docker.com/install" and follow instructions specific to your environment.
  ~~~~

* Docker Compose

  ~~~~
  Go to "https://docs.docker.com/compose/install" and follow instructions specific to your environment.
  ~~~~

* DotNet Core SDK

  ~~~~
  Go to "https://dotnet.microsoft.com/download" and follow instructions specific to your environment.
  ~~~~

* DotNet EntityFramework Cli
  ~~~~
  $ dotnet tool install --global dotnet-ef
  ~~~~

## How to run the application

The api can be executed by running the following command in the root directory of the api component:

~~~sh
# Start a local postgres server
$ docker-compose up;

# Runs the application with the specified profile
$ dotnet run -p ./src/api.csproj --launch-profile <ENV>;
~~~

The possible values for <ENV> are: **Development**, **Staging** and **Production**.
The default value is **Development**.

## How to test the application

The tests can be executed by running the following command in the root directory of the api component:

~~~sh
# Start a local postgres server
$ docker-compose up;

# Run the tests
$ export ASPNETCORE_ENVIRONMENT=<ENV>; dotnet test ./tests/api.tests.csproj;
~~~

The possible values for <ENV> are: **Development**, **Staging** and **Production**.
The default value is **Development**.

## How to configure the application

By default, dotnet applications store their settings in appsettings.json files.
The settings can be merged/overriden by appsettings files specific to an environment or by system environment variables. The order by wich the settings are loaded is:

- Read from *appsettings.json*
- Read from *appsettings.{environment}.json* and merge/override
- Read from environment variables and merge/override (Hierarchical settings can be set with __ (ex: Settings__JwtExpiration))

## How to configure swagger UI

All files necessary to customize the swagger UI can be found inside <code>/wwwroot/docs</code>.
If you need more control you can create an index.html file inside this folder that will replace the default one.

## How to configure Authentication/Authorization

The templace comes with 2 auth providers, a LIS provider that is ready to use and only needs to be configured, and a custom provider that is intended to be a starting point for a custom implementation.
By default, the template comes with authentication using the custom provider enabled.

### Enable or switch to the LIS auth provider

Follow the steps below to enable LIS auth provider:

* In <code>/Startup.cs</code>
  * Add or uncomment a call to <code>services.AddLisAuthentication()</code> in the 'ConfigureServices' method. The call must be placed before the call to 'services.AddControllers()'.
  * Add or uncomment a call to <code>app.UseLisAuthentication()</code> in the 'Configure' method. The call must be placed between the call to 'app.UseRouting()' and 'app.UseEndpoints()'.
* Remove the <code>abstract</code> keyword from the class definition at <code>/Infrastructure/Controllers/AuthenticationController.cs</code>.
* Add the <code>[Authorize]</code> attribute to the class definition at <code>/Infrastructure/BaseController.cs</code>
* Update the necessary settings in appsettings.json files under **Settings->Authentication->Lis**

To be able to develop using the LIS provider you need to update your hosts file with a subdomain of the LIS instance url you intend to use. For example, if you intend to use the LIS at **dev.city-platform.com** you need to add a subdomain of that to your hosts file, something like **local.dev.city-platform.com**.
Now all you need to do is open a browser using **local.dev.city-platform.com** instead of **localhost** and the auth should work.

### Enable or switch to the Custom auth provider

Follow the steps below to enable Custom auth provider:

* In <code>/Startup.cs</code>
  * Add or uncomment a call to <code>services.AddCustomAuthentication()</code> in the 'ConfigureServices' method. The call must be placed before the call to 'services.AddControllers()'.
  * Add or uncomment a call to <code>app.UseCustomAuthentication()</code> in the 'Configure' method. The call must be placed between the call to 'app.UseRouting()' and 'app.UseEndpoints()'.
* Remove the <code>abstract</code> keyword from the class definition at <code>/Infrastructure/Controllers/AuthenticationController.cs</code>.
* Add the <code>[Authorize]</code> attribute to the class definition at <code>/Infrastructure/BaseController.cs</code>
* Update the necessary settings in appsettings.json files under **Settings->Authentication->Custom**

You can test the sign-in using the following credentials 'user/pass' or 'admin/pass'.

### Disable auth

Follow the steps below to disable auth entirely:

* In <code>/Startup.cs</code> comment or remove calls to **AddCustomAuthentication**, **AddLisAuthentication**, **UseCustomAuthentication** and **UseLisAuthentication**.
* Add the <code>abstract</code> keyword to the class definition at <code>/Infrastructure/Controllers/AuthenticationController.cs</code>
* Remove the <code>[Authorize]</code> attribute from <code>/Infrastructure/BaseController.cs</code>

## How to work with database (using database code-first)

Database access is done using an "Object-relational mapping" framework, in this case Microsoft Entity Framework. We are using a code first strategy meaning that the database structure is defined in code and database schema modifications are done using migration scripts generated by Entity Framework.

## How to create a migration

The command below is used to create a migration.

~~~sh
$ dotnet ef migrations add <NAME>;
~~~

This generates migration files in the /Migrations folder but does not apply them to the database.

## How to apply database changes automatically

The "Startup" class was configured to ensure that the migrations are applied everytime the application starts.
If the database is up-to-date with the latest migration nothing is done, otherwise the migrations that are missing get applied to the database, including the database creation in case of an initial migration.

## How to apply database changes manually

In some cenarios is useful to apply the migrations manually. To apply migrations against a database run one of the following commands:

* Apply a migration against a development database:

  ~~~sh
  # Start a local postgres server
  $ docker-compose up;

  # Update the database by applying missing migrations
  $ export ASPNETCORE_ENVIRONMENT=Development; dotnet ef database update -p ./src/api.csproj;
  ~~~

* Apply a migration against a staging (qa) database:

  ~~~sh
  # Update the database by applying missing migrations
  $ export ASPNETCORE_ENVIRONMENT=Staging; dotnet ef database update
  ~~~

## How to recreate the database

During the development cycle is sometimes useful to recreate the local or the qa database.
To do this we need to drop the existing database and recreate by applying all existing migrations using the following commands:

* Recreating the local development database:

  ~~~sh
  # Drop the existing database
  $ export ASPNETCORE_ENVIRONMENT=Development; dotnet ef database drop -p ./src/api.csproj;

  # Recreate the database by applying all available migrations
  $ export ASPNETCORE_ENVIRONMENT=Development; dotnet ef database update -p ./src/api.csproj;
  ~~~

* Recreating the staging database:

  ~~~sh
  # Start a cloud-sql-proxy used to connect to the qa database
  $ docker-compose -f ./docker-compose-sql-proxy-qa.yml up;

  # Drop the existing database
  $ export ASPNETCORE_ENVIRONMENT=Staging; dotnet ef database drop -p ./src/api.csproj;

  # Recreate the database by applying all available migrations
  $ export ASPNETCORE_ENVIRONMENT=Staging; dotnet ef database update -p ./src/api.csproj;
  ~~~

## Useful commands

~~~sh
# Recreate database from scratch with newly generated migrations
$ dotnet ef database drop -f; rm -rf Migrations -f; dotnet ef migrations add Initial; dotnet ef database update;
~~~

