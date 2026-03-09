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
        SelectFromDatabase.Show()
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

        LoginPage.Show()
        LoginPage.Hide()


        Label2.Text = ""

        If Not My.Settings.CurrentAccountID = -1 Then
            LoginPage.AutoLogin(My.Settings.CurrentAccountID)
            ShowLoginDetails()
        End If

    End Sub

    Private Sub MainMenu_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

        ShowLoginDetails()

    End Sub

    Private Sub ShowLoginDetails()
        If LoginPage.loginSuccessful Then
            Label2.Text = $"Logged in as {LoginPage.loginEmail}{vbCr} (Customer ID: {LoginPage.loginCustomerID})"
        Else
            Label2.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        MessageBox.Show(CStr(DatabaseControls.GetValueFromTable("SELECT HashedPassword FROM Customer WHERE CustomerID = 1", "HashedPassword")))
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        LoginPage.Show()
    End Sub

End Class