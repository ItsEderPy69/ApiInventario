Imports ApiInventario.Data
Imports ApiInventario.Models
Imports Microsoft.EntityFrameworkCore

Namespace Repositories

    Public Class ProductoRepository
        Implements IProductoRepository

        Private ReadOnly _context As InventarioDbContext

        Public Sub New(context As InventarioDbContext)
            _context = context
        End Sub

        Public Async Function GetAllConCategoriaAsync() As Task(Of List(Of Producto)) Implements IProductoRepository.GetAllConCategoriaAsync
            Return Await _context.Productos.AsNoTracking().
                Include(Function(p) p.Categoria).
                OrderBy(Function(p) p.Id).
                ToListAsync()
        End Function

        Public Async Function GetByIdConCategoriaAsync(id As Integer) As Task(Of Producto) Implements IProductoRepository.GetByIdConCategoriaAsync
            Return Await _context.Productos.AsNoTracking().
                Include(Function(p) p.Categoria).
                FirstOrDefaultAsync(Function(p) p.Id = id)
        End Function

        Public Async Function GetByIdAsync(id As Integer) As Task(Of Producto) Implements IProductoRepository.GetByIdAsync
            Return Await _context.Productos.FirstOrDefaultAsync(Function(p) p.Id = id)
        End Function

        Public Async Function GetByCategoriaAsync(categoriaId As Integer) As Task(Of List(Of Producto)) Implements IProductoRepository.GetByCategoriaAsync
            Return Await _context.Productos.AsNoTracking().
                Include(Function(p) p.Categoria).
                Where(Function(p) p.CategoriaId = categoriaId).
                OrderBy(Function(p) p.Id).
                ToListAsync()
        End Function

        Public Async Function NombreExistsAsync(nombre As String, Optional excludeId As Integer? = Nothing) As Task(Of Boolean) Implements IProductoRepository.NombreExistsAsync
            Dim buscado = nombre.Trim().ToLower()
            Dim query = _context.Productos.Where(Function(p) p.Nombre.ToLower() = buscado)
            If excludeId.HasValue Then
                Dim idExcluido = excludeId.Value
                query = query.Where(Function(p) p.Id <> idExcluido)
            End If
            Return Await query.AnyAsync()
        End Function

        Public Async Function AddAsync(producto As Producto) As Task(Of Producto) Implements IProductoRepository.AddAsync
            _context.Productos.Add(producto)
            Await _context.SaveChangesAsync()
            Return producto
        End Function

        Public Async Function UpdateAsync(producto As Producto) As Task Implements IProductoRepository.UpdateAsync
            _context.Productos.Update(producto)
            Await _context.SaveChangesAsync()
        End Function

        Public Async Function DeleteAsync(producto As Producto) As Task Implements IProductoRepository.DeleteAsync
            _context.Productos.Remove(producto)
            Await _context.SaveChangesAsync()
        End Function

    End Class

End Namespace
