### Migrations

Run ef migration command in root directory
```shell
dotnet ef migrations add {migration_name} --startup-project src/Api/Api.csproj --project src/Infrastructure/Infrastructure.csproj -o Persistence/Migrations
```

Apply migrations to database
```shell
dotnet ef database update --startup-project src/Api/Api.csproj --project src/Infrastructure/Infrastructure.csproj
```