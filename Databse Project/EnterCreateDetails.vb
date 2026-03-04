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
                Label4.Show()
                TextBox4.Show()
                Label5.Show()
                TextBox5.Show()
                Label1.Text = "Please enter your first name."
                Label2.Text = "Please enter your surname."
                Label3.Text = "Please enter your date of birth."
            Case MainMenu.Selection.CreateAccount
                Label1.Text = "Please enter a customer ID."
                Label2.Text = "Please enter a starting balance."
                Label3.Hide()
                TextBox3.Hide()
                Label4.Hide()
                TextBox4.Hide()
                Label5.Hide()
                TextBox5.Hide()
            Case MainMenu.Selection.MakeTransaction
                Label3.Show()
                TextBox3.Show()
                Label1.Text = "Please enter the source account number."
                Label2.Text = "Please enter the destination account number."
                Label3.Text = "Please enter the amount."
                Label4.Hide()
                TextBox4.Hide()
                Label5.Hide()
                TextBox5.Hide()
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim fieldNames As String()
        Dim fieldValues As Object()

        Select Case mode
            Case MainMenu.Selection.CreateCustomer

                fieldNames = {"FirstName", "LastName", "Email", "HashedPassword", "DateOfBirth"}
                Try

                    fieldValues = {TextBox1.Text, TextBox2.Text, TextBox4.Text, Utils.GetSHA256Hash(TextBox5.Text), CDate(TextBox3.Text)}
                    DatabaseControls.Insert("Customer", fieldNames, fieldValues)

                    MessageBox.Show("Customer created successfully.")
                    Me.Hide()

                Catch ex As InvalidCastException
                    MessageBox.Show(ex.Message & vbCr & "Please enter valid data.")
                End Try

            Case MainMenu.Selection.CreateAccount

                Try

                    fieldNames = {"CustomerID", "OpeningDate", "Balance"}
                    fieldValues = {CInt(TextBox1.Text), Format(DateTime.Now, "dd/MM/yyyy"), CDec(TextBox2.Text)}
                    DatabaseControls.Insert("Account", fieldNames, fieldValues)

                    MessageBox.Show("Account created successfully.")
                    Me.Hide()

                Catch ex As InvalidCastException
                    MessageBox.Show(ex.Message & vbCr & "Please enter valid data.")
                End Try

            Case MainMenu.Selection.MakeTransaction

                Try

                    fieldNames = {"Amount", "SourceAccountID", "DestinationAccountID", "TransactionDate"}
                    fieldValues = {TextBox3.Text, TextBox1.Text, TextBox2.Text, Format(DateTime.Now, "dd/MM/yyyy")}
                    DatabaseControls.Insert("BankTransaction", fieldNames, fieldValues)
                    Dim updateQuery1 As String = "UPDATE Account SET Balance = Balance - " & CDec(TextBox3.Text) & " WHERE AccountID = " & CInt(TextBox1.Text) & ";"
                    Dim updateQuery2 As String = "UPDATE Account SET Balance = Balance + " & CDec(TextBox3.Text) & " WHERE AccountID = " & CInt(TextBox2.Text) & ";"
                    RunQuery(updateQuery1)
                    RunQuery(updateQuery2)

                    MessageBox.Show("Transaction made successfully.")
                    Me.Hide()

                Catch ex As InvalidCastException
                    MessageBox.Show(ex.Message & vbCr & "Please enter valid data.")
                End Try

        End Select

    End Sub

End Class