 Requirements
 * .NET Core 3.0
 * MySql
 * Vs code or Visual Studio Community

# Architecture

Layer architecture:
  * API (Start up project)
    * Point of access for our application
    * dto or ressources api
    * Validation with Fluent
    * AutoWrapper
    * AutoMapper
  * Core
    * Application foundation
    * Contracts => interface
    * Models
    * everything else that is essential
  * Data
    * Access layer
    * Repository Pattern
  * Services
    * Business logic (link between Api and Data)
  * Web (Angular Admin pannel)
    * Admin pannel for adminstration of our website

# Lib
 * Entity Framework core for persistence
 * AutoMapper for mapping models into Dto Api
 * Swagger to have a friendly API interface
 * FluentValidation for validate dto from api
 * AutoWrapper for Wrapp all response into uniform API rest response

# Install build project

`dotnet restore`

`dotnet build`

# EF Core Update Database - Add Migration
`dotnet ef --startup-project ElegantGlamour.Api/ElegantGlamour.Api.csproj migrations add InitialModel -p ElegantGlamour.Data\ElegantGlamour.Data.csproj`

`dotnet ef --startup-project ElegantGlamour.Api/ElegantGlamour.Api.csproj database update`


# Launch project
`dotnet run -p ElegantGlamour.Api\ElegantGlamour.Api.csproj`