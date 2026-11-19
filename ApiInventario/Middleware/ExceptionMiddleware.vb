Imports System.Net
Imports ApiInventario.Exceptions
Imports Microsoft.AspNetCore.Http
Imports Microsoft.Extensions.Logging

Namespace Middleware

    Public Class ExceptionMiddleware

        Private ReadOnly _next As RequestDelegate
        Private ReadOnly _logger As ILogger(Of ExceptionMiddleware)

        Public Sub New([next] As RequestDelegate, logger As ILogger(Of ExceptionMiddleware))
            _next = [next]
            _logger = logger
        End Sub

        Public Async Function InvokeAsync(context As HttpContext) As Task
            Dim statusCode As HttpStatusCode
            Dim message As String

            Try
                Await _next(context)
                Return
            Catch ex As ApiException
                statusCode = ex.StatusCode
                message = ex.Message
            Catch ex As Exception
                _logger.LogError(ex, "Error no controlado en {Method} {Path}", context.Request.Method, context.Request.Path)
                statusCode = HttpStatusCode.InternalServerError
                message = "Error interno del servidor"
            End Try

            context.Response.StatusCode = CInt(statusCode)
            Await context.Response.WriteAsJsonAsync(New With {.statusCode = CInt(statusCode), .message = message})
        End Function

    End Class

End Namespace
