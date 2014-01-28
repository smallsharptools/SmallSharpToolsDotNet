Imports System
Imports Sample.DataContracts

Namespace Sample

    Partial Public Class Composite

        Public ReadOnly Property FullName() As String
            Get
                Return Me.FirstName & " " & Me.LastName
            End Get
        End Property

    End Class

End Namespace
