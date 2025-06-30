dotnet ef migrations add CreateInitialScheme -s Api -p Infrastructure -o Persistences/Migrations
dotnet ef database update -s Api -p Infrastructure

dotnet ef migrations remove -s Api -p Infrastructure
dotnet ef database update 0 -s Api -p Infrastructure