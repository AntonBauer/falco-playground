namespace FalcoPlayground.Web

module Endppoints =

    open Falco
    open Falco.Routing

    module ToDo =
        let crud config =
            [ all "/todos" [ GET, ToDoHandlers.getAll config; POST, ToDoHandlers.create config ] ]
