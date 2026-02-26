Imports System.Data.OleDb
Imports System.Data.SqlTypes
Imports System.Environment
Imports System.IO
Imports ADOX

Public Class Form1

    Const dataBaseProvider As String = "Provider=Microsoft.ACE.OLEDB.16.0;"
    Dim myDatabasePath As String
    Dim databaseLoaded As Boolean = False

    Dim connection As OleDbConnection
    Dim adaptor As OleDbDataAdapter
    Dim myDataSet As DataSet

    Dim path As String = IO.Directory.GetParent(IO.Directory.GetParent(My.Application.Info.DirectoryPath).ToString).ToString

    Private Sub Form1_Close() Handles MyBase.Closed
        MainMenu.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not IO.File.Exists(path & "\BankDatabase.accdb") Then

            Dim cat As New Catalog
            cat.Create(dataBaseProvider & "Data Source=" & path & "\BankDatabase.accdb" & ";")

            connection = New OleDbConnection(dataBaseProvider & "Data Source=" & path & "\BankDatabase.accdb" & ";")
            connection.Open()

            Dim command As New OleDbCommand("", connection)

            command.Connection = connection

            command.CommandText =
"CREATE TABLE Customer (
    CustomerID AUTOINCREMENT PRIMARY KEY,
    FirstName TEXT,
    LastName TEXT,
    DateOfBirth DATE
);"

            command.ExecuteNonQuery()

            command.CommandText =
"CREATE TABLE Account (
    AccountID AUTOINCREMENT PRIMARY KEY,
    CustomerID INTEGER,
    OpeningDate DATE,
    ClosingDate DATE,
    Balance CURRENCY,
    CONSTRAINT fk_AccountCustomer
        FOREIGN KEY (CustomerID)
        REFERENCES Customer(CustomerID)
);"
            command.ExecuteNonQuery()
            command.CommandText =
"CREATE TABLE BankTransaction (
    TransactionID AUTOINCREMENT PRIMARY KEY,
    Amount CURRENCY,
    SourceAccountID INTEGER,
    DestinationAccountID INTEGER,
    TransactionDate DATE,
    CONSTRAINT fk_SourceAccount
        FOREIGN KEY (SourceAccountID)
        REFERENCES Account(AccountID),
    CONSTRAINT fk_DestinationAccount
        FOREIGN KEY (DestinationAccountID)
        REFERENCES Account(AccountID)
);"

            command.ExecuteNonQuery()

            connection.Close()

            databaseLoaded = True

        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        LoadDatabase()

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If databaseLoaded Then

            Dim dataSetTableName As String = TextBox1.Text
            Dim sqlQuery As String = TextBox2.Text

            connection.Open()

            Try
                adaptor = New OleDbDataAdapter(sqlQuery, connection)
                myDataSet = New DataSet()

                adaptor.Fill(myDataSet, dataSetTableName)
                ErrorLabel.Text = ""
                DataGridView1.DataSource = myDataSet.Tables(dataSetTableName)

            Catch ex As OleDbException
                ErrorLabel.Text = ex.Message
            End Try



            connection.Close()

        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If databaseLoaded Then

            Dim string1 As String = TextBox3.Text
            Dim string2 As String = TextBox4.Text

            Dim dataSetTableName As String = TextBox1.Text

            connection.Open()

            Dim command As New OleDbCommand("INSERT INTO [tableName] ([field1],[field2]) VALUES ('" & string1 & "','" & string2 & "')", connection)

            command.ExecuteNonQuery()

            connection.Close()

        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        MainMenu.Show()
    End Sub

    Private Sub LoadDatabaseFromDialog()

        Dim myOpenFileDialog As New OpenFileDialog
        myOpenFileDialog.Filter = "MS Access Files (*.accdb)|*.accdb|All Files(*.*)|*.*"

        myOpenFileDialog.FileName = ""
        myOpenFileDialog.ShowDialog()

        If myOpenFileDialog.FileName <> "" Then
            myDatabasePath = myOpenFileDialog.FileName
            connection = New OleDbConnection(dataBaseProvider & "Data Source=" & myDatabasePath)
            connection.Close()
            databaseLoaded = True
        End If

    End Sub

    Private Sub LoadDatabase()
        myDatabasePath = path & "\BankDatabase.accdb"
        connection = New OleDbConnection(dataBaseProvider & "Data Source=" & myDatabasePath)
        connection.Close()
        databaseLoaded = True
    End Sub

End Class
