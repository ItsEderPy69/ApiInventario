Imports ApiInventario.DTOs
Imports ApiInventario.Services
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers

    <ApiController>
    <Route("api/[controller]")>
    <Produces("application/json")>
    Public Class CategoriasController
        Inherits ControllerBase

        Private ReadOnly _service As ICategoriaService

        Public Sub New(service As ICategoriaService)
            _service = service
        End Sub

        <HttpGet>
        <EndpointSummary("retorna todas las categorias")>
        <ProducesResponseType(GetType(List(Of CategoriaResponseDto)), StatusCodes.Status200OK)>
        Public Async Function GetAll() As Task(Of IActionResult)
            Return Ok(Await _service.GetAllAsync())
        End Function

        <HttpGet("{id:int}")>
        <EndpointSummary("retorna la categoria indicada")>
        <ProducesResponseType(GetType(CategoriaResponseDto), StatusCodes.Status200OK)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        Public Async Function GetById(id As Integer) As Task(Of IActionResult)
            Return Ok(Await _service.GetByIdAsync(id))
        End Function

        <HttpGet("{id:int}/productos")>
        <EndpointSummary("retorna la categoria con sus productos")>
        <ProducesResponseType(GetType(CategoriaConProductosDto), StatusCodes.Status200OK)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        Public Async Function GetConProductos(id As Integer) As Task(Of IActionResult)
            Return Ok(Await _service.GetConProductosAsync(id))
        End Function

        <HttpPost>
        <EndpointSummary("retorna la categoria creada")>
        <ProducesResponseType(GetType(CategoriaResponseDto), StatusCodes.Status201Created)>
        <ProducesResponseType(StatusCodes.Status400BadRequest)>
        <ProducesResponseType(StatusCodes.Status409Conflict)>
        Public Async Function Create(<FromBody> dto As CategoriaCreateDto) As Task(Of IActionResult)
            Dim creada = Await _service.CreateAsync(dto)
            Return CreatedAtAction(NameOf(GetById), New With {.id = creada.Id}, creada)
        End Function

        <HttpPut("{id:int}")>
        <EndpointSummary("retorna la categoria modificada")>
        <ProducesResponseType(GetType(CategoriaResponseDto), StatusCodes.Status200OK)>
        <ProducesResponseType(StatusCodes.Status400BadRequest)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        <ProducesResponseType(StatusCodes.Status409Conflict)>
        Public Async Function Update(id As Integer, <FromBody> dto As CategoriaUpdateDto) As Task(Of IActionResult)
            Return Ok(Await _service.UpdateAsync(id, dto))
        End Function

        <HttpDelete("{id:int}")>
        <EndpointSummary("deeletea la categoria")>
        <ProducesResponseType(StatusCodes.Status204NoContent)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        <ProducesResponseType(StatusCodes.Status409Conflict)>
        Public Async Function Delete(id As Integer) As Task(Of IActionResult)
            Await _service.DeleteAsync(id)
            Return NoContent()
        End Function

    End Class

End Namespace
