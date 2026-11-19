Imports System.ComponentModel.DataAnnotations

Namespace DTOs

    Public Class CategoriaCreateDto
        <Required(ErrorMessage:="El nombre de la categoria es obligatorio")>
        <StringLength(50, ErrorMessage:="El nombre no puede superar 50 caracteres")>
        Public Property Nombre As String

        <StringLength(200, ErrorMessage:="La descripcion no puede superar 200 caracteres")>
        Public Property Descripcion As String
    End Class

    Public Class CategoriaUpdateDto
        <Required(ErrorMessage:="El nombre de la categoria es obligatorio")>
        <StringLength(50, ErrorMessage:="El nombre no puede superar 50 caracteres")>
        Public Property Nombre As String

        <StringLength(200, ErrorMessage:="La descripcion no puede superar 200 caracteres")>
        Public Property Descripcion As String
    End Class

    Public Class CategoriaResponseDto
        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Descripcion As String
    End Class

    Public Class CategoriaConProductosDto
        Inherits CategoriaResponseDto
        Public Property Productos As List(Of ProductoSimpleDto) = New List(Of ProductoSimpleDto)()
    End Class

End Namespace
