Public Class MainMenu

    Enum Selection
        ViewBalance
        ViewCustomer
        ViewTransaction
        CreateCustomer
        CreateAccount
        MakeTransaction
    End Enum

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub ViewBalanceButton_Click(sender As Object, e As EventArgs) Handles ViewBalanceButton.Click
        EnterSearchDetails.SetOpenMode(Selection.ViewBalance)
        EnterSearchDetails.Show()
    End Sub

    Private Sub ViewCustomerButton_Click(sender As Object, e As EventArgs) Handles ViewCustomerButton.Click
        EnterSearchDetails.SetOpenMode(Selection.ViewCustomer)
        EnterSearchDetails.Show()
    End Sub

    Private Sub ViewTransactionButton_Click(sender As Object, e As EventArgs) Handles ViewTransactionButton.Click
        EnterSearchDetails.SetOpenMode(Selection.ViewTransaction)
        EnterSearchDetails.Show()
    End Sub

    Private Sub MakeTransactionButton_Click(sender As Object, e As EventArgs) Handles MakeTransactionButton.Click
        EnterCreateDetails.SetOpenMode(Selection.MakeTransaction)
        EnterCreateDetails.Show()
    End Sub
    Private Sub CreateCustomerButton_Click(sender As Object, e As EventArgs) Handles CreateCustomerButton.Click
        EnterCreateDetails.SetOpenMode(Selection.CreateCustomer)
        EnterCreateDetails.Show()
    End Sub

    Private Sub CreateAccountButton_Click(sender As Object, e As EventArgs) Handles CreateAccountButton.Click
        EnterCreateDetails.SetOpenMode(Selection.CreateAccount)
        EnterCreateDetails.Show()
    End Sub

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DatabaseControls.LoadDatabase()

    End Sub


End Class