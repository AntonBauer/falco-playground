namespace FalcoPlayground.DataAccess

module ToDoRepository =
    open Npgsql.FSharp
    open FalcoPlayground.Domain.ToDo

    module Save =

        let private saveQuery =
            "INSERT INTO playground.ToDo (id, header, description) VALUES (@id, @header, @description)"

        let private descriptionExtractor (toDoItem: ToDoItem) =
            match toDoItem.Description with
            | Some description -> Sql.string description
            | None -> Sql.dbnull

        type private SaveToDoItem = ToDoItem -> string -> unit

        let execute: SaveToDoItem =
            fun todoItem connectionString ->
                connectionString
                |> Sql.connect
                |> Sql.query saveQuery
                |> Sql.parameters
                    [ "id", (Sql.uuid (ToDoId.value todoItem.Id));
                      "header", Sql.string todoItem.Header;
                      "description", descriptionExtractor todoItem ]
                |> Sql.executeNonQuery
                |> ignore

    module ReadAll =
        open FalcoPlayground.Domain.ToDo.ToDoId

        let private getAllQuery = "SELECT * FROM playground.ToDo"
        type private ToDoReader = RowReader -> ToDoItem

        let private readToDoItem: ToDoReader =
            fun read ->
                { Id = ToDoId(read.uuid "id")
                  Header = read.string "header"
                  Description = read.stringOrNone "description" }

        let execute connectionString =
            connectionString
            |> Sql.connect
            |> Sql.query getAllQuery
            |> Sql.execute readToDoItem
            |> Array.ofList
