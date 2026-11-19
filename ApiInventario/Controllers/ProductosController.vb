Imports ApiInventario.DTOs
Imports ApiInventario.Services
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers

    <ApiController>
    <Route("api/[controller]")>
    <Produces("application/json")>
    Public Class ProductosController
        Inherits ControllerBase

        Private ReadOnly _service As IProductoService

        Public Sub New(service As IProductoService)
            _service = service
        End Sub

        <HttpGet>
        <EndpointSummary("retorna todos los productos con su categoria")>
        <ProducesResponseType(GetType(List(Of ProductoResponseDto)), StatusCodes.Status200OK)>
        Public Async Function GetAll() As Task(Of IActionResult)
            Return Ok(Await _service.GetAllAsync())
        End Function

        <HttpGet("{id:int}")>
        <EndpointSummary("retorna el producto indicado con su categoria")>
        <ProducesResponseType(GetType(ProductoResponseDto), StatusCodes.Status200OK)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        Public Async Function GetById(id As Integer) As Task(Of IActionResult)
            Return Ok(Await _service.GetByIdAsync(id))
        End Function

        <HttpGet("categoria/{categoriaId:int}")>
        <EndpointSummary("retorna los productos de la categoria indicada")>
        <ProducesResponseType(GetType(List(Of ProductoResponseDto)), StatusCodes.Status200OK)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        Public Async Function GetByCategoria(categoriaId As Integer) As Task(Of IActionResult)
            Return Ok(Await _service.GetByCategoriaAsync(categoriaId))
        End Function

        <HttpPost>
        <EndpointSummary("retorna el producto creado")>
        <ProducesResponseType(GetType(ProductoResponseDto), StatusCodes.Status201Created)>
        <ProducesResponseType(StatusCodes.Status400BadRequest)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        <ProducesResponseType(StatusCodes.Status409Conflict)>
        Public Async Function Create(<FromBody> dto As ProductoCreateDto) As Task(Of IActionResult)
            Dim creado = Await _service.CreateAsync(dto)
            Return CreatedAtAction(NameOf(GetById), New With {.id = creado.Id}, creado)
        End Function

        <HttpPut("{id:int}")>
        <EndpointSummary("retorna el producto modificado")>
        <ProducesResponseType(GetType(ProductoResponseDto), StatusCodes.Status200OK)>
        <ProducesResponseType(StatusCodes.Status400BadRequest)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        <ProducesResponseType(StatusCodes.Status409Conflict)>
        Public Async Function Update(id As Integer, <FromBody> dto As ProductoUpdateDto) As Task(Of IActionResult)
            Return Ok(Await _service.UpdateAsync(id, dto))
        End Function

        <HttpDelete("{id:int}")>
        <EndpointSummary("elimina el producto")>
        <ProducesResponseType(StatusCodes.Status204NoContent)>
        <ProducesResponseType(StatusCodes.Status404NotFound)>
        Public Async Function Delete(id As Integer) As Task(Of IActionResult)
            Await _service.DeleteAsync(id)
            Return NoContent()
        End Function

    End Class

End Namespace
