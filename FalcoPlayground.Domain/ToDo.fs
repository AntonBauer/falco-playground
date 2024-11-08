namespace FalcoPlayground.Domain

module ToDo =
    open System

    module ToDoId =
        type ToDoId = ToDoId of Guid
        let createNew = ToDoId(Guid.NewGuid())
        let value (ToDoId id) = id

    open ToDoId

    type ToDoItem =
        { Id: ToDoId
          Header: string
          Description: string option }

    let create name description =
        { Id = createNew
          Header = name
          Description =
            match String.IsNullOrEmpty description with
            | true -> None
            | false -> Some description }
