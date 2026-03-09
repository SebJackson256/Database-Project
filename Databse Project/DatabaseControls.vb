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
    Const dataSetTableName As String = "A"

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

                connection.Close()

            Catch ex As OleDbException
                MessageBox.Show(ex.Message)
                connection.Close()
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Runs a select query and returns the value of a specified field in the first row of the output.
    ''' </summary>
    ''' <param name="sqlQuery">The query used to retrieve the data.</param>
    ''' <param name="fieldName">The field to return a value from.</param>
    ''' <returns>The value in the first row of the table retrieved through the "sqlQuery" in field "fieldName".</returns>
    Public Function GetValueFromTable(sqlQuery As String, fieldName As String) As Object

        If databaseLoaded Then

            connection.Open()

            Try
                adaptor = New OleDbDataAdapter(sqlQuery, connection)
                myDataSet = New DataSet()

                adaptor.Fill(myDataSet, dataSetTableName)

                connection.Close()

                Dim rows() As DataRow
                rows = myDataSet.Tables(dataSetTableName).Select()
                Return rows(0).Field(Of Object)(fieldName)

            Catch ex As OleDbException
                MessageBox.Show(ex.Message)
                connection.Close()
            End Try

        End If

    End Function

    ''' <summary>
    ''' Finds a value in a specified field in a specified table and returns the value of another specified field in the same row.
    ''' </summary>
    ''' <param name="tableName">The table to search through.</param>
    ''' <param name="fieldName">The field to search through.</param>
    ''' <param name="valueToFind">The value to find in the "fieldName" field.</param>
    ''' <param name="fieldToReturn">The field to return the value of in the row where "valueToFind" is found.</param>
    ''' <returns>The value of "fieldToReturn" in the row where "valueToFind" is found. Returns -1 if "valueToFind" is not found.</returns>
    Public Function FindValueInTable(tableName As String, fieldName As String, valueToFind As Object, fieldToReturn As String) As Object

        If databaseLoaded Then

            connection.Open()

            Dim sqlQuery As String = $"SELECT {fieldName}, {fieldToReturn} FROM {tableName}"


            Try
                adaptor = New OleDbDataAdapter(sqlQuery, connection)
                myDataSet = New DataSet()

                adaptor.Fill(myDataSet, dataSetTableName)

                connection.Close()

                Dim rows() As DataRow
                rows = myDataSet.Tables(dataSetTableName).Select()

                Dim row As DataRow

                For Each row In rows

                    If CStr(row.Field(Of Object)(fieldName)) = CStr(valueToFind) Then
                        Return row.Field(Of Object)(fieldToReturn)
                    End If

                Next

                Return -1

            Catch ex As OleDbException
                MessageBox.Show(ex.Message)
                connection.Close()
            End Try

        End If

    End Function

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
    ''' <returns></returns>
    Public Function Insert(tableName As String, fieldNames As String(), fieldValues As Object(), returnPreviousID As Boolean) As Integer

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

                Dim newID As Integer

                If returnPreviousID Then
                    newID = GetPreviousID()
                    connection.Close()
                    Return newID
                End If

                connection.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                connection.Close()
            End Try

        End If

        Return -1

    End Function


    Public Function GetPreviousID() As Integer


        Dim identityCommand As New OleDbCommand("SELECT @@IDENTITY", connection)
        Dim newID As Integer = Convert.ToInt32(identityCommand.ExecuteScalar())


        Return newID

    End Function

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
