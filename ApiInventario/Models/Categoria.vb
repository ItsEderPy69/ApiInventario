Namespace Models

    Public Class Categoria
        Public Property Id As Integer
        Public Property Nombre As String = String.Empty
        Public Property Descripcion As String
        Public Property Productos As ICollection(Of Producto) = New List(Of Producto)()
    End Class

End Namespace
