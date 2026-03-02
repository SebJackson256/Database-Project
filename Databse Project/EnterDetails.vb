Public Class EnterDetails

    Enum OpenMode
        ViewBalance
        ViewCustomer
        ViewTransaction
        MakeTransaction
    End Enum

    Dim mode As OpenMode

    Public Sub SetOpenMode(mode As OpenMode)
        Me.mode = mode
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GenericDatabaseView.SetDetails(mode, CInt(TextBox1.Text))
        Me.Hide()
        GenericDatabaseView.Show()
    End Sub

    Private Sub EnterDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case mode
            Case OpenMode.ViewBalance
                Label1.Text = "Please enter an account number."
            Case OpenMode.ViewCustomer
                Label1.Text = "Please enter a customer ID."
            Case OpenMode.ViewTransaction
                Label1.Text = "Please enter an account number."
            Case OpenMode.MakeTransaction
                Label1.Text = "Please enter an account number to make the transaction from."
        End Select
    End Sub

End Class