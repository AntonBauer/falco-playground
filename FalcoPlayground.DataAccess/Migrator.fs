namespace FalcoPlayground.DataAccess

module Migrator =
    open DbUp
    open System.Reflection

    let migrate connectionString =
        EnsureDatabase.For.PostgresqlDatabase(connectionString)

        let migrator =
            DeployChanges.To
                .PostgresqlDatabase(connectionString = connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build()

        migrator.PerformUpgrade() |> ignore
