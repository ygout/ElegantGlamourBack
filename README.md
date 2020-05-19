# Requirment

# Install build project
'dotnet restore'
'dotnet build'

# Update Database (Entity framework)
'dotnet ef --startup-project ElegantGlamour.Api/ElegantGlamour.Api.csproj migrations add InitialModel -p ElegantGlamour.Data\ElegantGlamour.Data.csproj'
'dotnet ef --startup-project ElegantGlamour.Api/ElegantGlamour.Api.csproj database update'

# Launch project
dotnet run -p ElegantGlamour.Api\ElegantGlamour.Api.csproj