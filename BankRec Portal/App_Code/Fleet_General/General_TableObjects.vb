Imports Microsoft.VisualBasic
'This contains the info object of the various objects that are general to all modules
Public Class APP_ActivitiesLogInfo

#Region "Private Members"
    Dim _iD As Integer
    Dim _userID As Integer
    Dim _modules_ActionsID As Integer
    Dim _actionTime As String
    Dim _actionDate As String
    Dim _description As String
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(ByVal iD As Integer, ByVal userID As Integer, ByVal modules_ActionsID As Integer, ByVal actionTime As String, ByVal actionDate As String, ByVal description As String)
        Me.ID = iD
        Me.UserID = userID
        Me.Modules_ActionsID = modules_ActionsID
        Me.ActionTime = actionTime
        Me.ActionDate = actionDate
        Me.Description = description
    End Sub
#End Region

#Region "Public Properties"
    Public Property ID() As Integer
        Get
            Return _iD
        End Get
        Set(ByVal Value As Integer)
            _iD = Value
        End Set
    End Property

    Public Property UserID() As Integer
        Get
            Return _userID
        End Get
        Set(ByVal Value As Integer)
            _userID = Value
        End Set
    End Property

    Public Property Modules_ActionsID() As Integer
        Get
            Return _modules_ActionsID
        End Get
        Set(ByVal Value As Integer)
            _modules_ActionsID = Value
        End Set
    End Property

    Public Property ActionTime() As String
        Get
            Return _actionTime
        End Get
        Set(ByVal Value As String)
            _actionTime = Value
        End Set
    End Property

    Public Property ActionDate() As String
        Get
            Return _actionDate
        End Get
        Set(ByVal Value As String)
            _actionDate = Value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal Value As String)
            _description = Value
        End Set
    End Property
#End Region

End Class

Public Class DashBoard_TableObject
    Public Class DashBoardInfo

#Region "Private Members"
        Dim _iD As Integer
        Dim _title As String
        Dim _description As String
        Dim _picUrl As String
        Dim _destinationUrl As String
        Dim _bodyClass As String
        Dim _headerClass As String
        Dim _status As Boolean
        Dim _display As String
        Dim _displayOrder As Integer
        Dim _roleId As Integer
#End Region

#Region "Constructors"
        Public Sub New()
        End Sub

        Public Sub New(ByVal iD As Integer, ByVal title As String, ByVal description As String, ByVal picUrl As String, ByVal destinationUrl As String, ByVal bodyClass As String, ByVal headerClass As String, ByVal status As Boolean, ByVal display As String, ByVal displayOrder As Integer, ByVal roleId As Integer)
            Me.ID = iD
            Me.Title = title
            Me.Description = description
            Me.PicUrl = picUrl
            Me.DestinationUrl = destinationUrl
            Me.BodyClass = bodyClass
            Me.HeaderClass = headerClass
            Me.Status = status
            Me.Display = display
            Me.DisplayOrder = displayOrder
            Me.RoleID = roleId
        End Sub
#End Region

#Region "Public Properties"
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal Value As Integer)
                _iD = Value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal Value As String)
                _title = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
            End Set
        End Property

        Public Property PicUrl() As String
            Get
                Return _picUrl
            End Get
            Set(ByVal Value As String)
                _picUrl = Value
            End Set
        End Property

        Public Property DestinationUrl() As String
            Get
                Return _destinationUrl
            End Get
            Set(ByVal Value As String)
                _destinationUrl = Value
            End Set
        End Property

        Public Property BodyClass() As String
            Get
                Return _bodyClass
            End Get
            Set(ByVal Value As String)
                _bodyClass = Value
            End Set
        End Property

        Public Property HeaderClass() As String
            Get
                Return _headerClass
            End Get
            Set(ByVal Value As String)
                _headerClass = Value
            End Set
        End Property

        Public Property Status() As Boolean
            Get
                Return _status
            End Get
            Set(ByVal Value As Boolean)
                _status = Value
            End Set
        End Property

        Public Property Display() As String
            Get
                Return _display
            End Get
            Set(ByVal Value As String)
                _display = Value
            End Set
        End Property

        Public Property DisplayOrder() As Integer
            Get
                Return _displayOrder
            End Get
            Set(ByVal Value As Integer)
                _displayOrder = Value
            End Set
        End Property
        Public Property RoleID() As Integer
            Get
                Return _roleId
            End Get
            Set(ByVal Value As Integer)
                _roleId = Value
            End Set
        End Property
#End Region

    End Class

End Class


