Public Class LoginPage

    Public loginSuccessful As Boolean = False
    Public loginEmail As String
    Public loginCustomerID As Integer
    Public rememberMeChecked As Boolean = False

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim customerID As Integer

        If FindValueInTable("Customer", "Email", TextBox1.Text, "CustomerID") <> -1 Then

            customerID = DatabaseControls.FindValueInTable("Customer", "Email", TextBox1.Text, "CustomerID")

            If CStr(DatabaseControls.GetValueFromTable($"SELECT HashedPassword FROM Customer WHERE CustomerID = {customerID}", "HashedPassword")) = Utils.GetSHA256Hash(TextBox2.Text) Then

                MessageBox.Show("Login successful.")
                MainMenu.Show()

                loginSuccessful = True

                loginEmail = TextBox1.Text
                loginCustomerID = customerID

                If CheckBox1.Checked Then
                    rememberMeChecked = True
                    My.Settings.CurrentAccountID = customerID
                Else my.Settings.CurrentAccountID = -1
                End If

                My.Settings.Save()

                Me.Hide()

            Else
                MessageBox.Show("Incorrect password.")
            End If

        Else MessageBox.Show("Email not found.")

        End If

    End Sub

    Private Sub LoginPage_Load(sender As Object, e As EventArgs) Handles MyBase.Activated

        If MyBase.Visible Then
            TextBox1.Clear()
            TextBox2.Clear()
        End If

    End Sub

    Public Sub AutoLogin(customerID As Integer)

        loginSuccessful = True
        loginCustomerID = customerID
        loginEmail = CStr(DatabaseControls.GetValueFromTable($"SELECT Email FROM Customer WHERE CustomerID = {customerID}", "Email"))

    End Sub

End Class