<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LsArchivosEntrada = New System.Windows.Forms.ListBox()
        Me.LbCarpetaEntrada = New System.Windows.Forms.Label()
        Me.BtCarpetaEntrada = New System.Windows.Forms.Button()
        Me.DgCarpetaEntrada = New System.Windows.Forms.FolderBrowserDialog()
        Me.SuspendLayout()
        '
        'LsArchivosEntrada
        '
        Me.LsArchivosEntrada.FormattingEnabled = True
        Me.LsArchivosEntrada.Location = New System.Drawing.Point(22, 57)
        Me.LsArchivosEntrada.Name = "LsArchivosEntrada"
        Me.LsArchivosEntrada.Size = New System.Drawing.Size(120, 95)
        Me.LsArchivosEntrada.TabIndex = 0
        '
        'LbCarpetaEntrada
        '
        Me.LbCarpetaEntrada.AutoSize = True
        Me.LbCarpetaEntrada.Location = New System.Drawing.Point(148, 24)
        Me.LbCarpetaEntrada.Name = "LbCarpetaEntrada"
        Me.LbCarpetaEntrada.Size = New System.Drawing.Size(0, 13)
        Me.LbCarpetaEntrada.TabIndex = 1
        '
        'BtCarpetaEntrada
        '
        Me.BtCarpetaEntrada.Location = New System.Drawing.Point(22, 19)
        Me.BtCarpetaEntrada.Name = "BtCarpetaEntrada"
        Me.BtCarpetaEntrada.Size = New System.Drawing.Size(109, 23)
        Me.BtCarpetaEntrada.TabIndex = 2
        Me.BtCarpetaEntrada.Text = "Carpeta de entrada"
        Me.BtCarpetaEntrada.UseVisualStyleBackColor = True
        '
        'DgCarpetaEntrada
        '
        Me.DgCarpetaEntrada.SelectedPath = "C:\dev\projects\ScriptsUtil\data"
        Me.DgCarpetaEntrada.ShowNewFolderButton = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.BtCarpetaEntrada)
        Me.Controls.Add(Me.LbCarpetaEntrada)
        Me.Controls.Add(Me.LsArchivosEntrada)
        Me.Name = "Main"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LsArchivosEntrada As System.Windows.Forms.ListBox
    Friend WithEvents LbCarpetaEntrada As System.Windows.Forms.Label
    Friend WithEvents BtCarpetaEntrada As System.Windows.Forms.Button
    Friend WithEvents DgCarpetaEntrada As System.Windows.Forms.FolderBrowserDialog

End Class
