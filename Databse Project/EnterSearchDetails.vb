Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class EnterSearchDetails

    Dim mode As MainMenu.Selection

    Public Sub SetOpenMode(mode As MainMenu.Selection)
        Me.mode = mode
        Me.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Select Case mode
            Case MainMenu.Selection.ViewBalance
                If CStr(LoginPage.loginCustomerID) <> CStr(DatabaseControls.FindValueInTable("Account", "AccountID", TextBox1.Text, "CustomerID")) Then
                    MessageBox.Show("You cannot view the balance of an account you do not own.")
                    Exit Sub
                End If
            Case MainMenu.Selection.ViewTransaction
                If CStr(LoginPage.loginCustomerID) <> CStr(DatabaseControls.FindValueInTable("Account", "AccountID", TextBox1.Text, "CustomerID")) Then
                    MessageBox.Show("You cannot view transactions from accounts you do not own.")
                    Exit Sub
                End If
        End Select

        GenericDatabaseView.SetDetails(mode, CInt(TextBox1.Text))
        Me.Hide()
        GenericDatabaseView.Show()

    End Sub

    Private Sub EnterDetails_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

        If LoginPage.loginSuccessful Then
            Select Case mode
                Case MainMenu.Selection.ViewBalance
                    Label1.Text = "Please enter an account number."
                Case MainMenu.Selection.ViewCustomer
                    Label1.Text = "Please enter a customer ID."
                Case MainMenu.Selection.ViewTransaction
                    Label1.Text = "Please enter an account number."
            End Select
        Else
            Me.Hide()
            MessageBox.Show("You must be logged in.")
            LoginPage.Show()
        End If

    End Sub

End Class