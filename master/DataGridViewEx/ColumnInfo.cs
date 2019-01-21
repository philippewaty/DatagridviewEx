using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGridViewEx
{
  /// <summary>
  /// Class used to save column info for saving/restoring configuration
  /// </summary>
  /// <url>http://www.extensionmethod.net/csharp/datagridview/load-save-configuration</url>
  [Serializable()]
  public sealed class ColumnInfo
  {
    public string Name { get; set; }

    public int DisplayIndex { get; set; }

    public int Width { get; set; }

    public bool Visible { get; set; }

  }

}
