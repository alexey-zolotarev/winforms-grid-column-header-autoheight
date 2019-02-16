Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Drawing

Namespace DXSample
	Public Class AutoHeightHelper
		Private view As GridView
		Public Sub New(ByVal view As GridView)
			Me.view = view
			EnableColumnPanelAutoHeight()
		End Sub

		Public Sub EnableColumnPanelAutoHeight()
			SetColumnPanelHeight()
			SubscribeToEvents()
		End Sub

		Private Sub SubscribeToEvents()
			AddHandler view.ColumnWidthChanged, AddressOf OnColumnWidthChanged
			AddHandler view.GridControl.Resize, AddressOf OnGridControlResize
			AddHandler view.EndSorting, AddressOf OnGridColumnEndSorting
		End Sub

		Private Sub OnGridColumnEndSorting(ByVal sender As Object, ByVal e As EventArgs)
			view.GridControl.BeginInvoke(New MethodInvoker(AddressOf SetColumnPanelHeight))
		End Sub

		Private Sub OnGridControlResize(ByVal sender As Object, ByVal e As EventArgs)
			SetColumnPanelHeight()
		End Sub

		Private Sub OnColumnWidthChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ColumnEventArgs)
			SetColumnPanelHeight()
		End Sub

		Private Sub SetColumnPanelHeight()
			Dim viewInfo As GridViewInfo = TryCast(view.GetViewInfo(), GridViewInfo)
			Dim height As Integer = 0
			For i As Integer = 0 To view.VisibleColumns.Count - 1
				height = Math.Max(GetColumnBestHeight(viewInfo, view.VisibleColumns(i)), height)
			Next i
			view.ColumnPanelRowHeight = height
		End Sub

		Private Function GetColumnBestHeight(ByVal viewInfo As GridViewInfo, ByVal column As GridColumn) As Integer
			Dim ex As GridColumnInfoArgs = viewInfo.ColumnsInfo(column)
			If ex Is Nothing Then
				viewInfo.GInfo.AddGraphics(Nothing)
				ex = New GridColumnInfoArgs(viewInfo.GInfo.Cache, Nothing)
				Try
					ex.InnerElements.Add(New DrawElementInfo(New GlyphElementPainter(), New GlyphElementInfoArgs(viewInfo.View.Images, 0, Nothing), StringAlignment.Near))
					If viewInfo.View.CanShowFilterButton(Nothing) Then
						ex.InnerElements.Add(viewInfo.Painter.ElementsPainter.FilterButton, New GridFilterButtonInfoArgs())
					End If
					ex.SetAppearance(viewInfo.PaintAppearance.HeaderPanel)
					ex.Caption = column.Caption
					ex.CaptionRect = New Rectangle(0, 0, column.Width - 20, 17)
				Finally
					viewInfo.GInfo.ReleaseGraphics()
				End Try
			End If
				Dim grInfo As New GraphicsInfo()
				grInfo.AddGraphics(Nothing)
				ex.Cache = grInfo.Cache
				Dim canDrawMore As Boolean = True
				Dim captionSize As Size = CalcCaptionTextSize(grInfo.Cache, TryCast(ex, HeaderObjectInfoArgs), column.GetCaption())
				Dim res As Size = ex.InnerElements.CalcMinSize(grInfo.Graphics, canDrawMore)
				res.Height = Math.Max(res.Height, captionSize.Height)
				res.Width += captionSize.Width
				res = viewInfo.Painter.ElementsPainter.Column.CalcBoundsByClientRectangle(ex, New Rectangle(Point.Empty, res)).Size
				grInfo.ReleaseGraphics()
				Return res.Height
		End Function

		Private Function CalcCaptionTextSize(ByVal cache As GraphicsCache, ByVal ee As HeaderObjectInfoArgs, ByVal caption As String) As Size
			Dim captionSize As Size = ee.Appearance.CalcTextSize(cache, caption, ee.CaptionRect.Width).ToSize()
			captionSize.Height += 1
			captionSize.Width += 1
			Return captionSize
		End Function

		Public Sub DisableColumnPanelAutoHeight()
			UnsubscribeFromEvents()
		End Sub

		Private Sub UnsubscribeFromEvents()
			RemoveHandler view.ColumnWidthChanged, AddressOf OnColumnWidthChanged
			RemoveHandler view.GridControl.Resize, AddressOf OnGridControlResize
			RemoveHandler view.EndSorting, AddressOf OnGridColumnEndSorting
		End Sub
	End Class
End Namespace