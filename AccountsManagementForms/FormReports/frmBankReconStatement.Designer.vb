<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBankReconStatement
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
        Me.tc_MainWindow = New System.Windows.Forms.TabControl()
        Me.tp_STL = New System.Windows.Forms.TabPage()
        Me.txt_stlBankAdjusted = New System.Windows.Forms.TextBox()
        Me.txt_stlBookAdjusted = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txt_stlBookCharges = New System.Windows.Forms.TextBox()
        Me.txt_stlBankCheck = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txt_stlBookNSSPay = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txt_stlBookPRRep = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txt_stlBankEndBalance = New System.Windows.Forms.TextBox()
        Me.txt_stlBookEndBalance = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_stlBookInterest = New System.Windows.Forms.TextBox()
        Me.txt_stlBookMF = New System.Windows.Forms.TextBox()
        Me.txt_stlBookPR = New System.Windows.Forms.TextBox()
        Me.txt_stlBookNSS = New System.Windows.Forms.TextBox()
        Me.txt_stlBookEFT = New System.Windows.Forms.TextBox()
        Me.txt_stlBookCheck = New System.Windows.Forms.TextBox()
        Me.txt_stlBookCol = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_stlBegBalance = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tp_PR = New System.Windows.Forms.TabPage()
        Me.txt_PRBankAdjusted = New System.Windows.Forms.TextBox()
        Me.txt_PRBookAdjusted = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txt_PRBookCharges = New System.Windows.Forms.TextBox()
        Me.txt_PRBankCheck = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txt_PRBookDDown = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txt_PRBookPay = New System.Windows.Forms.TextBox()
        Me.txt_PRBankEndBalance = New System.Windows.Forms.TextBox()
        Me.txt_PRBookEndBalance = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_PRBookInterest = New System.Windows.Forms.TextBox()
        Me.txt_PRBookPrudential = New System.Windows.Forms.TextBox()
        Me.txt_PRBookCol = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txt_PRBegBalance = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.tp_NSS = New System.Windows.Forms.TabPage()
        Me.txt_NSSBookPay = New System.Windows.Forms.TextBox()
        Me.txt_NSSBankEndBal = New System.Windows.Forms.TextBox()
        Me.txt_NSSBookEndBal = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txt_NSSBookInterest = New System.Windows.Forms.TextBox()
        Me.txt_NSSBookNSS = New System.Windows.Forms.TextBox()
        Me.txt_NSSBookCol = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txt_NSSBegBalance = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.cmd_Generate = New System.Windows.Forms.Button()
        Me.cmd_Close = New System.Windows.Forms.Button()
        Me.cmd_export = New System.Windows.Forms.Button()
        Me.nud_Year = New System.Windows.Forms.NumericUpDown()
        Me.cbo_MonthSelected = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmd_Save = New System.Windows.Forms.Button()
        Me.chk_Search = New System.Windows.Forms.CheckBox()
        Me.tc_MainWindow.SuspendLayout()
        Me.tp_STL.SuspendLayout()
        Me.tp_PR.SuspendLayout()
        Me.tp_NSS.SuspendLayout()
        CType(Me.nud_Year, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tc_MainWindow
        '
        Me.tc_MainWindow.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tc_MainWindow.Controls.Add(Me.tp_STL)
        Me.tc_MainWindow.Controls.Add(Me.tp_PR)
        Me.tc_MainWindow.Controls.Add(Me.tp_NSS)
        Me.tc_MainWindow.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.tc_MainWindow.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tc_MainWindow.Location = New System.Drawing.Point(12, 94)
        Me.tc_MainWindow.Name = "tc_MainWindow"
        Me.tc_MainWindow.SelectedIndex = 0
        Me.tc_MainWindow.Size = New System.Drawing.Size(665, 547)
        Me.tc_MainWindow.TabIndex = 0
        '
        'tp_STL
        '
        Me.tp_STL.Controls.Add(Me.txt_stlBankAdjusted)
        Me.tp_STL.Controls.Add(Me.txt_stlBookAdjusted)
        Me.tp_STL.Controls.Add(Me.Label32)
        Me.tp_STL.Controls.Add(Me.txt_stlBookCharges)
        Me.tp_STL.Controls.Add(Me.txt_stlBankCheck)
        Me.tp_STL.Controls.Add(Me.Label30)
        Me.tp_STL.Controls.Add(Me.Label29)
        Me.tp_STL.Controls.Add(Me.txt_stlBookNSSPay)
        Me.tp_STL.Controls.Add(Me.Label21)
        Me.tp_STL.Controls.Add(Me.txt_stlBookPRRep)
        Me.tp_STL.Controls.Add(Me.Label20)
        Me.tp_STL.Controls.Add(Me.txt_stlBankEndBalance)
        Me.tp_STL.Controls.Add(Me.txt_stlBookEndBalance)
        Me.tp_STL.Controls.Add(Me.Label12)
        Me.tp_STL.Controls.Add(Me.Label11)
        Me.tp_STL.Controls.Add(Me.Label10)
        Me.tp_STL.Controls.Add(Me.txt_stlBookInterest)
        Me.tp_STL.Controls.Add(Me.txt_stlBookMF)
        Me.tp_STL.Controls.Add(Me.txt_stlBookPR)
        Me.tp_STL.Controls.Add(Me.txt_stlBookNSS)
        Me.tp_STL.Controls.Add(Me.txt_stlBookEFT)
        Me.tp_STL.Controls.Add(Me.txt_stlBookCheck)
        Me.tp_STL.Controls.Add(Me.txt_stlBookCol)
        Me.tp_STL.Controls.Add(Me.Label9)
        Me.tp_STL.Controls.Add(Me.Label8)
        Me.tp_STL.Controls.Add(Me.Label7)
        Me.tp_STL.Controls.Add(Me.Label6)
        Me.tp_STL.Controls.Add(Me.Label5)
        Me.tp_STL.Controls.Add(Me.Label4)
        Me.tp_STL.Controls.Add(Me.Label3)
        Me.tp_STL.Controls.Add(Me.Label2)
        Me.tp_STL.Controls.Add(Me.txt_stlBegBalance)
        Me.tp_STL.Controls.Add(Me.Label1)
        Me.tp_STL.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tp_STL.Location = New System.Drawing.Point(4, 23)
        Me.tp_STL.Name = "tp_STL"
        Me.tp_STL.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_STL.Size = New System.Drawing.Size(657, 520)
        Me.tp_STL.TabIndex = 0
        Me.tp_STL.Text = "Settlement"
        Me.tp_STL.UseVisualStyleBackColor = True
        '
        'txt_stlBankAdjusted
        '
        Me.txt_stlBankAdjusted.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBankAdjusted.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBankAdjusted.Location = New System.Drawing.Point(462, 484)
        Me.txt_stlBankAdjusted.MaxLength = 20
        Me.txt_stlBankAdjusted.Name = "txt_stlBankAdjusted"
        Me.txt_stlBankAdjusted.ReadOnly = True
        Me.txt_stlBankAdjusted.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBankAdjusted.TabIndex = 39
        Me.txt_stlBankAdjusted.Text = "0.00"
        Me.txt_stlBankAdjusted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBookAdjusted
        '
        Me.txt_stlBookAdjusted.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookAdjusted.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookAdjusted.Location = New System.Drawing.Point(190, 484)
        Me.txt_stlBookAdjusted.MaxLength = 20
        Me.txt_stlBookAdjusted.Name = "txt_stlBookAdjusted"
        Me.txt_stlBookAdjusted.ReadOnly = True
        Me.txt_stlBookAdjusted.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookAdjusted.TabIndex = 37
        Me.txt_stlBookAdjusted.Text = "0.00"
        Me.txt_stlBookAdjusted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(6, 486)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(104, 14)
        Me.Label32.TabIndex = 36
        Me.Label32.Text = "Adjusted Balance:"
        '
        'txt_stlBookCharges
        '
        Me.txt_stlBookCharges.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookCharges.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookCharges.Location = New System.Drawing.Point(190, 430)
        Me.txt_stlBookCharges.MaxLength = 20
        Me.txt_stlBookCharges.Name = "txt_stlBookCharges"
        Me.txt_stlBookCharges.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookCharges.TabIndex = 35
        Me.txt_stlBookCharges.Text = "0.00"
        Me.txt_stlBookCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBankCheck
        '
        Me.txt_stlBankCheck.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBankCheck.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBankCheck.Location = New System.Drawing.Point(462, 403)
        Me.txt_stlBankCheck.MaxLength = 20
        Me.txt_stlBankCheck.Name = "txt_stlBankCheck"
        Me.txt_stlBankCheck.ReadOnly = True
        Me.txt_stlBankCheck.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBankCheck.TabIndex = 34
        Me.txt_stlBankCheck.Text = "0.00"
        Me.txt_stlBankCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(6, 432)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(85, 14)
        Me.Label30.TabIndex = 33
        Me.Label30.Text = "Bank Charges:"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(6, 405)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(120, 14)
        Me.Label29.TabIndex = 32
        Me.Label29.Text = "Outstanding Checks:"
        '
        'txt_stlBookNSSPay
        '
        Me.txt_stlBookNSSPay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookNSSPay.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookNSSPay.Location = New System.Drawing.Point(190, 310)
        Me.txt_stlBookNSSPay.MaxLength = 20
        Me.txt_stlBookNSSPay.Name = "txt_stlBookNSSPay"
        Me.txt_stlBookNSSPay.ReadOnly = True
        Me.txt_stlBookNSSPay.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookNSSPay.TabIndex = 31
        Me.txt_stlBookNSSPay.Text = "0.00"
        Me.txt_stlBookNSSPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(29, 312)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 14)
        Me.Label21.TabIndex = 30
        Me.Label21.Text = "FTF - NSS:"
        '
        'txt_stlBookPRRep
        '
        Me.txt_stlBookPRRep.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookPRRep.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookPRRep.Location = New System.Drawing.Point(190, 283)
        Me.txt_stlBookPRRep.MaxLength = 20
        Me.txt_stlBookPRRep.Name = "txt_stlBookPRRep"
        Me.txt_stlBookPRRep.ReadOnly = True
        Me.txt_stlBookPRRep.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookPRRep.TabIndex = 29
        Me.txt_stlBookPRRep.Text = "0.00"
        Me.txt_stlBookPRRep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(29, 285)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(96, 14)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "FTF - Prudential:"
        '
        'txt_stlBankEndBalance
        '
        Me.txt_stlBankEndBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBankEndBalance.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBankEndBalance.Location = New System.Drawing.Point(462, 352)
        Me.txt_stlBankEndBalance.MaxLength = 20
        Me.txt_stlBankEndBalance.Name = "txt_stlBankEndBalance"
        Me.txt_stlBankEndBalance.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBankEndBalance.TabIndex = 27
        Me.txt_stlBankEndBalance.Text = "0.00"
        Me.txt_stlBankEndBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBookEndBalance
        '
        Me.txt_stlBookEndBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookEndBalance.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookEndBalance.Location = New System.Drawing.Point(190, 352)
        Me.txt_stlBookEndBalance.MaxLength = 20
        Me.txt_stlBookEndBalance.Name = "txt_stlBookEndBalance"
        Me.txt_stlBookEndBalance.ReadOnly = True
        Me.txt_stlBookEndBalance.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookEndBalance.TabIndex = 26
        Me.txt_stlBookEndBalance.Text = "0.00"
        Me.txt_stlBookEndBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(6, 354)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(94, 14)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "Ending Balance:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.Label11.Location = New System.Drawing.Point(527, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 15)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "Bank Balance"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.Label10.Location = New System.Drawing.Point(225, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 15)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Book Balance"
        '
        'txt_stlBookInterest
        '
        Me.txt_stlBookInterest.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookInterest.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookInterest.Location = New System.Drawing.Point(190, 159)
        Me.txt_stlBookInterest.MaxLength = 20
        Me.txt_stlBookInterest.Name = "txt_stlBookInterest"
        Me.txt_stlBookInterest.ReadOnly = True
        Me.txt_stlBookInterest.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookInterest.TabIndex = 16
        Me.txt_stlBookInterest.Text = "0.00"
        Me.txt_stlBookInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBookMF
        '
        Me.txt_stlBookMF.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookMF.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookMF.Location = New System.Drawing.Point(190, 256)
        Me.txt_stlBookMF.MaxLength = 20
        Me.txt_stlBookMF.Name = "txt_stlBookMF"
        Me.txt_stlBookMF.ReadOnly = True
        Me.txt_stlBookMF.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookMF.TabIndex = 15
        Me.txt_stlBookMF.Text = "0.00"
        Me.txt_stlBookMF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBookPR
        '
        Me.txt_stlBookPR.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookPR.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookPR.Location = New System.Drawing.Point(190, 105)
        Me.txt_stlBookPR.MaxLength = 20
        Me.txt_stlBookPR.Name = "txt_stlBookPR"
        Me.txt_stlBookPR.ReadOnly = True
        Me.txt_stlBookPR.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookPR.TabIndex = 14
        Me.txt_stlBookPR.Text = "0.00"
        Me.txt_stlBookPR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBookNSS
        '
        Me.txt_stlBookNSS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookNSS.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookNSS.Location = New System.Drawing.Point(190, 132)
        Me.txt_stlBookNSS.MaxLength = 20
        Me.txt_stlBookNSS.Name = "txt_stlBookNSS"
        Me.txt_stlBookNSS.ReadOnly = True
        Me.txt_stlBookNSS.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookNSS.TabIndex = 13
        Me.txt_stlBookNSS.Text = "0.00"
        Me.txt_stlBookNSS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBookEFT
        '
        Me.txt_stlBookEFT.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookEFT.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookEFT.Location = New System.Drawing.Point(190, 229)
        Me.txt_stlBookEFT.MaxLength = 20
        Me.txt_stlBookEFT.Name = "txt_stlBookEFT"
        Me.txt_stlBookEFT.ReadOnly = True
        Me.txt_stlBookEFT.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookEFT.TabIndex = 12
        Me.txt_stlBookEFT.Text = "0.00"
        Me.txt_stlBookEFT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBookCheck
        '
        Me.txt_stlBookCheck.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookCheck.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookCheck.Location = New System.Drawing.Point(190, 203)
        Me.txt_stlBookCheck.MaxLength = 20
        Me.txt_stlBookCheck.Name = "txt_stlBookCheck"
        Me.txt_stlBookCheck.ReadOnly = True
        Me.txt_stlBookCheck.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookCheck.TabIndex = 11
        Me.txt_stlBookCheck.Text = "0.00"
        Me.txt_stlBookCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_stlBookCol
        '
        Me.txt_stlBookCol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBookCol.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBookCol.Location = New System.Drawing.Point(190, 78)
        Me.txt_stlBookCol.MaxLength = 20
        Me.txt_stlBookCol.Name = "txt_stlBookCol"
        Me.txt_stlBookCol.ReadOnly = True
        Me.txt_stlBookCol.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBookCol.TabIndex = 10
        Me.txt_stlBookCol.Text = "0.00"
        Me.txt_stlBookCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(6, 162)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 14)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Interest Earned:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(29, 258)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 14)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "FTF - Market Fees:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(6, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 14)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "PR Drawdown:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 135)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 14)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "FTF - NSS:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(29, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 14)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "EFT:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(29, 206)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "LBC:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(6, 184)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Payment:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Collection:"
        '
        'txt_stlBegBalance
        '
        Me.txt_stlBegBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_stlBegBalance.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_stlBegBalance.Location = New System.Drawing.Point(190, 6)
        Me.txt_stlBegBalance.MaxLength = 20
        Me.txt_stlBegBalance.Name = "txt_stlBegBalance"
        Me.txt_stlBegBalance.Size = New System.Drawing.Size(189, 20)
        Me.txt_stlBegBalance.TabIndex = 1
        Me.txt_stlBegBalance.Text = "0.00"
        Me.txt_stlBegBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Beginning Balance:"
        '
        'tp_PR
        '
        Me.tp_PR.Controls.Add(Me.txt_PRBankAdjusted)
        Me.tp_PR.Controls.Add(Me.txt_PRBookAdjusted)
        Me.tp_PR.Controls.Add(Me.Label40)
        Me.tp_PR.Controls.Add(Me.txt_PRBookCharges)
        Me.tp_PR.Controls.Add(Me.txt_PRBankCheck)
        Me.tp_PR.Controls.Add(Me.Label38)
        Me.tp_PR.Controls.Add(Me.Label39)
        Me.tp_PR.Controls.Add(Me.txt_PRBookDDown)
        Me.tp_PR.Controls.Add(Me.Label37)
        Me.tp_PR.Controls.Add(Me.Label33)
        Me.tp_PR.Controls.Add(Me.txt_PRBookPay)
        Me.tp_PR.Controls.Add(Me.txt_PRBankEndBalance)
        Me.tp_PR.Controls.Add(Me.txt_PRBookEndBalance)
        Me.tp_PR.Controls.Add(Me.Label13)
        Me.tp_PR.Controls.Add(Me.Label14)
        Me.tp_PR.Controls.Add(Me.Label15)
        Me.tp_PR.Controls.Add(Me.txt_PRBookInterest)
        Me.tp_PR.Controls.Add(Me.txt_PRBookPrudential)
        Me.tp_PR.Controls.Add(Me.txt_PRBookCol)
        Me.tp_PR.Controls.Add(Me.Label16)
        Me.tp_PR.Controls.Add(Me.Label18)
        Me.tp_PR.Controls.Add(Me.Label22)
        Me.tp_PR.Controls.Add(Me.Label23)
        Me.tp_PR.Controls.Add(Me.txt_PRBegBalance)
        Me.tp_PR.Controls.Add(Me.Label24)
        Me.tp_PR.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tp_PR.Location = New System.Drawing.Point(4, 23)
        Me.tp_PR.Name = "tp_PR"
        Me.tp_PR.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_PR.Size = New System.Drawing.Size(657, 520)
        Me.tp_PR.TabIndex = 1
        Me.tp_PR.Text = "Prudential Requirements"
        Me.tp_PR.UseVisualStyleBackColor = True
        '
        'txt_PRBankAdjusted
        '
        Me.txt_PRBankAdjusted.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBankAdjusted.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBankAdjusted.Location = New System.Drawing.Point(462, 394)
        Me.txt_PRBankAdjusted.MaxLength = 20
        Me.txt_PRBankAdjusted.Name = "txt_PRBankAdjusted"
        Me.txt_PRBankAdjusted.ReadOnly = True
        Me.txt_PRBankAdjusted.Size = New System.Drawing.Size(189, 22)
        Me.txt_PRBankAdjusted.TabIndex = 67
        Me.txt_PRBankAdjusted.Text = "0.00"
        Me.txt_PRBankAdjusted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_PRBookAdjusted
        '
        Me.txt_PRBookAdjusted.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBookAdjusted.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBookAdjusted.Location = New System.Drawing.Point(190, 394)
        Me.txt_PRBookAdjusted.MaxLength = 20
        Me.txt_PRBookAdjusted.Name = "txt_PRBookAdjusted"
        Me.txt_PRBookAdjusted.ReadOnly = True
        Me.txt_PRBookAdjusted.Size = New System.Drawing.Size(189, 22)
        Me.txt_PRBookAdjusted.TabIndex = 66
        Me.txt_PRBookAdjusted.Text = "0.00"
        Me.txt_PRBookAdjusted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(6, 396)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(108, 15)
        Me.Label40.TabIndex = 65
        Me.Label40.Text = "Adjusted Balance"
        '
        'txt_PRBookCharges
        '
        Me.txt_PRBookCharges.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBookCharges.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBookCharges.Location = New System.Drawing.Point(190, 340)
        Me.txt_PRBookCharges.MaxLength = 20
        Me.txt_PRBookCharges.Name = "txt_PRBookCharges"
        Me.txt_PRBookCharges.Size = New System.Drawing.Size(189, 21)
        Me.txt_PRBookCharges.TabIndex = 64
        Me.txt_PRBookCharges.Text = "0.00"
        Me.txt_PRBookCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_PRBankCheck
        '
        Me.txt_PRBankCheck.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBankCheck.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBankCheck.Location = New System.Drawing.Point(462, 313)
        Me.txt_PRBankCheck.MaxLength = 20
        Me.txt_PRBankCheck.Name = "txt_PRBankCheck"
        Me.txt_PRBankCheck.ReadOnly = True
        Me.txt_PRBankCheck.Size = New System.Drawing.Size(189, 21)
        Me.txt_PRBankCheck.TabIndex = 63
        Me.txt_PRBankCheck.Text = "0.00"
        Me.txt_PRBankCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(6, 342)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(85, 14)
        Me.Label38.TabIndex = 62
        Me.Label38.Text = "Bank Charges"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(6, 315)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(117, 14)
        Me.Label39.TabIndex = 61
        Me.Label39.Text = "Outstanding Checks"
        '
        'txt_PRBookDDown
        '
        Me.txt_PRBookDDown.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBookDDown.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBookDDown.Location = New System.Drawing.Point(190, 202)
        Me.txt_PRBookDDown.MaxLength = 20
        Me.txt_PRBookDDown.Name = "txt_PRBookDDown"
        Me.txt_PRBookDDown.ReadOnly = True
        Me.txt_PRBookDDown.Size = New System.Drawing.Size(189, 21)
        Me.txt_PRBookDDown.TabIndex = 60
        Me.txt_PRBookDDown.Text = "0.00"
        Me.txt_PRBookDDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(6, 204)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(67, 15)
        Me.Label37.TabIndex = 59
        Me.Label37.Text = "Drawdown"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(6, 69)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(153, 15)
        Me.Label33.TabIndex = 58
        Me.Label33.Text = "Replenishment/Collection"
        '
        'txt_PRBookPay
        '
        Me.txt_PRBookPay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBookPay.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBookPay.Location = New System.Drawing.Point(190, 120)
        Me.txt_PRBookPay.MaxLength = 20
        Me.txt_PRBookPay.Name = "txt_PRBookPay"
        Me.txt_PRBookPay.ReadOnly = True
        Me.txt_PRBookPay.Size = New System.Drawing.Size(189, 21)
        Me.txt_PRBookPay.TabIndex = 57
        Me.txt_PRBookPay.Text = "0.00"
        Me.txt_PRBookPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_PRBankEndBalance
        '
        Me.txt_PRBankEndBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBankEndBalance.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBankEndBalance.Location = New System.Drawing.Point(462, 256)
        Me.txt_PRBankEndBalance.MaxLength = 20
        Me.txt_PRBankEndBalance.Name = "txt_PRBankEndBalance"
        Me.txt_PRBankEndBalance.Size = New System.Drawing.Size(189, 22)
        Me.txt_PRBankEndBalance.TabIndex = 56
        Me.txt_PRBankEndBalance.Text = "0.00"
        Me.txt_PRBankEndBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_PRBookEndBalance
        '
        Me.txt_PRBookEndBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBookEndBalance.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBookEndBalance.Location = New System.Drawing.Point(190, 256)
        Me.txt_PRBookEndBalance.MaxLength = 20
        Me.txt_PRBookEndBalance.Name = "txt_PRBookEndBalance"
        Me.txt_PRBookEndBalance.ReadOnly = True
        Me.txt_PRBookEndBalance.Size = New System.Drawing.Size(189, 22)
        Me.txt_PRBookEndBalance.TabIndex = 55
        Me.txt_PRBookEndBalance.Text = "0.00"
        Me.txt_PRBookEndBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(6, 258)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 15)
        Me.Label13.TabIndex = 54
        Me.Label13.Text = "Ending Balance"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.Label14.Location = New System.Drawing.Point(527, 49)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(87, 15)
        Me.Label14.TabIndex = 53
        Me.Label14.Text = "Bank Balance"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.Label15.Location = New System.Drawing.Point(225, 49)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 15)
        Me.Label15.TabIndex = 52
        Me.Label15.Text = "Book Balance"
        '
        'txt_PRBookInterest
        '
        Me.txt_PRBookInterest.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBookInterest.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBookInterest.Location = New System.Drawing.Point(190, 175)
        Me.txt_PRBookInterest.MaxLength = 20
        Me.txt_PRBookInterest.Name = "txt_PRBookInterest"
        Me.txt_PRBookInterest.ReadOnly = True
        Me.txt_PRBookInterest.Size = New System.Drawing.Size(189, 21)
        Me.txt_PRBookInterest.TabIndex = 44
        Me.txt_PRBookInterest.Text = "0.00"
        Me.txt_PRBookInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_PRBookPrudential
        '
        Me.txt_PRBookPrudential.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBookPrudential.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBookPrudential.Location = New System.Drawing.Point(190, 148)
        Me.txt_PRBookPrudential.MaxLength = 20
        Me.txt_PRBookPrudential.Name = "txt_PRBookPrudential"
        Me.txt_PRBookPrudential.ReadOnly = True
        Me.txt_PRBookPrudential.Size = New System.Drawing.Size(189, 21)
        Me.txt_PRBookPrudential.TabIndex = 42
        Me.txt_PRBookPrudential.Text = "0.00"
        Me.txt_PRBookPrudential.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_PRBookCol
        '
        Me.txt_PRBookCol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBookCol.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBookCol.Location = New System.Drawing.Point(190, 94)
        Me.txt_PRBookCol.MaxLength = 20
        Me.txt_PRBookCol.Name = "txt_PRBookCol"
        Me.txt_PRBookCol.ReadOnly = True
        Me.txt_PRBookCol.Size = New System.Drawing.Size(189, 21)
        Me.txt_PRBookCol.TabIndex = 38
        Me.txt_PRBookCol.Text = "0.00"
        Me.txt_PRBookCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(21, 177)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(90, 14)
        Me.Label16.TabIndex = 37
        Me.Label16.Text = "Interest Earned"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(21, 150)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(82, 14)
        Me.Label18.TabIndex = 35
        Me.Label18.Text = "Direct Update"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(21, 122)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(130, 14)
        Me.Label22.TabIndex = 31
        Me.Label22.Text = "Transfer from Payment"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(21, 96)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(114, 14)
        Me.Label23.TabIndex = 30
        Me.Label23.Text = "Collection from STL"
        '
        'txt_PRBegBalance
        '
        Me.txt_PRBegBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_PRBegBalance.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRBegBalance.Location = New System.Drawing.Point(190, 6)
        Me.txt_PRBegBalance.MaxLength = 20
        Me.txt_PRBegBalance.Name = "txt_PRBegBalance"
        Me.txt_PRBegBalance.Size = New System.Drawing.Size(189, 21)
        Me.txt_PRBegBalance.TabIndex = 29
        Me.txt_PRBegBalance.Text = "0.00"
        Me.txt_PRBegBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(6, 8)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(118, 15)
        Me.Label24.TabIndex = 28
        Me.Label24.Text = "Beginning Balance:"
        '
        'tp_NSS
        '
        Me.tp_NSS.Controls.Add(Me.txt_NSSBookPay)
        Me.tp_NSS.Controls.Add(Me.txt_NSSBankEndBal)
        Me.tp_NSS.Controls.Add(Me.txt_NSSBookEndBal)
        Me.tp_NSS.Controls.Add(Me.Label25)
        Me.tp_NSS.Controls.Add(Me.Label26)
        Me.tp_NSS.Controls.Add(Me.Label27)
        Me.tp_NSS.Controls.Add(Me.txt_NSSBookInterest)
        Me.tp_NSS.Controls.Add(Me.txt_NSSBookNSS)
        Me.tp_NSS.Controls.Add(Me.txt_NSSBookCol)
        Me.tp_NSS.Controls.Add(Me.Label28)
        Me.tp_NSS.Controls.Add(Me.Label31)
        Me.tp_NSS.Controls.Add(Me.Label34)
        Me.tp_NSS.Controls.Add(Me.Label35)
        Me.tp_NSS.Controls.Add(Me.txt_NSSBegBalance)
        Me.tp_NSS.Controls.Add(Me.Label36)
        Me.tp_NSS.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tp_NSS.Location = New System.Drawing.Point(4, 23)
        Me.tp_NSS.Name = "tp_NSS"
        Me.tp_NSS.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_NSS.Size = New System.Drawing.Size(657, 520)
        Me.tp_NSS.TabIndex = 2
        Me.tp_NSS.Text = "NSS Transactions"
        Me.tp_NSS.UseVisualStyleBackColor = True
        '
        'txt_NSSBookPay
        '
        Me.txt_NSSBookPay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSBookPay.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSBookPay.Location = New System.Drawing.Point(190, 149)
        Me.txt_NSSBookPay.MaxLength = 20
        Me.txt_NSSBookPay.Name = "txt_NSSBookPay"
        Me.txt_NSSBookPay.ReadOnly = True
        Me.txt_NSSBookPay.Size = New System.Drawing.Size(189, 21)
        Me.txt_NSSBookPay.TabIndex = 57
        Me.txt_NSSBookPay.Text = "0.00"
        Me.txt_NSSBookPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NSSBankEndBal
        '
        Me.txt_NSSBankEndBal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSBankEndBal.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSBankEndBal.Location = New System.Drawing.Point(462, 216)
        Me.txt_NSSBankEndBal.MaxLength = 20
        Me.txt_NSSBankEndBal.Name = "txt_NSSBankEndBal"
        Me.txt_NSSBankEndBal.Size = New System.Drawing.Size(189, 22)
        Me.txt_NSSBankEndBal.TabIndex = 56
        Me.txt_NSSBankEndBal.Text = "0.00"
        Me.txt_NSSBankEndBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NSSBookEndBal
        '
        Me.txt_NSSBookEndBal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSBookEndBal.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSBookEndBal.Location = New System.Drawing.Point(190, 216)
        Me.txt_NSSBookEndBal.MaxLength = 20
        Me.txt_NSSBookEndBal.Name = "txt_NSSBookEndBal"
        Me.txt_NSSBookEndBal.ReadOnly = True
        Me.txt_NSSBookEndBal.Size = New System.Drawing.Size(189, 22)
        Me.txt_NSSBookEndBal.TabIndex = 55
        Me.txt_NSSBookEndBal.Text = "0.00"
        Me.txt_NSSBookEndBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(6, 218)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(96, 15)
        Me.Label25.TabIndex = 54
        Me.Label25.Text = "Ending Balance"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.Label26.Location = New System.Drawing.Point(527, 49)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(87, 15)
        Me.Label26.TabIndex = 53
        Me.Label26.Text = "Bank Balance"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.Label27.Location = New System.Drawing.Point(225, 49)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(87, 15)
        Me.Label27.TabIndex = 52
        Me.Label27.Text = "Book Balance"
        '
        'txt_NSSBookInterest
        '
        Me.txt_NSSBookInterest.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSBookInterest.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSBookInterest.Location = New System.Drawing.Point(190, 105)
        Me.txt_NSSBookInterest.MaxLength = 20
        Me.txt_NSSBookInterest.Name = "txt_NSSBookInterest"
        Me.txt_NSSBookInterest.ReadOnly = True
        Me.txt_NSSBookInterest.Size = New System.Drawing.Size(189, 21)
        Me.txt_NSSBookInterest.TabIndex = 44
        Me.txt_NSSBookInterest.Text = "0.00"
        Me.txt_NSSBookInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NSSBookNSS
        '
        Me.txt_NSSBookNSS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSBookNSS.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSBookNSS.Location = New System.Drawing.Point(190, 176)
        Me.txt_NSSBookNSS.MaxLength = 20
        Me.txt_NSSBookNSS.Name = "txt_NSSBookNSS"
        Me.txt_NSSBookNSS.ReadOnly = True
        Me.txt_NSSBookNSS.Size = New System.Drawing.Size(189, 21)
        Me.txt_NSSBookNSS.TabIndex = 41
        Me.txt_NSSBookNSS.Text = "0.00"
        Me.txt_NSSBookNSS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NSSBookCol
        '
        Me.txt_NSSBookCol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSBookCol.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSBookCol.Location = New System.Drawing.Point(190, 78)
        Me.txt_NSSBookCol.MaxLength = 20
        Me.txt_NSSBookCol.Name = "txt_NSSBookCol"
        Me.txt_NSSBookCol.ReadOnly = True
        Me.txt_NSSBookCol.Size = New System.Drawing.Size(189, 21)
        Me.txt_NSSBookCol.TabIndex = 38
        Me.txt_NSSBookCol.Text = "0.00"
        Me.txt_NSSBookCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(6, 108)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(111, 15)
        Me.Label28.TabIndex = 37
        Me.Label28.Text = "Uploaded Interest "
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(6, 178)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(86, 15)
        Me.Label31.TabIndex = 34
        Me.Label31.Text = "Interest to MP"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(6, 80)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(123, 15)
        Me.Label34.TabIndex = 31
        Me.Label34.Text = "FTF from Settlement"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(6, 151)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(110, 15)
        Me.Label35.TabIndex = 30
        Me.Label35.Text = "FTF To Settlement"
        '
        'txt_NSSBegBalance
        '
        Me.txt_NSSBegBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txt_NSSBegBalance.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NSSBegBalance.Location = New System.Drawing.Point(190, 6)
        Me.txt_NSSBegBalance.MaxLength = 20
        Me.txt_NSSBegBalance.Name = "txt_NSSBegBalance"
        Me.txt_NSSBegBalance.Size = New System.Drawing.Size(189, 21)
        Me.txt_NSSBegBalance.TabIndex = 29
        Me.txt_NSSBegBalance.Text = "0.00"
        Me.txt_NSSBegBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Helvetica", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(6, 8)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(118, 15)
        Me.Label36.TabIndex = 28
        Me.Label36.Text = "Beginning Balance:"
        '
        'cmd_Generate
        '
        Me.cmd_Generate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Generate.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmd_Generate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Generate.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight
        Me.cmd_Generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Generate.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Generate.ForeColor = System.Drawing.Color.Black
        Me.cmd_Generate.Image = Global.AccountsManagementForms.My.Resources.Resources.DownloadIcon22x22
        Me.cmd_Generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Generate.Location = New System.Drawing.Point(268, 33)
        Me.cmd_Generate.Name = "cmd_Generate"
        Me.cmd_Generate.Size = New System.Drawing.Size(131, 39)
        Me.cmd_Generate.TabIndex = 31
        Me.cmd_Generate.Text = "Generate"
        Me.cmd_Generate.UseVisualStyleBackColor = True
        '
        'cmd_Close
        '
        Me.cmd_Close.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Close.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmd_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight
        Me.cmd_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Close.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Close.ForeColor = System.Drawing.Color.Black
        Me.cmd_Close.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.cmd_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Close.Location = New System.Drawing.Point(542, 34)
        Me.cmd_Close.Name = "cmd_Close"
        Me.cmd_Close.Size = New System.Drawing.Size(131, 39)
        Me.cmd_Close.TabIndex = 32
        Me.cmd_Close.Text = "Close"
        Me.cmd_Close.UseVisualStyleBackColor = True
        '
        'cmd_export
        '
        Me.cmd_export.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_export.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_export.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmd_export.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_export.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight
        Me.cmd_export.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_export.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_export.ForeColor = System.Drawing.Color.Black
        Me.cmd_export.Image = Global.AccountsManagementForms.My.Resources.Resources.CSVIconColored22x22
        Me.cmd_export.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_export.Location = New System.Drawing.Point(523, 647)
        Me.cmd_export.Name = "cmd_export"
        Me.cmd_export.Size = New System.Drawing.Size(150, 39)
        Me.cmd_export.TabIndex = 33
        Me.cmd_export.Text = "Export to CSV"
        Me.cmd_export.UseVisualStyleBackColor = True
        '
        'nud_Year
        '
        Me.nud_Year.Enabled = False
        Me.nud_Year.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_Year.Location = New System.Drawing.Point(104, 59)
        Me.nud_Year.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nud_Year.Minimum = New Decimal(New Integer() {1990, 0, 0, 0})
        Me.nud_Year.Name = "nud_Year"
        Me.nud_Year.Size = New System.Drawing.Size(138, 20)
        Me.nud_Year.TabIndex = 34
        Me.nud_Year.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_Year.Value = New Decimal(New Integer() {1990, 0, 0, 0})
        '
        'cbo_MonthSelected
        '
        Me.cbo_MonthSelected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_MonthSelected.Enabled = False
        Me.cbo_MonthSelected.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_MonthSelected.FormattingEnabled = True
        Me.cbo_MonthSelected.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.cbo_MonthSelected.Location = New System.Drawing.Point(104, 33)
        Me.cbo_MonthSelected.MaxDropDownItems = 12
        Me.cbo_MonthSelected.Name = "cbo_MonthSelected"
        Me.cbo_MonthSelected.Size = New System.Drawing.Size(138, 20)
        Me.cbo_MonthSelected.TabIndex = 35
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(12, 35)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 14)
        Me.Label17.TabIndex = 36
        Me.Label17.Text = "Select Month:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(12, 61)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(69, 14)
        Me.Label19.TabIndex = 37
        Me.Label19.Text = "Select Year:"
        '
        'cmd_Save
        '
        Me.cmd_Save.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.cmd_Save.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmd_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LemonChiffon
        Me.cmd_Save.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight
        Me.cmd_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Save.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Save.ForeColor = System.Drawing.Color.Black
        Me.cmd_Save.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.cmd_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Save.Location = New System.Drawing.Point(405, 34)
        Me.cmd_Save.Name = "cmd_Save"
        Me.cmd_Save.Size = New System.Drawing.Size(131, 39)
        Me.cmd_Save.TabIndex = 38
        Me.cmd_Save.Text = "Save"
        Me.cmd_Save.UseVisualStyleBackColor = True
        '
        'chk_Search
        '
        Me.chk_Search.AutoSize = True
        Me.chk_Search.Font = New System.Drawing.Font("Helvetica", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Search.ForeColor = System.Drawing.Color.Black
        Me.chk_Search.Location = New System.Drawing.Point(16, 9)
        Me.chk_Search.Name = "chk_Search"
        Me.chk_Search.Size = New System.Drawing.Size(142, 18)
        Me.chk_Search.TabIndex = 39
        Me.chk_Search.Text = "Search Historical Data"
        Me.chk_Search.UseVisualStyleBackColor = True
        '
        'frmBankReconStatement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(687, 695)
        Me.Controls.Add(Me.chk_Search)
        Me.Controls.Add(Me.cmd_Save)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.cbo_MonthSelected)
        Me.Controls.Add(Me.nud_Year)
        Me.Controls.Add(Me.cmd_export)
        Me.Controls.Add(Me.cmd_Close)
        Me.Controls.Add(Me.cmd_Generate)
        Me.Controls.Add(Me.tc_MainWindow)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximumSize = New System.Drawing.Size(703, 733)
        Me.MinimumSize = New System.Drawing.Size(703, 733)
        Me.Name = "frmBankReconStatement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bank Recon Statement"
        Me.tc_MainWindow.ResumeLayout(False)
        Me.tp_STL.ResumeLayout(False)
        Me.tp_STL.PerformLayout()
        Me.tp_PR.ResumeLayout(False)
        Me.tp_PR.PerformLayout()
        Me.tp_NSS.ResumeLayout(False)
        Me.tp_NSS.PerformLayout()
        CType(Me.nud_Year, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tc_MainWindow As System.Windows.Forms.TabControl
    Friend WithEvents tp_STL As System.Windows.Forms.TabPage
    Friend WithEvents tp_PR As System.Windows.Forms.TabPage
    Friend WithEvents txt_stlBegBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_stlBookInterest As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBookMF As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBookPR As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBookNSS As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBookEFT As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBookCheck As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBookCol As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tp_NSS As System.Windows.Forms.TabPage
    Friend WithEvents txt_stlBankEndBalance As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBookEndBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_PRBookPay As System.Windows.Forms.TextBox
    Friend WithEvents txt_PRBankEndBalance As System.Windows.Forms.TextBox
    Friend WithEvents txt_PRBookEndBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txt_PRBookInterest As System.Windows.Forms.TextBox
    Friend WithEvents txt_PRBookPrudential As System.Windows.Forms.TextBox
    Friend WithEvents txt_PRBookCol As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txt_PRBegBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txt_NSSBankEndBal As System.Windows.Forms.TextBox
    Friend WithEvents txt_NSSBookEndBal As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txt_NSSBookInterest As System.Windows.Forms.TextBox
    Friend WithEvents txt_NSSBookNSS As System.Windows.Forms.TextBox
    Friend WithEvents txt_NSSBookCol As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txt_NSSBegBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txt_NSSBookPay As System.Windows.Forms.TextBox
    Friend WithEvents cmd_Generate As System.Windows.Forms.Button
    Friend WithEvents cmd_Close As System.Windows.Forms.Button
    Friend WithEvents cmd_export As System.Windows.Forms.Button
    Friend WithEvents txt_stlBookNSSPay As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txt_stlBookPRRep As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txt_stlBankAdjusted As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBookAdjusted As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txt_stlBookCharges As System.Windows.Forms.TextBox
    Friend WithEvents txt_stlBankCheck As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txt_PRBookDDown As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txt_PRBankAdjusted As System.Windows.Forms.TextBox
    Friend WithEvents txt_PRBookAdjusted As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txt_PRBookCharges As System.Windows.Forms.TextBox
    Friend WithEvents txt_PRBankCheck As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents nud_Year As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbo_MonthSelected As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmd_Save As System.Windows.Forms.Button
    Friend WithEvents chk_Search As System.Windows.Forms.CheckBox
End Class
