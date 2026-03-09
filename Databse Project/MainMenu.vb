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
    End Sub

    Private Sub ViewCustomerButton_Click(sender As Object, e As EventArgs) Handles ViewCustomerButton.Click
        EnterSearchDetails.SetOpenMode(Selection.ViewCustomer)
    End Sub

    Private Sub ViewTransactionButton_Click(sender As Object, e As EventArgs) Handles ViewTransactionButton.Click
        EnterSearchDetails.SetOpenMode(Selection.ViewTransaction)
    End Sub

    Private Sub MakeTransactionButton_Click(sender As Object, e As EventArgs) Handles MakeTransactionButton.Click
        EnterCreateDetails.SetOpenMode(Selection.MakeTransaction)
    End Sub
    Private Sub CreateCustomerButton_Click(sender As Object, e As EventArgs) Handles CreateCustomerButton.Click
        EnterCreateDetails.SetOpenMode(Selection.CreateCustomer)
    End Sub

    Private Sub CreateAccountButton_Click(sender As Object, e As EventArgs) Handles CreateAccountButton.Click
        EnterCreateDetails.SetOpenMode(Selection.CreateAccount)
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

    Public Sub ShowLoginDetails()
        If LoginPage.loginSuccessful Then
            Label2.Text = $"Logged in as {LoginPage.loginEmail} (Customer ID: {LoginPage.loginCustomerID})"
            Button2.Text = "Sign out"
        Else
            Label2.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If LoginPage.loginSuccessful Then
            MessageBox.Show("Signed out")
            Button2.Text = "Login..."
            LoginPage.loginSuccessful = False
            ShowLoginDetails()
            My.Settings.CurrentAccountID = -1
            My.Settings.Save()
        Else
            LoginPage.Show()
        End If

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        ShowLoginDetails()
    End Sub

End Class