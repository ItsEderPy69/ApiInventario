Imports ApiInventario.DTOs
Imports ApiInventario.Models

Namespace Services

    Friend Module Mapper

        Public Function ToDto(c As Categoria) As CategoriaResponseDto
            Return New CategoriaResponseDto With {.Id = c.Id, .Nombre = c.Nombre, .Descripcion = c.Descripcion}
        End Function

        Public Function ToSimpleDto(p As Producto) As ProductoSimpleDto
            Return New ProductoSimpleDto With {
                .Id = p.Id, .Nombre = p.Nombre, .Descripcion = p.Descripcion,
                .Precio = p.Precio, .Stock = p.Stock, .FechaRegistro = p.FechaRegistro}
        End Function

        Public Function ToDto(p As Producto) As ProductoResponseDto
            Return New ProductoResponseDto With {
                .Id = p.Id, .Nombre = p.Nombre, .Descripcion = p.Descripcion,
                .Precio = p.Precio, .Stock = p.Stock, .FechaRegistro = p.FechaRegistro,
                .CategoriaId = p.CategoriaId,
                .Categoria = If(p.Categoria Is Nothing, Nothing, ToDto(p.Categoria))}
        End Function

        Public Function ToConProductosDto(c As Categoria) As CategoriaConProductosDto
            Return New CategoriaConProductosDto With {
                .Id = c.Id, .Nombre = c.Nombre, .Descripcion = c.Descripcion,
                .Productos = c.Productos.OrderBy(Function(p) p.Id).Select(Function(p) ToSimpleDto(p)).ToList()}
        End Function

    End Module

End Namespace
