namespace FalcoPlayground.Web
module ToDoHandlers =
    open Falco
    let getAll: HttpHandler =
        Response.ofPlainText "Listed"

    let create: HttpHandler =
        Response.ofPlainText "Created"