Imports System.Data.OleDb
Imports System.Data.SqlTypes
Imports System.Environment
Imports System.IO
Imports ADOX

Public Class Form1

    Const dataBaseProvider As String = "Provider=Microsoft.ACE.OLEDB.16.0;"
    Dim myDatabasePath As String
    Dim databaseLoaded As Boolean = False

    Dim connection As OleDbConnection
    Dim adaptor As OleDbDataAdapter
    Dim myDataSet As DataSet

    Dim path As String = IO.Directory.GetParent(IO.Directory.GetParent(My.Application.Info.DirectoryPath).ToString).ToString

    Private Sub Form1_Close() Handles MyBase.Closed
        MainMenu.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DatabaseControls.StartChecks()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        DatabaseControls.LoadDatabase()

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        RunSelectQuery(TextBox2.Text, DataGridView1)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        MainMenu.Show()
    End Sub

End Class
