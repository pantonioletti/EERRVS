Public Class Main

    Private Sub BtCarpetaEntrada_Click(sender As Object, e As EventArgs) Handles BtCarpetaEntrada.Click
        Dim path As String
        DgCarpetaEntrada.ShowDialog()
        path = DgCarpetaEntrada.SelectedPath
        MsgBox(path)

    End Sub

End Class
