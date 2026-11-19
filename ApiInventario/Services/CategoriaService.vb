Imports System.Net
Imports ApiInventario.DTOs
Imports ApiInventario.Exceptions
Imports ApiInventario.Models
Imports ApiInventario.Repositories

Namespace Services

    Public Class CategoriaService
        Implements ICategoriaService

        Private ReadOnly _repository As ICategoriaRepository

        Public Sub New(repository As ICategoriaRepository)
            _repository = repository
        End Sub

        Public Async Function GetAllAsync() As Task(Of List(Of CategoriaResponseDto)) Implements ICategoriaService.GetAllAsync
            Dim categorias = Await _repository.GetAllAsync()
            Return categorias.Select(Function(c) Mapper.ToDto(c)).ToList()
        End Function

        Public Async Function GetByIdAsync(id As Integer) As Task(Of CategoriaResponseDto) Implements ICategoriaService.GetByIdAsync
            Dim categoria = Await _repository.GetByIdAsync(id)
            If categoria Is Nothing Then Throw NoEncontrada()
            Return Mapper.ToDto(categoria)
        End Function

        Public Async Function GetConProductosAsync(id As Integer) As Task(Of CategoriaConProductosDto) Implements ICategoriaService.GetConProductosAsync
            Dim categoria = Await _repository.GetByIdConProductosAsync(id)
            If categoria Is Nothing Then Throw NoEncontrada()
            Return Mapper.ToConProductosDto(categoria)
        End Function

        Public Async Function CreateAsync(dto As CategoriaCreateDto) As Task(Of CategoriaResponseDto) Implements ICategoriaService.CreateAsync
            If Await _repository.NombreExistsAsync(dto.Nombre) Then
                Throw New ApiException(HttpStatusCode.Conflict, "Ya existe una categoria con ese nombre")
            End If

            Dim categoria As New Categoria With {.Nombre = dto.Nombre.Trim(), .Descripcion = dto.Descripcion}
            Await _repository.AddAsync(categoria)
            Return Mapper.ToDto(categoria)
        End Function

        Public Async Function UpdateAsync(id As Integer, dto As CategoriaUpdateDto) As Task(Of CategoriaResponseDto) Implements ICategoriaService.UpdateAsync
            Dim categoria = Await _repository.GetByIdAsync(id)
            If categoria Is Nothing Then Throw NoEncontrada()

            If Await _repository.NombreExistsAsync(dto.Nombre, id) Then
                Throw New ApiException(HttpStatusCode.Conflict, "Ya existe una categoria con ese nombre")
            End If

            categoria.Nombre = dto.Nombre.Trim()
            categoria.Descripcion = dto.Descripcion
            Await _repository.UpdateAsync(categoria)
            Return Mapper.ToDto(categoria)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task Implements ICategoriaService.DeleteAsync
            Dim categoria = Await _repository.GetByIdAsync(id)
            If categoria Is Nothing Then Throw NoEncontrada()

            If Await _repository.TieneProductosAsync(id) Then
                Throw New ApiException(HttpStatusCode.Conflict, "No se puede eliminar la categoria porque tiene productos asociados")
            End If

            Await _repository.DeleteAsync(categoria)
        End Function

        Private Shared Function NoEncontrada() As ApiException
            Return New ApiException(HttpStatusCode.NotFound, "Categoria no encontrada")
        End Function

    End Class

End Namespace
