Imports ApiInventario.Models

Namespace Repositories

    Public Interface IProductoRepository
        Function GetAllConCategoriaAsync() As Task(Of List(Of Producto))
        Function GetByIdConCategoriaAsync(id As Integer) As Task(Of Producto)
        Function GetByIdAsync(id As Integer) As Task(Of Producto)
        Function GetByCategoriaAsync(categoriaId As Integer) As Task(Of List(Of Producto))
        Function NombreExistsAsync(nombre As String, Optional excludeId As Integer? = Nothing) As Task(Of Boolean)
        Function AddAsync(producto As Producto) As Task(Of Producto)
        Function UpdateAsync(producto As Producto) As Task
        Function DeleteAsync(producto As Producto) As Task
    End Interface

End Namespace
