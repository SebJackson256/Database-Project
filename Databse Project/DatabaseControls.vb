Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Data.OleDb
Imports System.Data.SqlTypes
Imports System.Environment
Imports System.IO
Imports ADOX

Module DatabaseControls

    Const dataBaseProvider As String = "Provider=Microsoft.ACE.OLEDB.16.0;"
    Dim myDatabasePath As String
    Dim databaseLoaded As Boolean = False

    Dim connection As OleDbConnection
    Dim adaptor As OleDbDataAdapter
    Dim myDataSet As DataSet
    Dim dataSetTableName As String = "A"

    Dim path As String = IO.Directory.GetParent(IO.Directory.GetParent(My.Application.Info.DirectoryPath).ToString).ToString

    Private Sub StartChecks()

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

    Sub RunSelectQuery(sqlQuery As String)
        If databaseLoaded Then


            connection.Open()

            Try
                adaptor = New OleDbDataAdapter(sqlQuery, connection)
                myDataSet = New DataSet()

                adaptor.Fill(myDataSet, dataSetTableName)

                Form1.DataGridView1.DataSource = myDataSet.Tables(dataSetTableName)

            Catch ex As OleDbException
                MessageBox.Show(ex.Message)
            End Try

            connection.Close()

        End If

    End Sub

    Sub RunQuery(sqlQuery As String)

        connection.Open()

        Dim command As New OleDbCommand(sqlQuery, connection)

        command.ExecuteNonQuery()

        connection.Close()
    End Sub

    Sub Insert(tableName As String, fieldNames As String(), fieldValues As String())

        If databaseLoaded Then

            Dim query As String = "INSERT INTO " & tableName & "("

            For Each fieldName As String In fieldNames
                query &= fieldName & ","
            Next
            query = Left(query, query.Length - 1)
            query &= ") VALUES ('"

            For Each fieldValue As String In fieldValues
                query &= fieldValue & "','"
            Next
            query = Left(query, query.Length - 2)
            query &= ");"

            RunQuery(query)

        End If
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

    Public Sub LoadDatabase()
        myDatabasePath = path & "\BankDatabase.accdb"
        connection = New OleDbConnection(dataBaseProvider & "Data Source=" & myDatabasePath)
        connection.Close()
        databaseLoaded = True
    End Sub


End Module
