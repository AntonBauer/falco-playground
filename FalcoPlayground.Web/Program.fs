module FalcoPlayground.Web.Program

open Falco.HostBuilder
open FalcoPlayground.DataAccess

let config = configuration[||] {
    required_json "appsettings.json"
    optional_json "appsettings.Development.json"
}

config.GetSection("ConnectionStrings:Playground").Value
|> Migrator.migrate 

webHost[||]{
    endpoints (Endppoints.ToDo.crud config)
}
