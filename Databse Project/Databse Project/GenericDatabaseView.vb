Public Class GenericDatabaseView

    Dim mode As MainMenu.Selection
    Dim accountNumber As Integer
    Dim customerID As Integer

    Private Sub GenericDatabaseView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case mode
            Case MainMenu.Selection.ViewBalance
                DatabaseControls.RunSelectQuery("SELECT Balance FROM Account WHERE AccountID = " & accountNumber & ";", DataGridView1)
            Case MainMenu.Selection.ViewCustomer
                DatabaseControls.RunSelectQuery("SELECT * FROM Customer WHERE CustomerID = " & customerID & ";", DataGridView1)
            Case MainMenu.Selection.ViewTransaction
                DatabaseControls.RunSelectQuery("SELECT * FROM BankTransaction WHERE SourceAccountID = " & accountNumber & ";", DataGridView1)
        End Select
    End Sub

    Public Sub SetDetails(mode As MainMenu.Selection, input As Integer)
        Me.mode = mode
        Select Case mode
            Case MainMenu.Selection.ViewBalance
                Me.accountNumber = input
            Case MainMenu.Selection.ViewCustomer
                Me.customerID = input
            Case MainMenu.Selection.ViewTransaction
                Me.accountNumber = input
            Case MainMenu.Selection.MakeTransaction
                Me.accountNumber = input
        End Select
    End Sub

End Class