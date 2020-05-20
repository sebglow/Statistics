# README #


### What is this repository for? ###

* The api for GitHub repositories statistics. It calls https://api.github.com/users/{user}/repos endpoint and produces statistics for a result.
* As GitHub has limitations on maximum request per hour, the cache has been implemented. The statistics are stored in a database for one hour. 
  So for one hour it returns stored statistics for a given user. After one hour, the fresh copy is retrieved from GitHub api.
* The requests to GitHub api can be authorized if GitHub user and token are set in configuration file.  See **Configuration details -> GitHub** section

### How do I get set up? ###

#### Option 1 ####
In Visual Studio hit F5 or use dotnet run command.
By default InMemory Database will be used here.

#### Option 2 ####
Use docker-compose by running: 
~~~~
"docker-compose up --build" 
~~~~
command in solution's root directory. By default Sql Database will be used here.
The included docker-compose.yml file is responsible for running both app and db containers.

By default the application runs on **[http/https]://localhost/api/repositories/{owner}** (depending on local environment configuration)


#### Swagger ####
Is available at **[http/https]://localhost/swagger/**

### Configuration details ###

#### Database ####
By default the InMemory database is used.
If environmental variable "ADD_DB_CONTEXT" is set to "SqlServer", then the MsSql server database is configured.
The connection string for it is in appsettings.json file -> ConnectionStrings: SebGlowDb

#### GitHub ####
GitHub related settings can be found in appsettings.json file in GitHub section.

* BaseAddress: the GitHub api address
* User: the GitHub user (optinoal)
* Token: the GitHub token (optional)

#### Docker-compose ####

The following environmental variables can be used for `api` service:

* - `ADD_DB_CONTEXT=SqlServer` (if Sql instead of InMemory database must be used)
* - `ASPNETCORE_URLS=http://+:80` (if http only)
* - `ASPNETCORE_URLS=https://+:443;http://+:80` (if both http and https should be available)
* - `ASPNETCORE_Kestrel__Certificates__Default__Password=Password1!` (if both http and https should be available)
* - `ASPNETCORE_Kestrel__Certificates__Default__Path=/https/iisexpress.pfx` (if both http and https should be available)

The pfx file can be copied into a container or mount with parent directory as a volume:

    volumes:
      - ./SebGlow.Api/https:/https:ro