Public Class Users

  Private _Id As Integer
  Public Property Id() As Integer
    Get
      Return _Id
    End Get
    Set(ByVal value As Integer)
      _Id = value
    End Set
  End Property

  Private _Name As String
  Public Property Name() As String
    Get
      Return _Name
    End Get
    Set(ByVal value As String)
      _Name = value
    End Set
  End Property

  Private _Login As String
  Public Property Login() As String
    Get
      Return _Login
    End Get
    Set(ByVal value As String)
      _Login = value
    End Set
  End Property

  Private _Password As String
  Public Property Password() As String
    Get
      Return _Password
    End Get
    Set(ByVal value As String)
      _Password = value
    End Set
  End Property

  Private _DNais As Date
  Public Property DNais() As Date
    Get
      Return _DNais
    End Get
    Set(ByVal value As Date)
      _DNais = value
    End Set
  End Property

  Private _Progress As Integer
  Public Property Progress() As Integer
    Get
      Return _Progress
    End Get
    Set(ByVal value As Integer)
      _Progress = value
    End Set
  End Property

  Public Sub New(ByVal id As Integer, ByVal name As String, ByVal login As String, ByVal password As String, ByVal dnais As Date, ByVal progress As Integer)
    Me.Id = id
    Me.Name = name
    Me.Login = login
    Me.Password = password
    Me.DNais = dnais
    Me.Progress = progress
  End Sub

End Class
