# Requirements
.NET Core 3.0
MySql
Vs code or Visual Studio Community

# Architecture

# Lib
Entity Framework core for persistence
AutoMapper for mapping models into Dto Api
Swagger to have a friendly API interface
FluentValidation for validate dto from api
AutoWrapper for Wrapp all response into uniform API rest response

# Install build project
`dotnet restore`
`dotnet build`

# Update Database (Entity framework)
`dotnet ef --startup-project ElegantGlamour.Api/ElegantGlamour.Api.csproj migrations add InitialModel -p ElegantGlamour.Data\ElegantGlamour.Data.csproj`
`dotnet ef --startup-project ElegantGlamour.Api/ElegantGlamour.Api.csproj database update`

# Launch project
`dotnet run -p ElegantGlamour.Api\ElegantGlamour.Api.csproj`