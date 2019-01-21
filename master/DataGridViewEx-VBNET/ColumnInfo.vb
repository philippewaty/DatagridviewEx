''' <summary>
''' Class used to save column info for saving/restoring configuration
''' </summary>
''' <url>http://www.extensionmethod.net/csharp/datagridview/load-save-configuration</url>
<Serializable>
Public NotInheritable Class ColumnInfo

  Private m_Name As String
  Public Property Name() As String
    Get
      Return m_Name
    End Get
    Set
      m_Name = Value
    End Set
  End Property

  Private m_DisplayIndex As Integer
  Public Property DisplayIndex() As Integer
    Get
      Return m_DisplayIndex
    End Get
    Set
      m_DisplayIndex = Value
    End Set
  End Property

  Private m_Width As Integer
  Public Property Width() As Integer
    Get
      Return m_Width
    End Get
    Set
      m_Width = Value
    End Set
  End Property

  Private m_Visible As Boolean
  Public Property Visible() As Boolean
    Get
      Return m_Visible
    End Get
    Set
      m_Visible = Value
    End Set
  End Property

End Class
