Partial Public Class AuthenticatedRequest1
	Implements Yahoo.Samples.Common.ISample

	''' <summary>
	''' Gets the sample description.
	''' </summary>
	Public ReadOnly Property Description() As String Implements Common.ISample.Description
		Get
			Return "Performs an HTTP authenticated request to del.icio.us."
		End Get
	End Property

	''' <summary>
	''' Gets the sample name.
	''' </summary>
	Public ReadOnly Property Name() As String Implements Common.ISample.Name
		Get
			Return "VB.NET: Authenticated Request"
		End Get
	End Property

	''' <summary>
	''' Gets the sample source filename.
	''' </summary>
	Public ReadOnly Property SourceFile() As String Implements Common.ISample.SourceFile
		Get
			Return "AuthenticatedRequest1Sample.vb"
		End Get
	End Property
End Class
