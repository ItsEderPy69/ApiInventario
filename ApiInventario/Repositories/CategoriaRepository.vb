Imports ApiInventario.Data
Imports ApiInventario.Models
Imports Microsoft.EntityFrameworkCore

Namespace Repositories

    Public Class CategoriaRepository
        Implements ICategoriaRepository

        Private ReadOnly _context As InventarioDbContext

        Public Sub New(context As InventarioDbContext)
            _context = context
        End Sub

        Public Async Function GetAllAsync() As Task(Of List(Of Categoria)) Implements ICategoriaRepository.GetAllAsync
            Return Await _context.Categorias.AsNoTracking().OrderBy(Function(c) c.Id).ToListAsync()
        End Function

        Public Async Function GetByIdAsync(id As Integer) As Task(Of Categoria) Implements ICategoriaRepository.GetByIdAsync
            Return Await _context.Categorias.FirstOrDefaultAsync(Function(c) c.Id = id)
        End Function

        Public Async Function GetByIdConProductosAsync(id As Integer) As Task(Of Categoria) Implements ICategoriaRepository.GetByIdConProductosAsync
            Return Await _context.Categorias.AsNoTracking().
                Include(Function(c) c.Productos).
                FirstOrDefaultAsync(Function(c) c.Id = id)
        End Function

        Public Async Function ExistsAsync(id As Integer) As Task(Of Boolean) Implements ICategoriaRepository.ExistsAsync
            Return Await _context.Categorias.AnyAsync(Function(c) c.Id = id)
        End Function

        Public Async Function NombreExistsAsync(nombre As String, Optional excludeId As Integer? = Nothing) As Task(Of Boolean) Implements ICategoriaRepository.NombreExistsAsync
            Dim buscado = nombre.Trim().ToLower()
            Dim query = _context.Categorias.Where(Function(c) c.Nombre.ToLower() = buscado)
            If excludeId.HasValue Then
                Dim idExcluido = excludeId.Value
                query = query.Where(Function(c) c.Id <> idExcluido)
            End If
            Return Await query.AnyAsync()
        End Function

        Public Async Function TieneProductosAsync(id As Integer) As Task(Of Boolean) Implements ICategoriaRepository.TieneProductosAsync
            Return Await _context.Productos.AnyAsync(Function(p) p.CategoriaId = id)
        End Function

        Public Async Function AddAsync(categoria As Categoria) As Task(Of Categoria) Implements ICategoriaRepository.AddAsync
            _context.Categorias.Add(categoria)
            Await _context.SaveChangesAsync()
            Return categoria
        End Function

        Public Async Function UpdateAsync(categoria As Categoria) As Task Implements ICategoriaRepository.UpdateAsync
            _context.Categorias.Update(categoria)
            Await _context.SaveChangesAsync()
        End Function

        Public Async Function DeleteAsync(categoria As Categoria) As Task Implements ICategoriaRepository.DeleteAsync
            _context.Categorias.Remove(categoria)
            Await _context.SaveChangesAsync()
        End Function

    End Class

End Namespace