#Region "Sorting Generic List Sample"
Public Class PositionScore
    Implements IComparable(Of PositionScore)

    Public Overloads Function CompareTo(ByVal other As PositionScore) As Integer _
        Implements IComparable(Of PositionScore).CompareTo
        Return Score.CompareTo(other.Score)
    End Function

    Private _StudentID As Integer
    Private _SubjectID As Integer
    Private _Score As Double

    Public Property StudentID() As Integer
        Get
            Return _StudentID
        End Get
        Set(ByVal value As Integer)
            _StudentID = value
        End Set
    End Property
    Public Property SubjectID() As Integer
        Get
            Return _SubjectID
        End Get
        Set(ByVal value As Integer)
            _SubjectID = value
        End Set
    End Property
    Public Property Score() As Double
        Get
            Return _Score
        End Get
        Set(ByVal value As Double)
            _Score = value
        End Set
    End Property

    Public Sub New(ByVal StudentID As Integer, ByVal SubjectID As Integer, ByVal Score As Double)
        Me.StudentID = StudentID
        Me.SubjectID = SubjectID
        Me.Score = Score
    End Sub
End Class
#End Region

Public Class LectureNote_Object

#Region "Member Data"

    Private _ID As Integer
    Private _TermID As Integer
    Private _UserID As Integer
    Private _TeacherID As Integer
    Private _SubjectID As String
    Private _ClassID As Integer
    Private _DateAdded As String
    Private _Week As Integer
    Private _Url As String
    Private _SessionID As Integer

#End Region

#Region "Property Accessor"

    Public Property SessionID() As Integer
        Get
            Return _SessionID
        End Get
        Set(ByVal value As Integer)
            _SessionID = value
        End Set
    End Property

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Public Property TermID() As Integer
        Get
            Return _TermID
        End Get
        Set(ByVal value As Integer)
            _TermID = value
        End Set
    End Property

    Public Property UserID() As Integer
        Get
            Return _UserID
        End Get
        Set(ByVal value As Integer)
            _UserID = value
        End Set
    End Property

    Public Property TeacherID() As Integer
        Get
            Return _TeacherID
        End Get
        Set(ByVal value As Integer)
            _TeacherID = value
        End Set
    End Property

    Public Property SubjectID() As String
        Get
            Return _SubjectID
        End Get
        Set(ByVal value As String)
            _SubjectID = value
        End Set
    End Property

    Public Property ClassID() As Integer
        Get
            Return _ClassID
        End Get
        Set(ByVal value As Integer)
            _ClassID = value
        End Set
    End Property

    Public Property DateAdded() As String
        Get
            Return _DateAdded
        End Get
        Set(ByVal value As String)
            _DateAdded = value
        End Set
    End Property

    Public Property Week() As Integer
        Get
            Return _Week
        End Get
        Set(ByVal value As Integer)
            _Week = value
        End Set
    End Property

    Public Property Url() As String
        Get
            Return _Url
        End Get
        Set(ByVal value As String)
            _Url = value
        End Set
    End Property

#End Region

End Class

Public Class Assignment_Object

#Region "Member Data"

    Private _ID As Integer
    Private _TeacherID As Integer
    Private _ArmID As Integer
    Private _SubjectID As String
    Private _SubmissionDate As String
    Private _AssignmentType As Integer
    Private _AssignmentTitle As String
    Private _Assignment As String
    Private _DateCreated As String
    Private _TimeCreated As String
    Private _UserID As Integer
    Private _SessionID As Integer


#End Region

#Region "Property Accessor"

    Public Property SessionID() As Integer
        Get
            Return _SessionID
        End Get
        Set(ByVal value As Integer)
            _SessionID = value
        End Set
    End Property

    Public Property UserID() As Integer
        Get
            Return _UserID
        End Get
        Set(ByVal value As Integer)
            _UserID = value
        End Set
    End Property

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Public Property TeacherID() As Integer
        Get
            Return _TeacherID
        End Get
        Set(ByVal value As Integer)
            _TeacherID = value
        End Set
    End Property

    Public Property ArmID() As Integer
        Get
            Return _ArmID
        End Get
        Set(ByVal value As Integer)
            _ArmID = value
        End Set
    End Property

    Public Property SubjectID() As String
        Get
            Return _SubjectID
        End Get
        Set(ByVal value As String)
            _SubjectID = value
        End Set
    End Property

    Public Property SubmissionDate() As String
        Get
            Return _SubmissionDate
        End Get
        Set(ByVal value As String)
            _SubmissionDate = value
        End Set
    End Property

    Public Property AssignmentType() As Integer
        Get
            Return _AssignmentType
        End Get
        Set(ByVal value As Integer)
            _AssignmentType = value
        End Set
    End Property

    Public Property AssignmentTitle() As String
        Get
            Return _AssignmentTitle
        End Get
        Set(ByVal value As String)
            _AssignmentTitle = value
        End Set
    End Property

    Public Property Assignment() As String
        Get
            Return _Assignment
        End Get
        Set(ByVal value As String)
            _Assignment = value
        End Set
    End Property

    Public Property DateCreated() As String
        Get
            Return _DateCreated
        End Get
        Set(ByVal value As String)
            _DateCreated = value
        End Set
    End Property

    Public Property TimeCreated() As String
        Get
            Return _TimeCreated
        End Get
        Set(ByVal value As String)
            _TimeCreated = value
        End Set
    End Property

#End Region

End Class
