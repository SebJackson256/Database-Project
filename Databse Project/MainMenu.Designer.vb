<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainMenu))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ViewBalanceButton = New System.Windows.Forms.Button()
        Me.ViewCustomerButton = New System.Windows.Forms.Button()
        Me.ViewTransactionButton = New System.Windows.Forms.Button()
        Me.MakeTransactionButton = New System.Windows.Forms.Button()
        Me.CreateCustomerButton = New System.Windows.Forms.Button()
        Me.CreateAccountButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Yu Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(259, 43)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Online Banking"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Yu Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(20, 55)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(215, 51)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Open Database"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ViewBalanceButton
        '
        Me.ViewBalanceButton.Font = New System.Drawing.Font("Yu Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewBalanceButton.Location = New System.Drawing.Point(20, 112)
        Me.ViewBalanceButton.Name = "ViewBalanceButton"
        Me.ViewBalanceButton.Size = New System.Drawing.Size(215, 51)
        Me.ViewBalanceButton.TabIndex = 2
        Me.ViewBalanceButton.Text = "View Balance"
        Me.ViewBalanceButton.UseVisualStyleBackColor = True
        '
        'ViewCustomerButton
        '
        Me.ViewCustomerButton.Font = New System.Drawing.Font("Yu Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewCustomerButton.Location = New System.Drawing.Point(20, 169)
        Me.ViewCustomerButton.Name = "ViewCustomerButton"
        Me.ViewCustomerButton.Size = New System.Drawing.Size(215, 51)
        Me.ViewCustomerButton.TabIndex = 3
        Me.ViewCustomerButton.Text = "View Customer"
        Me.ViewCustomerButton.UseVisualStyleBackColor = True
        '
        'ViewTransactionButton
        '
        Me.ViewTransactionButton.Font = New System.Drawing.Font("Yu Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewTransactionButton.Location = New System.Drawing.Point(20, 226)
        Me.ViewTransactionButton.Name = "ViewTransactionButton"
        Me.ViewTransactionButton.Size = New System.Drawing.Size(215, 51)
        Me.ViewTransactionButton.TabIndex = 4
        Me.ViewTransactionButton.Text = "View Transactions"
        Me.ViewTransactionButton.UseVisualStyleBackColor = True
        '
        'MakeTransactionButton
        '
        Me.MakeTransactionButton.Font = New System.Drawing.Font("Yu Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MakeTransactionButton.Location = New System.Drawing.Point(241, 226)
        Me.MakeTransactionButton.Name = "MakeTransactionButton"
        Me.MakeTransactionButton.Size = New System.Drawing.Size(215, 51)
        Me.MakeTransactionButton.TabIndex = 5
        Me.MakeTransactionButton.Text = "Make Transaction"
        Me.MakeTransactionButton.UseVisualStyleBackColor = True
        '
        'CreateCustomerButton
        '
        Me.CreateCustomerButton.Font = New System.Drawing.Font("Yu Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CreateCustomerButton.Location = New System.Drawing.Point(241, 112)
        Me.CreateCustomerButton.Name = "CreateCustomerButton"
        Me.CreateCustomerButton.Size = New System.Drawing.Size(215, 51)
        Me.CreateCustomerButton.TabIndex = 6
        Me.CreateCustomerButton.Text = "Create Customer"
        Me.CreateCustomerButton.UseVisualStyleBackColor = True
        '
        'CreateAccountButton
        '
        Me.CreateAccountButton.Font = New System.Drawing.Font("Yu Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CreateAccountButton.Location = New System.Drawing.Point(241, 169)
        Me.CreateAccountButton.Name = "CreateAccountButton"
        Me.CreateAccountButton.Size = New System.Drawing.Size(215, 51)
        Me.CreateAccountButton.TabIndex = 7
        Me.CreateAccountButton.Text = "Create Account"
        Me.CreateAccountButton.UseVisualStyleBackColor = True
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 285)
        Me.Controls.Add(Me.CreateAccountButton)
        Me.Controls.Add(Me.CreateCustomerButton)
        Me.Controls.Add(Me.MakeTransactionButton)
        Me.Controls.Add(Me.ViewTransactionButton)
        Me.Controls.Add(Me.ViewCustomerButton)
        Me.Controls.Add(Me.ViewBalanceButton)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainMenu"
        Me.Text = "MainMenu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents ViewBalanceButton As Button
    Friend WithEvents ViewCustomerButton As Button
    Friend WithEvents ViewTransactionButton As Button
    Friend WithEvents MakeTransactionButton As Button
    Friend WithEvents CreateCustomerButton As Button
    Friend WithEvents CreateAccountButton As Button
End Class
