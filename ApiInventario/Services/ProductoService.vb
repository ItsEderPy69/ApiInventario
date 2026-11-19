Imports System.Net
Imports ApiInventario.DTOs
Imports ApiInventario.Exceptions
Imports ApiInventario.Models
Imports ApiInventario.Repositories

Namespace Services

    Public Class ProductoService
        Implements IProductoService

        Private ReadOnly _productos As IProductoRepository
        Private ReadOnly _categorias As ICategoriaRepository

        Public Sub New(productos As IProductoRepository, categorias As ICategoriaRepository)
            _productos = productos
            _categorias = categorias
        End Sub

        Public Async Function GetAllAsync() As Task(Of List(Of ProductoResponseDto)) Implements IProductoService.GetAllAsync
            Dim lista = Await _productos.GetAllConCategoriaAsync()
            Return lista.Select(Function(p) Mapper.ToDto(p)).ToList()
        End Function

        Public Async Function GetByIdAsync(id As Integer) As Task(Of ProductoResponseDto) Implements IProductoService.GetByIdAsync
            Dim producto = Await _productos.GetByIdConCategoriaAsync(id)
            If producto Is Nothing Then Throw NoEncontrado()
            Return Mapper.ToDto(producto)
        End Function

        Public Async Function GetByCategoriaAsync(categoriaId As Integer) As Task(Of List(Of ProductoResponseDto)) Implements IProductoService.GetByCategoriaAsync
            If Not Await _categorias.ExistsAsync(categoriaId) Then
                Throw New ApiException(HttpStatusCode.NotFound, "Categoria no encontrada")
            End If
            Dim lista = Await _productos.GetByCategoriaAsync(categoriaId)
            Return lista.Select(Function(p) Mapper.ToDto(p)).ToList()
        End Function

        Public Async Function CreateAsync(dto As ProductoCreateDto) As Task(Of ProductoResponseDto) Implements IProductoService.CreateAsync
            Await ValidarReglasAsync(dto.Nombre, dto.Stock, dto.CategoriaId, Nothing)

            Dim producto As New Producto With {
                .Nombre = dto.Nombre.Trim(),
                .Descripcion = dto.Descripcion,
                .Precio = dto.Precio,
                .Stock = dto.Stock,
                .CategoriaId = dto.CategoriaId,
                .FechaRegistro = DateTime.UtcNow}

            Await _productos.AddAsync(producto)
            Return Await GetByIdAsync(producto.Id)
        End Function

        Public Async Function UpdateAsync(id As Integer, dto As ProductoUpdateDto) As Task(Of ProductoResponseDto) Implements IProductoService.UpdateAsync
            Dim producto = Await _productos.GetByIdAsync(id)
            If producto Is Nothing Then Throw NoEncontrado()

            Await ValidarReglasAsync(dto.Nombre, dto.Stock, dto.CategoriaId, id)

            producto.Nombre = dto.Nombre.Trim()
            producto.Descripcion = dto.Descripcion
            producto.Precio = dto.Precio
            producto.Stock = dto.Stock
            producto.CategoriaId = dto.CategoriaId

            Await _productos.UpdateAsync(producto)
            Return Await GetByIdAsync(id)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task Implements IProductoService.DeleteAsync
            Dim producto = Await _productos.GetByIdAsync(id)
            If producto Is Nothing Then Throw NoEncontrado()
            Await _productos.DeleteAsync(producto)
        End Function

        Private Async Function ValidarReglasAsync(nombre As String, stock As Integer, categoriaId As Integer, excludeId As Integer?) As Task
            If stock < 0 Then
                Throw New ApiException(HttpStatusCode.BadRequest, "El stock no puede ser negativo")
            End If

            If Not Await _categorias.ExistsAsync(categoriaId) Then
                Throw New ApiException(HttpStatusCode.NotFound, "La categoria indicada no existe")
            End If

            If Await _productos.NombreExistsAsync(nombre, excludeId) Then
                Throw New ApiException(HttpStatusCode.Conflict, "Ya existe un producto con ese nombre")
            End If
        End Function

        Private Shared Function NoEncontrado() As ApiException
            Return New ApiException(HttpStatusCode.NotFound, "Producto no encontrado")
        End Function

    End Class

End Namespace
