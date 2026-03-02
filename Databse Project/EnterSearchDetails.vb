Public Class EnterSearchDetails

    Dim mode As MainMenu.Selection

    Public Sub SetOpenMode(mode As MainMenu.Selection)
        Me.mode = mode
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GenericDatabaseView.SetDetails(mode, CInt(TextBox1.Text))
        Me.Hide()
        GenericDatabaseView.Show()
    End Sub

    Private Sub EnterDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case mode
            Case MainMenu.Selection.ViewBalance
                Label1.Text = "Please enter an account number."
            Case MainMenu.Selection.ViewCustomer
                Label1.Text = "Please enter a customer ID."
            Case MainMenu.Selection.ViewTransaction
                Label1.Text = "Please enter an account number."
        End Select
    End Sub

End Class