﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("DataGridViewExVBNET.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property document_preview() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("document_preview", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property fileclose() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("fileclose", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property fileprint() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("fileprint", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property html() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("html", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to   &lt;/body&gt;
        '''&lt;/html&gt;.
        '''</summary>
        Friend ReadOnly Property HTMLFooter() As String
            Get
                Return ResourceManager.GetString("HTMLFooter", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;!DOCTYPE HTML PUBLIC &quot;-//W3C//DTD HTML 4.01 Transitional//EN&quot;&gt;
        '''&lt;html&gt;
        '''  &lt;head&gt;
        '''  &lt;meta http-equiv=&quot;content-type&quot; content=&quot;text/html; charset=utf-8&quot;&gt;
        '''  &lt;style type=&quot;text/css&quot;&gt;
        '''    table, th, td {
        '''      border: 1px solid black;
        '''      border-collapse: collapse;
        '''    }
        '''	th {
        '''		background-color: rgb(79, 129, 189);
        '''		color: white;
        '''	}
        '''  &lt;/style&gt;
        '''  &lt;title&gt;&lt;/title&gt;
        '''  &lt;/head&gt;
        '''  &lt;body&gt;.
        '''</summary>
        Friend ReadOnly Property HTMLHeader() As String
            Get
                Return ResourceManager.GetString("HTMLHeader", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property package_settings() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("package_settings", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property page_excel() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("page_excel", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property recur() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("recur", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Friend ReadOnly Property up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace
