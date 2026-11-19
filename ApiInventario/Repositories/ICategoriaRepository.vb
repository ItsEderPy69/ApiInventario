Imports ApiInventario.Models

Namespace Repositories

    Public Interface ICategoriaRepository
        Function GetAllAsync() As Task(Of List(Of Categoria))
        Function GetByIdAsync(id As Integer) As Task(Of Categoria)
        Function GetByIdConProductosAsync(id As Integer) As Task(Of Categoria)
        Function ExistsAsync(id As Integer) As Task(Of Boolean)
        Function NombreExistsAsync(nombre As String, Optional excludeId As Integer? = Nothing) As Task(Of Boolean)
        Function TieneProductosAsync(id As Integer) As Task(Of Boolean)
        Function AddAsync(categoria As Categoria) As Task(Of Categoria)
        Function UpdateAsync(categoria As Categoria) As Task
        Function DeleteAsync(categoria As Categoria) As Task
    End Interface

End Namespace
