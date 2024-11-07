module FalcoPlayground.Web.Program

open Falco
open Falco.Routing
open Falco.HostBuilder

[<EntryPoint>]
let main args =
    webHost args { endpoints Endppoints.ToDo.crud }
    0
