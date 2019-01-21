Imports System.ComponentModel

<TypeConverter(GetType(ExportSettingsConverter))>
Public Class ExportSettings

  Private _ProgressBarValue As Boolean = True
  ''' <summary>
  ''' Indicates if the progressbar's value is exported (TRUE) or if it is his picture (FALSE)
  ''' </summary>
  ''' <returns></returns>
  Public Property ProgressBarValue() As Boolean
    Get
      Return _ProgressBarValue
    End Get
    Set(ByVal value As Boolean)
      _ProgressBarValue = value
    End Set
  End Property

End Class

'https://msdn.microsoft.com/en-us/library/aa302334.aspx
Friend Class ExportSettingsConverter
  Inherits TypeConverter

  Public Overrides Function GetProperties(context As ITypeDescriptorContext, value As Object, filter As Attribute()) As PropertyDescriptorCollection
    Return TypeDescriptor.GetProperties(value, filter)
  End Function

  Public Overrides Function GetPropertiesSupported(context As ITypeDescriptorContext) As Boolean
    Return True
  End Function
End Class
