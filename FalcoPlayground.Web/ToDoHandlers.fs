namespace FalcoPlayground.Web

module ToDoHandlers =
    open Falco
    open Microsoft.Extensions.Configuration
    open FalcoPlayground.DataAccess
    open System.Text.Json
    open FalcoPlayground.Domain
    open System
    open FalcoPlayground.Domain.ToDo.ToDoId

    type CreateTodoRequest = { Header: string; Description: string }

    type ToDoDto =
        { Id: Guid
          Header: string
          Description: string }

    let getAll (config: IConfiguration) =
        fun ctx ->
            let items =
                config.GetSection("ConnectionStrings:Playground").Value
                |> ToDoRepository.ReadAll.execute

            let response =
                items
                |> Array.map (fun item -> $"{value item.Id} - {item.Header}")
                |> String.concat "\n"

            Response.ofPlainText response ctx

    let private extractRequest ctx =
        let raw = (Request.getBodyString ctx).GetAwaiter().GetResult()
        JsonSerializer.Deserialize<CreateTodoRequest> raw

    let create (config: IConfiguration) =
        fun ctx ->
            let request = extractRequest ctx
            let item = ToDo.create request.Header request.Description

            config.GetSection("ConnectionStrings:Playground").Value
            |> ToDoRepository.Save.execute item

            let id = (value item.Id).ToString()
            Response.ofPlainText id ctx
