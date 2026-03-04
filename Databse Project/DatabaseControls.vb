Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Data.OleDb
Imports System.Data.SqlTypes
Imports System.Environment
Imports System.IO
Imports ADOX

''' <summary>
''' Controls for the bank database
''' </summary>
Module DatabaseControls

    Const dataBaseProvider As String = "Provider=Microsoft.ACE.OLEDB.16.0;"
    Dim myDatabasePath As String
    Dim databaseLoaded As Boolean = False

    Dim connection As OleDbConnection
    Dim adaptor As OleDbDataAdapter
    Dim myDataSet As DataSet
    Dim dataSetTableName As String = "A"

    Dim path As String = IO.Directory.GetParent(IO.Directory.GetParent(My.Application.Info.DirectoryPath).ToString).ToString

    ''' <summary>
    ''' Runs when program opens to check whether the bank database already exists and if not creates it.
    ''' </summary>
    Public Sub StartChecks()

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
    Email TEXT,
    HashedPassword TEXT,
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

            MessageBox.Show("Database created.")

        End If



    End Sub

    ''' <summary>
    ''' Runs a select query and displays the output in a DataGridView.
    ''' </summary>
    ''' <param name="sqlQuery">The query to run.</param>
    ''' <param name="dataGrid">The DataGridView to display the data in.</param>
    Public Sub RunSelectQuery(sqlQuery As String, dataGrid As DataGridView)
        If databaseLoaded Then


            connection.Open()

            Try
                adaptor = New OleDbDataAdapter(sqlQuery, connection)
                myDataSet = New DataSet()

                adaptor.Fill(myDataSet, dataSetTableName)

                dataGrid.DataSource = myDataSet.Tables(dataSetTableName)

            Catch ex As OleDbException
                MessageBox.Show(ex.Message)
            End Try

            connection.Close()

        End If

    End Sub

    ''' <summary>
    ''' Runs an SQL query.
    ''' </summary>
    ''' <param name="sqlQuery">The query to run.</param>
    Public Sub RunQuery(sqlQuery As String)

        connection.Open()

        Dim command As New OleDbCommand(sqlQuery, connection)

        command.ExecuteNonQuery()

        connection.Close()
    End Sub

    ''' <summary>
    ''' Runs an insert query based on user specified values.
    ''' </summary>
    ''' <param name="tableName">The table in the database to insert into.</param>
    ''' <param name="fieldNames">A string array containing the names of the fields (in order) to insert into in the query.</param>
    ''' <param name="fieldValues">The values to insert into each of the fields specified in the "fieldNames" array.</param>
    Public Sub Insert(tableName As String, fieldNames As String(), fieldValues As Object())

        If databaseLoaded Then

            Try

                connection.Open()

                Dim fields As String = String.Join(",", fieldNames)
                Dim placeholders As String = String.Join(",", Enumerable.Repeat("?", fieldValues.Length))
                Dim query As String = $"INSERT INTO {tableName} ({fields}) VALUES ({placeholders})"
                Dim command As New OleDbCommand(query, connection)

                For Each value As Object In fieldValues
                    command.Parameters.AddWithValue("?", value)
                Next

                command.ExecuteNonQuery()
                connection.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Gives the user a dialogue box from which to select an access database.
    ''' </summary>
    Public Sub LoadDatabaseFromDialog()

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

    ''' <summary>
    ''' Loads the bank database
    ''' </summary>
    Public Sub LoadDatabase()
        myDatabasePath = path & "\BankDatabase.accdb"
        connection = New OleDbConnection(dataBaseProvider & "Data Source=" & myDatabasePath)
        connection.Close()
        databaseLoaded = True
    End Sub

End Module
