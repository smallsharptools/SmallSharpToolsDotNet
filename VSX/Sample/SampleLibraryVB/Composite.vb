Imports System
Imports Sample.DataContracts

Namespace Sample
    
    Partial Public Class Composite
        
        Private _summary As SummaryDataContract
        
        Private _detail As DetailDataContract
        
        Public Sub New(ByVal summary As SummaryDataContract, ByVal detail As DetailDataContract)
            MyBase.New
            _summary = summary
            _detail = detail
        End Sub
        
        '''<summary>
        ''' SummaryDataContract.FirstName
        ''' </summary>
        Public Overridable Property FirstName() As String
            Get
                Return Me._summary.FirstName
            End Get
            Set
                Me._summary.FirstName = value
            End Set
        End Property
        
        '''<summary>
        ''' SummaryDataContract.LastName
        ''' </summary>
        Public Overridable Property LastName() As String
            Get
                Return Me._summary.LastName
            End Get
            Set
                Me._summary.LastName = value
            End Set
        End Property
        
        '''<summary>
        ''' SummaryDataContract.Phone
        ''' </summary>
        Public Overridable Property Phone() As String
            Get
                Return Me._summary.Phone
            End Get
            Set
                Me._summary.Phone = value
            End Set
        End Property
        
        '''<summary>
        ''' SummaryDataContract.ID
        ''' </summary>
        Public Overridable ReadOnly Property ID() As Integer
            Get
                Return Me._summary.ID
            End Get
        End Property
        
        '''<summary>
        ''' SummaryDataContract.Created
        ''' </summary>
        Public Overridable ReadOnly Property Created() As Date
            Get
                Return Me._summary.Created
            End Get
        End Property
        
        '''<summary>
        ''' SummaryDataContract.Modified
        ''' </summary>
        Public Overridable ReadOnly Property Modified() As Date
            Get
                Return Me._summary.Modified
            End Get
        End Property
        
        '''<summary>
        ''' DetailDataContract.Address1
        ''' </summary>
        Public Overridable Property Address1() As String
            Get
                Return Me._detail.Address1
            End Get
            Set
                Me._detail.Address1 = value
            End Set
        End Property
        
        '''<summary>
        ''' DetailDataContract.Address2
        ''' </summary>
        Public Overridable Property Address2() As String
            Get
                Return Me._detail.Address2
            End Get
            Set
                Me._detail.Address2 = value
            End Set
        End Property
        
        '''<summary>
        ''' DetailDataContract.City
        ''' </summary>
        Public Overridable Property City() As String
            Get
                Return Me._detail.City
            End Get
            Set
                Me._detail.City = value
            End Set
        End Property
        
        '''<summary>
        ''' DetailDataContract.State
        ''' </summary>
        Public Overridable Property State() As String
            Get
                Return Me._detail.State
            End Get
            Set
                Me._detail.State = value
            End Set
        End Property
        
        '''<summary>
        ''' DetailDataContract.Zip
        ''' </summary>
        Public Overridable Property Zip() As String
            Get
                Return Me._detail.Zip
            End Get
            Set
                Me._detail.Zip = value
            End Set
        End Property
        
        '''<summary>
        ''' DetailDataContract.DetailID
        ''' </summary>
        Public Overridable ReadOnly Property DetailID() As Integer
            Get
                Return Me._detail.ID
            End Get
        End Property
        
        '''<summary>
        ''' DetailDataContract.DetailCreated
        ''' </summary>
        Public Overridable ReadOnly Property DetailCreated() As Date
            Get
                Return Me._detail.Created
            End Get
        End Property
        
        '''<summary>
        ''' DetailDataContract.DetailModified
        ''' </summary>
        Public Overridable ReadOnly Property DetailModified() As Date
            Get
                Return Me._detail.Modified
            End Get
        End Property
    End Class
End Namespace

