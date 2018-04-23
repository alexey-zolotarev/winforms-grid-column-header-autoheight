Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Drawing


Namespace DXSample
	Partial Public Class Form1
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
		End Sub
		Public Sub InitData()
			For i As Integer = 0 To 4
				dataSet11.Tables(0).Rows.Add(New Object() { i, String.Format("FirstName {0}", i), String.Format("LastName {0}", i), 20 + i })
			Next i
		End Sub
		Private helper As AutoHeightHelper
		Private Sub OnFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			InitData()
			gridControl1.ForceInitialize()
			helper = New AutoHeightHelper(gridView1)
			helper.EnableColumnPanelAutoHeight()
		End Sub

		Private Overloads Sub OnFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			helper.DisableColumnPanelAutoHeight()
		End Sub
	End Class
End Namespace
