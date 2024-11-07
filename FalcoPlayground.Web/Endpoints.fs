namespace FalcoPlayground.Web

module Endppoints =

    open Falco
    open Falco.Routing

    module ToDo = 
        let crud = [all "/todos" [ GET, ToDoHandlers.getAll; POST, ToDoHandlers.create ]]