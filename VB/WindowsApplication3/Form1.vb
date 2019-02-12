Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports System
Imports System.Data
Imports System.Windows.Forms


Namespace DXSample
	Partial Public Class Form1
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
		End Sub
		Public Sub InitData()
			Dim dt = New DataTable()
			dt.Columns.Add("CustomerID", GetType(Int32))
			dt.Columns.Add("FirstName", GetType(String))
			dt.Columns.Add("LastName", GetType(String))
			dt.Columns.Add("Age", GetType(Int32))
			For i As Integer = 0 To 4
				dt.Rows.Add(New Object() { i, String.Format("FirstName {0}", i), String.Format("LastName {0}", i), 20 + i })
			Next i
			gridControl1.DataSource = dt
		End Sub
		Private helper As AutoHeightHelper
		Private Sub OnFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			InitData()
			gridControl1.ForceInitialize()
			helper = New AutoHeightHelper(gridView1)
			helper.EnableColumnPanelAutoHeight()
		End Sub
		Private Sub OnFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			helper.DisableColumnPanelAutoHeight()
		End Sub
	End Class
End Namespace
