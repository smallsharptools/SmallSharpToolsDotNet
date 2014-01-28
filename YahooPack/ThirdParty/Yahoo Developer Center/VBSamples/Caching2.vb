Partial Public Class Caching2
	Implements Yahoo.Samples.Common.ISample

	''' <summary>
	''' Gets the sample description.
	''' </summary>
	Public ReadOnly Property Description() As String Implements Common.ISample.Description
		Get
			Return "(CODE ONLY) Code sample that demonstrates adding items to the ASP.NET session state."
		End Get
	End Property

	''' <summary>
	''' Gets the sample name.
	''' </summary>
	Public ReadOnly Property Name() As String Implements Common.ISample.Name
		Get
			Return "VB.NET: Session State Caching"
		End Get
	End Property

	''' <summary>
	''' Gets the sample source filename.
	''' </summary>
	Public ReadOnly Property SourceFile() As String Implements Common.ISample.SourceFile
		Get
			Return "Caching2Sample.vb"
		End Get
	End Property

	Public Sub RunSample() Implements Common.ISample.RunSample

		Console.WriteLine("This sample does not produce any results. Please refer to the sample source.")

	End Sub

End Class
