Partial Public Class SimpleGet2
	Implements Yahoo.Samples.Common.ISample

	''' <summary>
	''' Gets the sample description.
	''' </summary>
	Public ReadOnly Property Description() As String Implements Common.ISample.Description
		Get
			Return "Retrieves the contents of a page or web service call using a GET request with basic error handling."
		End Get
	End Property

	''' <summary>
	''' Gets the sample name.
	''' </summary>
	Public ReadOnly Property Name() As String Implements Common.ISample.Name
		Get
			Return "VB.NET: Simple GET 2"
		End Get
	End Property

	''' <summary>
	''' Gets the sample source filename.
	''' </summary>
	Public ReadOnly Property SourceFile() As String Implements Common.ISample.SourceFile
		Get
			Return "SimpleGet2Sample.vb"
		End Get
	End Property
End Class
