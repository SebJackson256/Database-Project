Public Class MainMenu
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub ViewBalanceButton_Click(sender As Object, e As EventArgs) Handles ViewBalanceButton.Click


    End Sub

    Private Sub ViewCustomerButton_Click(sender As Object, e As EventArgs) Handles ViewCustomerButton.Click

    End Sub

    Private Sub ViewTransactionButton_Click(sender As Object, e As EventArgs) Handles ViewTransactionButton.Click

    End Sub

    Private Sub MakeTransactionButton_Click(sender As Object, e As EventArgs) Handles MakeTransactionButton.Click

    End Sub

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fieldNames As String() = {"firstname", "surname"}
        Dim fieldValues As String() = {"John", "Smith"}
        DatabaseControls.LoadDatabase()
        Insert("People", fieldNames, fieldValues)
    End Sub
End Class