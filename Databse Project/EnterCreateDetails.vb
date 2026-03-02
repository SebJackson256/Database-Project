Public Class EnterCreateDetails

    Dim mode As MainMenu.Selection

    Public Sub SetOpenMode(mode As MainMenu.Selection)
        Me.mode = mode
    End Sub

    Private Sub EnterCreateDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case mode
            Case MainMenu.Selection.CreateCustomer
                Label3.Show()
                TextBox3.Show()
                Label1.Text = "Please enter your first name."
                Label2.Text = "Please enter your surname."
                Label3.Text = "Please enter your date of birth."
            Case MainMenu.Selection.CreateAccount
                Label1.Text = "Please enter a customer ID."
                Label2.Text = "Please enter a starting balance."
                Label3.Hide()
                TextBox3.Hide()
            Case MainMenu.Selection.MakeTransaction
                Label3.Show()
                TextBox3.Show()
                Label1.Text = "Please enter the source account number."
                Label2.Text = "Please enter the destination account number."
                Label3.Text = "Please enter the amount."
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Label1.Text IsNot Nothing And Label2.Text IsNot Nothing And Label3.Text IsNot Nothing Then
            Dim fieldNames As String()
            Dim fieldValues As Object()
            Select Case mode
                Case MainMenu.Selection.CreateCustomer
                    fieldNames = {"FirstName", "LastName", "DateOfBirth"}
                    fieldValues = {TextBox1.Text, TextBox2.Text, CDate(TextBox3.Text)}
                    DatabaseControls.Insert("Customer", fieldNames, fieldValues)
                Case MainMenu.Selection.CreateAccount
                    fieldNames = {"CustomerID", "OpeningDate", "Balance"}
                    fieldValues = {CInt(TextBox1.Text), DateTime.Now, CDec(TextBox2.Text)}
                    DatabaseControls.Insert("Account", fieldNames, fieldValues)
                Case MainMenu.Selection.MakeTransaction
                    fieldNames = {"Amount", "SourceAccountID", "DestinationAccountID", "TransactionDate"}
                    fieldValues = {TextBox3.Text, TextBox1.Text, TextBox2.Text, DateTime.Now}
                    DatabaseControls.Insert("BankTransaction", fieldNames, fieldValues)
                    'INCOMPLETE
            End Select
        End If
    End Sub

End Class