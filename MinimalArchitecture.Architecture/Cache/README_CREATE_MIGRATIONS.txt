COMMAND TO CREATE NEW MIGRATIONS

dotnet ef migrations add firstMigration --context MinimalArchitecture.Architecture.Cache.DbContextCache --project ./MinimalArchitecture.Architecture -o Cache/Migrations --startup-project MinimalArchitecture.Architecture 