namespace FalcoPlayground.Domain

module Say =
    open System

    type ToDoId = ToDoId of Guid

    type ToDoItem =
        { Id: ToDoId
          Header: string
          Description: string option }

    let create name description =
        { Id = ToDoId(Guid.NewGuid())
          Header = name
          Description =
            match String.IsNullOrEmpty description with
            | true -> None
            | false -> Some description }
