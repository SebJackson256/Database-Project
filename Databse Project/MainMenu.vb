Public Class MainMenu
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub ViewBalanceButton_Click(sender As Object, e As EventArgs) Handles ViewBalanceButton.Click
        EnterDetails.SetOpenMode(EnterDetails.OpenMode.ViewBalance)
        EnterDetails.Show()
    End Sub

    Private Sub ViewCustomerButton_Click(sender As Object, e As EventArgs) Handles ViewCustomerButton.Click
        EnterDetails.SetOpenMode(EnterDetails.OpenMode.ViewCustomer)
        EnterDetails.Show()
    End Sub

    Private Sub ViewTransactionButton_Click(sender As Object, e As EventArgs) Handles ViewTransactionButton.Click
        EnterDetails.SetOpenMode(EnterDetails.OpenMode.ViewTransaction)
        EnterDetails.Show()
    End Sub

    Private Sub MakeTransactionButton_Click(sender As Object, e As EventArgs) Handles MakeTransactionButton.Click
        EnterDetails.SetOpenMode(EnterDetails.OpenMode.MakeTransaction)
        EnterDetails.Show()
    End Sub

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DatabaseControls.LoadDatabase()

    End Sub

End Class