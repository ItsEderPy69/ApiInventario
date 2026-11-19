Imports System.ComponentModel.DataAnnotations

Namespace DTOs

    Public Class ProductoCreateDto
        <Required(ErrorMessage:="El nombre del producto es obligatorio")>
        <StringLength(50, ErrorMessage:="El nombre no puede superar 50 caracteres")>
        Public Property Nombre As String

        <StringLength(200, ErrorMessage:="La descripcion no puede superar 200 caracteres")>
        Public Property Descripcion As String

        <Range(0.01, 1.0E+15, ErrorMessage:="El precio debe ser mayor que cero")>
        Public Property Precio As Decimal

        <Range(0, Integer.MaxValue, ErrorMessage:="El stock no puede ser negativo")>
        Public Property Stock As Integer

        <Range(1, Integer.MaxValue, ErrorMessage:="La categoria es obligatoria")>
        Public Property CategoriaId As Integer
    End Class

    Public Class ProductoUpdateDto
        <Required(ErrorMessage:="El nombre del producto es obligatorio")>
        <StringLength(50, ErrorMessage:="El nombre no puede superar 50 caracteres")>
        Public Property Nombre As String

        <StringLength(200, ErrorMessage:="La descripcion no puede superar 200 caracteres")>
        Public Property Descripcion As String

        <Range(0.01, 1.0E+15, ErrorMessage:="El precio debe ser mayor que cero")>
        Public Property Precio As Decimal

        <Range(0, Integer.MaxValue, ErrorMessage:="El stock no puede ser negativo")>
        Public Property Stock As Integer

        <Range(1, Integer.MaxValue, ErrorMessage:="La categoria es obligatoria")>
        Public Property CategoriaId As Integer
    End Class

    Public Class ProductoSimpleDto
        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Descripcion As String
        Public Property Precio As Decimal
        Public Property Stock As Integer
        Public Property FechaRegistro As DateTime
    End Class

    Public Class ProductoResponseDto
        Inherits ProductoSimpleDto
        Public Property CategoriaId As Integer
        Public Property Categoria As CategoriaResponseDto
    End Class

End Namespace
