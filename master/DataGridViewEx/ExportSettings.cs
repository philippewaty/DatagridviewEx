using System;
using System.ComponentModel;

namespace DataGridViewEx
{
  [TypeConverter(typeof(ExportSettingsConverter))]
  public class ExportSettings
  {

    /// <summary>
    /// Indicates if the progressbar's value is exported (TRUE) or if it is his picture (FALSE)
    /// </summary>
    /// <returns></returns>
    [Description("Indicates if the progressbar's value is exported (TRUE) or if it is his picture (FALSE)"), Browsable(true), Category("ExportSettings")]
    public bool ProgressBarValue { get; set; }
  }

  //https://msdn.microsoft.com/en-us/library/aa302334.aspx
  internal class ExportSettingsConverter : TypeConverter
  {

    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] filter)
    {
      return TypeDescriptor.GetProperties(value, filter);
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }

}
