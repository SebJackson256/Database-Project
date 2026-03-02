Public Class GenericDatabaseView

    Dim mode As EnterDetails.OpenMode
    Dim accountNumber As Integer
    Dim customerID As Integer

    Private Sub GenericDatabaseView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case mode
            Case EnterDetails.OpenMode.ViewBalance
                DatabaseControls.RunSelectQuery("SELECT Balance FROM Account WHERE AccountID = " & accountNumber & ";", DataGridView1)
            Case EnterDetails.OpenMode.ViewCustomer
                DatabaseControls.RunSelectQuery("SELECT * FROM Customer WHERE CustomerID = " & customerID & ";", DataGridView1)
            Case EnterDetails.OpenMode.ViewTransaction
            Case EnterDetails.OpenMode.MakeTransaction
        End Select
    End Sub

    Public Sub SetDetails(mode As EnterDetails.OpenMode, accountNumber As Integer)
        Me.mode = mode
        Me.accountNumber = accountNumber
    End Sub

End Class