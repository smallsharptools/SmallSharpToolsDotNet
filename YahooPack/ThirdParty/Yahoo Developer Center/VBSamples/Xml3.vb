Partial Public Class Xml3
	Implements Yahoo.Samples.Common.ISample

	''' <summary>
	''' Gets the sample description.
	''' </summary>
	Public ReadOnly Property Description() As String Implements Common.ISample.Description
		Get
			Return "Shows a simple example on using XPathNavigator/XPathDocument."
		End Get
	End Property
	''' <summary>
	''' Gets the sample name.
	''' </summary>

	Public ReadOnly Property Name() As String Implements Common.ISample.Name
		Get
			Return "VB.NET: XPathNavigator/XPathDocument"
		End Get
	End Property

	''' <summary>
	''' Gets the sample source filename.
	''' </summary>
	Public ReadOnly Property SourceFile() As String Implements Common.ISample.SourceFile
		Get
			Return "Xml3Sample.vb"
		End Get
	End Property
End Class
