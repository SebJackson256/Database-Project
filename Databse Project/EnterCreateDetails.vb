Public Class EnterCreateDetails

    Dim mode As MainMenu.Selection

    Public Sub SetOpenMode(mode As MainMenu.Selection)
        Me.mode = mode
        Me.Show()
    End Sub

    Private Sub EnterCreateDetails_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Select Case mode
            Case MainMenu.Selection.CreateCustomer

                Label2.Show()
                TextBox2.Show()
                Label3.Show()
                TextBox3.Show()
                Label4.Show()
                TextBox4.Show()
                Label5.Show()
                TextBox5.Show()

                Me.Height = 275
                Button1.Location = New Point(15, 207)

                Label1.Text = "Please enter your first name."
                Label2.Text = "Please enter your surname."
                Label3.Text = "Please enter your date of birth."
                Label4.Text = "Please enter your email address."
                Label5.Text = "Please enter a password."

            Case MainMenu.Selection.CreateAccount

                If LoginPage.loginSuccessful Then

                    Label2.Hide()
                    TextBox2.Hide()
                    Label3.Hide()
                    TextBox3.Hide()
                    Label4.Hide()
                    TextBox4.Hide()
                    Label5.Hide()
                    TextBox5.Hide()

                    Me.Height = 89
                    Button1.Location = New Point(169, 22)

                    Label1.Text = "Please enter a starting balance."

                Else

                    Me.Hide()
                    MessageBox.Show("You must be logged in to create an account.")
                    LoginPage.Show()

                End If

            Case MainMenu.Selection.MakeTransaction

                If LoginPage.loginSuccessful Then

                    Label2.Show()
                    TextBox2.Show()
                    Label3.Show()
                    TextBox3.Show()
                    Label4.Hide()
                    TextBox4.Hide()
                    Label5.Hide()
                    TextBox5.Hide()

                    Me.Height = 168
                    Button1.Location = New Point(178, 103)

                    Label1.Text = "Please enter the source account."
                    Label2.Text = "Please enter the destination account number."
                    Label3.Text = "Please enter the amount."

                Else

                    Me.Hide()
                    MessageBox.Show("You must be logged in to make a transaction.")
                    LoginPage.Show()

                End If

        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim fieldNames As String()
        Dim fieldValues As Object()

        Dim newID As Integer

        Select Case mode
            Case MainMenu.Selection.CreateCustomer

                fieldNames = {"FirstName", "LastName", "Email", "HashedPassword", "DateOfBirth"}
                Try

                    fieldValues = {TextBox1.Text, TextBox2.Text, TextBox4.Text, Utils.GetSHA256Hash(TextBox5.Text), CDate(TextBox3.Text)}
                    newID = DatabaseControls.Insert("Customer", fieldNames, fieldValues, True)

                    MessageBox.Show($"Customer created successfully. Customer ID number is {DatabaseControls.GetValueFromTable($"Select CustomerID From Customer Where Email = '{TextBox4.Text}'", "CustomerID")}")

                    Me.Hide()
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox5.Text = ""

                Catch ex As InvalidCastException
                    MessageBox.Show(ex.Message & vbCr & "Please enter valid data.")
                End Try

            Case MainMenu.Selection.CreateAccount

                Try

                    Dim dateNow As String = Format(DateTime.Now, "dd/MM/yyyy")

                    fieldNames = {"CustomerID", "OpeningDate", "Balance"}
                    fieldValues = {LoginPage.loginCustomerID, dateNow, CDec(TextBox1.Text)}
                    newID = DatabaseControls.Insert("Account", fieldNames, fieldValues, True)

                    MessageBox.Show($"Account created successfully. Account number is {Format(newID, "0000")}")

                    Me.Hide()
                    TextBox1.Text = ""

                Catch ex As InvalidCastException
                    MessageBox.Show(ex.Message & vbCr & "Please enter valid data.")
                End Try

            Case MainMenu.Selection.MakeTransaction

                Try

                    If CStr(LoginPage.loginCustomerID) <> CStr(DatabaseControls.FindValueInTable("Account", "AccountID", TextBox1.Text, "CustomerID")) Then
                        MessageBox.Show("You cannot make a transaction from an account you do not own.")
                        Exit Sub
                    End If

                    fieldNames = {"Amount", "SourceAccountID", "DestinationAccountID", "TransactionDate"}
                    fieldValues = {CStr(TextBox3.Text), CStr(TextBox1.Text), CStr(TextBox2.Text), Format(DateTime.Now, "dd/MM/yyyy")}
                    DatabaseControls.Insert("BankTransaction", fieldNames, fieldValues, False)
                    Dim updateQuery1 As String = "UPDATE Account SET Balance = Balance - " & CDec(TextBox3.Text) & " WHERE AccountID = " & CInt(TextBox1.Text) & ";"
                    Dim updateQuery2 As String = "UPDATE Account SET Balance = Balance + " & CDec(TextBox3.Text) & " WHERE AccountID = " & CInt(TextBox2.Text) & ";"
                    RunQuery(updateQuery1)
                    RunQuery(updateQuery2)

                    MessageBox.Show("Transaction made successfully.")

                    Me.Hide()
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""

                Catch ex As InvalidCastException
                    MessageBox.Show(ex.Message & vbCr & ex.StackTrace & vbCr & "Please enter valid data.")
                End Try

        End Select

    End Sub

End Class