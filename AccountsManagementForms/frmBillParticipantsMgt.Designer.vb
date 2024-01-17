<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillParticipantsMgt
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillParticipantsMgt))
        Me.gpMain = New System.Windows.Forms.GroupBox()
        Me.txtBusinesStyle = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtEffectiveDate = New System.Windows.Forms.TextBox()
        Me.chckEcoZoneEffectivityDate = New System.Windows.Forms.CheckBox()
        Me.dtEcoZoneEffectivityDate = New System.Windows.Forms.DateTimePicker()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtEcoZoneRegCertNo = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.ddlType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtParticipantID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtIDNumber = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTIN = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.gpRep = New System.Windows.Forms.GroupBox()
        Me.txtRepLName = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtRepMName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRepEmailAddress = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtRepContactNumbers = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtRepPosition = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtRepFName = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBank = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtBankBranch = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtCheckPay = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtBankTransCode = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtMFWHVat = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.ddlPaymentType = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtBankAccountNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtMFWHTax = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.gpBankInfo = New System.Windows.Forms.GroupBox()
        Me.txtVirtualAccountNo = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.gpRateInfo = New System.Windows.Forms.GroupBox()
        Me.txtEnergyWHVAT = New System.Windows.Forms.TextBox()
        Me.txtEnergyWHTax = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.gpOtherInfo = New System.Windows.Forms.GroupBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.rbInactive = New System.Windows.Forms.RadioButton()
        Me.rbActive = New System.Windows.Forms.RadioButton()
        Me.CB_MembershipType = New System.Windows.Forms.ComboBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.ATCType_cmb = New System.Windows.Forms.ComboBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.gpZeroRatedEnergy = New System.Windows.Forms.GroupBox()
        Me.rbZeroRatedEnergyNo = New System.Windows.Forms.RadioButton()
        Me.rbZeroRatedEnergyYes = New System.Windows.Forms.RadioButton()
        Me.gpZeroRatedMF = New System.Windows.Forms.GroupBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.rbZeroRatedMFNo = New System.Windows.Forms.RadioButton()
        Me.rbZeroRatedMFYes = New System.Windows.Forms.RadioButton()
        Me.gpAddress = New System.Windows.Forms.GroupBox()
        Me.txtBillingAddress = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtParticipantAddress = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.ddlRegion = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtZipCode = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtProvince = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMunicipality = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.gpMain.SuspendLayout()
        Me.gpRep.SuspendLayout()
        Me.gpBankInfo.SuspendLayout()
        Me.gpRateInfo.SuspendLayout()
        Me.gpOtherInfo.SuspendLayout()
        Me.gpZeroRatedEnergy.SuspendLayout()
        Me.gpZeroRatedMF.SuspendLayout()
        Me.gpAddress.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gpMain
        '
        Me.gpMain.Controls.Add(Me.txtBusinesStyle)
        Me.gpMain.Controls.Add(Me.Label6)
        Me.gpMain.Controls.Add(Me.txtEffectiveDate)
        Me.gpMain.Controls.Add(Me.chckEcoZoneEffectivityDate)
        Me.gpMain.Controls.Add(Me.dtEcoZoneEffectivityDate)
        Me.gpMain.Controls.Add(Me.Label41)
        Me.gpMain.Controls.Add(Me.txtEcoZoneRegCertNo)
        Me.gpMain.Controls.Add(Me.Label40)
        Me.gpMain.Controls.Add(Me.Label32)
        Me.gpMain.Controls.Add(Me.ddlType)
        Me.gpMain.Controls.Add(Me.Label8)
        Me.gpMain.Controls.Add(Me.txtFullName)
        Me.gpMain.Controls.Add(Me.Label3)
        Me.gpMain.Controls.Add(Me.txtParticipantID)
        Me.gpMain.Controls.Add(Me.Label2)
        Me.gpMain.Controls.Add(Me.txtIDNumber)
        Me.gpMain.Controls.Add(Me.Label1)
        Me.gpMain.Location = New System.Drawing.Point(8, 12)
        Me.gpMain.Name = "gpMain"
        Me.gpMain.Size = New System.Drawing.Size(450, 230)
        Me.gpMain.TabIndex = 0
        Me.gpMain.TabStop = False
        '
        'txtBusinesStyle
        '
        Me.txtBusinesStyle.Location = New System.Drawing.Point(175, 91)
        Me.txtBusinesStyle.MaxLength = 150
        Me.txtBusinesStyle.Multiline = True
        Me.txtBusinesStyle.Name = "txtBusinesStyle"
        Me.txtBusinesStyle.Size = New System.Drawing.Size(256, 47)
        Me.txtBusinesStyle.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(78, 94)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 14)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Business Style:"
        '
        'txtEffectiveDate
        '
        Me.txtEffectiveDate.BackColor = System.Drawing.Color.White
        Me.txtEffectiveDate.Location = New System.Drawing.Point(197, 166)
        Me.txtEffectiveDate.MaxLength = 30
        Me.txtEffectiveDate.Name = "txtEffectiveDate"
        Me.txtEffectiveDate.ReadOnly = True
        Me.txtEffectiveDate.Size = New System.Drawing.Size(234, 20)
        Me.txtEffectiveDate.TabIndex = 60
        '
        'chckEcoZoneEffectivityDate
        '
        Me.chckEcoZoneEffectivityDate.AutoSize = True
        Me.chckEcoZoneEffectivityDate.Location = New System.Drawing.Point(176, 170)
        Me.chckEcoZoneEffectivityDate.Name = "chckEcoZoneEffectivityDate"
        Me.chckEcoZoneEffectivityDate.Size = New System.Drawing.Size(15, 14)
        Me.chckEcoZoneEffectivityDate.TabIndex = 6
        Me.chckEcoZoneEffectivityDate.UseVisualStyleBackColor = True
        '
        'dtEcoZoneEffectivityDate
        '
        Me.dtEcoZoneEffectivityDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEcoZoneEffectivityDate.Location = New System.Drawing.Point(197, 166)
        Me.dtEcoZoneEffectivityDate.Name = "dtEcoZoneEffectivityDate"
        Me.dtEcoZoneEffectivityDate.Size = New System.Drawing.Size(234, 20)
        Me.dtEcoZoneEffectivityDate.TabIndex = 51
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(26, 169)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(144, 14)
        Me.Label41.TabIndex = 47
        Me.Label41.Text = "Eco-Zone Effectivity Date:"
        '
        'txtEcoZoneRegCertNo
        '
        Me.txtEcoZoneRegCertNo.Location = New System.Drawing.Point(175, 142)
        Me.txtEcoZoneRegCertNo.MaxLength = 30
        Me.txtEcoZoneRegCertNo.Name = "txtEcoZoneRegCertNo"
        Me.txtEcoZoneRegCertNo.Size = New System.Drawing.Size(256, 20)
        Me.txtEcoZoneRegCertNo.TabIndex = 5
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(32, 145)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(138, 14)
        Me.Label40.TabIndex = 45
        Me.Label40.Text = "Eco-Zone Reg. Cert. No.:"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label32.Location = New System.Drawing.Point(6, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(103, 14)
        Me.Label32.TabIndex = 44
        Me.Label32.Text = "Main Information:"
        '
        'ddlType
        '
        Me.ddlType.BackColor = System.Drawing.Color.White
        Me.ddlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlType.FormattingEnabled = True
        Me.ddlType.Location = New System.Drawing.Point(176, 190)
        Me.ddlType.Name = "ddlType"
        Me.ddlType.Size = New System.Drawing.Size(256, 22)
        Me.ddlType.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(134, 193)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 14)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Type:"
        '
        'txtFullName
        '
        Me.txtFullName.Location = New System.Drawing.Point(175, 67)
        Me.txtFullName.MaxLength = 150
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(256, 20)
        Me.txtFullName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(129, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Name:"
        '
        'txtParticipantID
        '
        Me.txtParticipantID.Location = New System.Drawing.Point(175, 44)
        Me.txtParticipantID.MaxLength = 30
        Me.txtParticipantID.Name = "txtParticipantID"
        Me.txtParticipantID.Size = New System.Drawing.Size(256, 20)
        Me.txtParticipantID.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(89, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Participant ID:"
        '
        'txtIDNumber
        '
        Me.txtIDNumber.Location = New System.Drawing.Point(175, 21)
        Me.txtIDNumber.MaxLength = 30
        Me.txtIDNumber.Name = "txtIDNumber"
        Me.txtIDNumber.Size = New System.Drawing.Size(256, 20)
        Me.txtIDNumber.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(103, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID Number:"
        '
        'txtTIN
        '
        Me.txtTIN.Location = New System.Drawing.Point(172, 19)
        Me.txtTIN.MaxLength = 30
        Me.txtTIN.Name = "txtTIN"
        Me.txtTIN.Size = New System.Drawing.Size(251, 20)
        Me.txtTIN.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(43, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(122, 14)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Tax Identification No.:"
        '
        'gpRep
        '
        Me.gpRep.Controls.Add(Me.txtRepLName)
        Me.gpRep.Controls.Add(Me.Label22)
        Me.gpRep.Controls.Add(Me.txtRepMName)
        Me.gpRep.Controls.Add(Me.Label7)
        Me.gpRep.Controls.Add(Me.txtTitle)
        Me.gpRep.Controls.Add(Me.Label4)
        Me.gpRep.Controls.Add(Me.txtRepEmailAddress)
        Me.gpRep.Controls.Add(Me.Label17)
        Me.gpRep.Controls.Add(Me.txtRepContactNumbers)
        Me.gpRep.Controls.Add(Me.Label16)
        Me.gpRep.Controls.Add(Me.txtRepPosition)
        Me.gpRep.Controls.Add(Me.Label15)
        Me.gpRep.Controls.Add(Me.txtRepFName)
        Me.gpRep.Controls.Add(Me.Label14)
        Me.gpRep.Controls.Add(Me.Label13)
        Me.gpRep.Location = New System.Drawing.Point(476, 203)
        Me.gpRep.Name = "gpRep"
        Me.gpRep.Size = New System.Drawing.Size(450, 226)
        Me.gpRep.TabIndex = 6
        Me.gpRep.TabStop = False
        '
        'txtRepLName
        '
        Me.txtRepLName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRepLName.Location = New System.Drawing.Point(167, 89)
        Me.txtRepLName.MaxLength = 50
        Me.txtRepLName.Name = "txtRepLName"
        Me.txtRepLName.Size = New System.Drawing.Size(256, 20)
        Me.txtRepLName.TabIndex = 4
        '
        'Label22
        '
        Me.Label22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(94, 92)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(68, 14)
        Me.Label22.TabIndex = 43
        Me.Label22.Text = "Last Name:"
        '
        'txtRepMName
        '
        Me.txtRepMName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRepMName.Location = New System.Drawing.Point(167, 65)
        Me.txtRepMName.MaxLength = 50
        Me.txtRepMName.Name = "txtRepMName"
        Me.txtRepMName.Size = New System.Drawing.Size(256, 20)
        Me.txtRepMName.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(50, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 14)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "Middle Initial/Name:"
        '
        'txtTitle
        '
        Me.txtTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTitle.Location = New System.Drawing.Point(168, 19)
        Me.txtTitle.MaxLength = 50
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(256, 20)
        Me.txtTitle.TabIndex = 40
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(128, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 14)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Title:"
        '
        'txtRepEmailAddress
        '
        Me.txtRepEmailAddress.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRepEmailAddress.Location = New System.Drawing.Point(168, 162)
        Me.txtRepEmailAddress.MaxLength = 50
        Me.txtRepEmailAddress.Multiline = True
        Me.txtRepEmailAddress.Name = "txtRepEmailAddress"
        Me.txtRepEmailAddress.Size = New System.Drawing.Size(256, 56)
        Me.txtRepEmailAddress.TabIndex = 7
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(74, 165)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 14)
        Me.Label17.TabIndex = 37
        Me.Label17.Text = "Email Address:"
        '
        'txtRepContactNumbers
        '
        Me.txtRepContactNumbers.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRepContactNumbers.Location = New System.Drawing.Point(168, 138)
        Me.txtRepContactNumbers.MaxLength = 50
        Me.txtRepContactNumbers.Name = "txtRepContactNumbers"
        Me.txtRepContactNumbers.Size = New System.Drawing.Size(256, 20)
        Me.txtRepContactNumbers.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(58, 141)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(106, 14)
        Me.Label16.TabIndex = 35
        Me.Label16.Text = "ContactNumber/s:"
        '
        'txtRepPosition
        '
        Me.txtRepPosition.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRepPosition.Location = New System.Drawing.Point(168, 114)
        Me.txtRepPosition.MaxLength = 50
        Me.txtRepPosition.Name = "txtRepPosition"
        Me.txtRepPosition.Size = New System.Drawing.Size(256, 20)
        Me.txtRepPosition.TabIndex = 5
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(108, 117)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 14)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "Position:"
        '
        'txtRepFName
        '
        Me.txtRepFName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRepFName.Location = New System.Drawing.Point(168, 42)
        Me.txtRepFName.MaxLength = 50
        Me.txtRepFName.Name = "txtRepFName"
        Me.txtRepFName.Size = New System.Drawing.Size(256, 20)
        Me.txtRepFName.TabIndex = 2
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(94, 45)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(69, 14)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "First Name:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(6, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 14)
        Me.Label13.TabIndex = 30
        Me.Label13.Text = "Representative:"
        '
        'txtBank
        '
        Me.txtBank.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBank.Location = New System.Drawing.Point(168, 66)
        Me.txtBank.MaxLength = 50
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(256, 20)
        Me.txtBank.TabIndex = 3
        '
        'Label30
        '
        Me.Label30.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(92, 68)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(71, 14)
        Me.Label30.TabIndex = 48
        Me.Label30.Text = "Bank Name:"
        '
        'txtBankBranch
        '
        Me.txtBankBranch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBankBranch.Location = New System.Drawing.Point(168, 89)
        Me.txtBankBranch.MaxLength = 100
        Me.txtBankBranch.Name = "txtBankBranch"
        Me.txtBankBranch.Size = New System.Drawing.Size(256, 20)
        Me.txtBankBranch.TabIndex = 4
        '
        'Label29
        '
        Me.Label29.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(85, 92)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(78, 14)
        Me.Label29.TabIndex = 46
        Me.Label29.Text = "Bank Branch:"
        '
        'txtCheckPay
        '
        Me.txtCheckPay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCheckPay.Location = New System.Drawing.Point(168, 112)
        Me.txtCheckPay.MaxLength = 200
        Me.txtCheckPay.Name = "txtCheckPay"
        Me.txtCheckPay.Size = New System.Drawing.Size(256, 20)
        Me.txtCheckPay.TabIndex = 5
        '
        'Label28
        '
        Me.Label28.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(83, 114)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(81, 14)
        Me.Label28.TabIndex = 44
        Me.Label28.Text = "Check Payee:"
        '
        'txtBankTransCode
        '
        Me.txtBankTransCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBankTransCode.Location = New System.Drawing.Point(168, 19)
        Me.txtBankTransCode.MaxLength = 30
        Me.txtBankTransCode.Name = "txtBankTransCode"
        Me.txtBankTransCode.Size = New System.Drawing.Size(256, 20)
        Me.txtBankTransCode.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(28, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(136, 14)
        Me.Label20.TabIndex = 40
        Me.Label20.Text = "Bank Transaction Code:"
        '
        'txtMFWHVat
        '
        Me.txtMFWHVat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMFWHVat.Location = New System.Drawing.Point(168, 41)
        Me.txtMFWHVat.MaxLength = 6
        Me.txtMFWHVat.Name = "txtMFWHVat"
        Me.txtMFWHVat.Size = New System.Drawing.Size(256, 20)
        Me.txtMFWHVat.TabIndex = 2
        Me.txtMFWHVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(17, 44)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(147, 14)
        Me.Label19.TabIndex = 38
        Me.Label19.Text = "Market Fees WHVAT Rate:"
        '
        'ddlPaymentType
        '
        Me.ddlPaymentType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ddlPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlPaymentType.FormattingEnabled = True
        Me.ddlPaymentType.Location = New System.Drawing.Point(168, 160)
        Me.ddlPaymentType.Name = "ddlPaymentType"
        Me.ddlPaymentType.Size = New System.Drawing.Size(256, 22)
        Me.ddlPaymentType.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(77, 164)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(87, 14)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Payment Type:"
        '
        'txtBankAccountNo
        '
        Me.txtBankAccountNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBankAccountNo.Location = New System.Drawing.Point(168, 42)
        Me.txtBankAccountNo.MaxLength = 30
        Me.txtBankAccountNo.Name = "txtBankAccountNo"
        Me.txtBankAccountNo.Size = New System.Drawing.Size(256, 20)
        Me.txtBankAccountNo.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(58, 45)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(105, 14)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Bank Account No.:"
        '
        'txtMFWHTax
        '
        Me.txtMFWHTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMFWHTax.Location = New System.Drawing.Point(168, 18)
        Me.txtMFWHTax.MaxLength = 6
        Me.txtMFWHTax.Name = "txtMFWHTax"
        Me.txtMFWHTax.Size = New System.Drawing.Size(256, 20)
        Me.txtMFWHTax.TabIndex = 1
        Me.txtMFWHTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(20, 21)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(144, 14)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Market Fees WHTax Rate:"
        '
        'gpBankInfo
        '
        Me.gpBankInfo.Controls.Add(Me.txtVirtualAccountNo)
        Me.gpBankInfo.Controls.Add(Me.Label44)
        Me.gpBankInfo.Controls.Add(Me.Label31)
        Me.gpBankInfo.Controls.Add(Me.txtBank)
        Me.gpBankInfo.Controls.Add(Me.Label30)
        Me.gpBankInfo.Controls.Add(Me.txtBankBranch)
        Me.gpBankInfo.Controls.Add(Me.Label29)
        Me.gpBankInfo.Controls.Add(Me.Label12)
        Me.gpBankInfo.Controls.Add(Me.txtCheckPay)
        Me.gpBankInfo.Controls.Add(Me.ddlPaymentType)
        Me.gpBankInfo.Controls.Add(Me.Label11)
        Me.gpBankInfo.Controls.Add(Me.Label28)
        Me.gpBankInfo.Controls.Add(Me.txtBankAccountNo)
        Me.gpBankInfo.Controls.Add(Me.txtBankTransCode)
        Me.gpBankInfo.Controls.Add(Me.Label20)
        Me.gpBankInfo.Location = New System.Drawing.Point(476, 9)
        Me.gpBankInfo.Name = "gpBankInfo"
        Me.gpBankInfo.Size = New System.Drawing.Size(450, 191)
        Me.gpBankInfo.TabIndex = 5
        Me.gpBankInfo.TabStop = False
        '
        'txtVirtualAccountNo
        '
        Me.txtVirtualAccountNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVirtualAccountNo.Location = New System.Drawing.Point(168, 135)
        Me.txtVirtualAccountNo.MaxLength = 30
        Me.txtVirtualAccountNo.Name = "txtVirtualAccountNo"
        Me.txtVirtualAccountNo.Size = New System.Drawing.Size(256, 20)
        Me.txtVirtualAccountNo.TabIndex = 6
        '
        'Label44
        '
        Me.Label44.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(49, 137)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(114, 14)
        Me.Label44.TabIndex = 50
        Me.Label44.Text = "Virtual Account No.:"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label31.Location = New System.Drawing.Point(6, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(104, 14)
        Me.Label31.TabIndex = 31
        Me.Label31.Text = "Bank Information:"
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(48, 95)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(116, 14)
        Me.Label36.TabIndex = 54
        Me.Label36.Text = "Energy WHVAT Rate:"
        '
        'Label33
        '
        Me.Label33.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(51, 69)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(113, 14)
        Me.Label33.TabIndex = 50
        Me.Label33.Text = "Energy WHTax Rate:"
        '
        'gpRateInfo
        '
        Me.gpRateInfo.Controls.Add(Me.txtEnergyWHVAT)
        Me.gpRateInfo.Controls.Add(Me.txtEnergyWHTax)
        Me.gpRateInfo.Controls.Add(Me.Label42)
        Me.gpRateInfo.Controls.Add(Me.Label36)
        Me.gpRateInfo.Controls.Add(Me.Label33)
        Me.gpRateInfo.Controls.Add(Me.txtMFWHTax)
        Me.gpRateInfo.Controls.Add(Me.Label19)
        Me.gpRateInfo.Controls.Add(Me.txtMFWHVat)
        Me.gpRateInfo.Controls.Add(Me.Label9)
        Me.gpRateInfo.Location = New System.Drawing.Point(476, 435)
        Me.gpRateInfo.Name = "gpRateInfo"
        Me.gpRateInfo.Size = New System.Drawing.Size(450, 126)
        Me.gpRateInfo.TabIndex = 1
        Me.gpRateInfo.TabStop = False
        '
        'txtEnergyWHVAT
        '
        Me.txtEnergyWHVAT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEnergyWHVAT.Location = New System.Drawing.Point(168, 92)
        Me.txtEnergyWHVAT.MaxLength = 6
        Me.txtEnergyWHVAT.Name = "txtEnergyWHVAT"
        Me.txtEnergyWHVAT.Size = New System.Drawing.Size(256, 20)
        Me.txtEnergyWHVAT.TabIndex = 4
        Me.txtEnergyWHVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEnergyWHTax
        '
        Me.txtEnergyWHTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEnergyWHTax.Location = New System.Drawing.Point(168, 66)
        Me.txtEnergyWHTax.MaxLength = 6
        Me.txtEnergyWHTax.Name = "txtEnergyWHTax"
        Me.txtEnergyWHTax.Size = New System.Drawing.Size(256, 20)
        Me.txtEnergyWHTax.TabIndex = 3
        Me.txtEnergyWHTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label42.Location = New System.Drawing.Point(6, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(101, 14)
        Me.Label42.TabIndex = 58
        Me.Label42.Text = "Rate Information:"
        '
        'gpOtherInfo
        '
        Me.gpOtherInfo.Controls.Add(Me.Label34)
        Me.gpOtherInfo.Controls.Add(Me.rbInactive)
        Me.gpOtherInfo.Controls.Add(Me.rbActive)
        Me.gpOtherInfo.Controls.Add(Me.CB_MembershipType)
        Me.gpOtherInfo.Controls.Add(Me.Label35)
        Me.gpOtherInfo.Controls.Add(Me.ATCType_cmb)
        Me.gpOtherInfo.Controls.Add(Me.Label43)
        Me.gpOtherInfo.Controls.Add(Me.Label48)
        Me.gpOtherInfo.Controls.Add(Me.txtTIN)
        Me.gpOtherInfo.Controls.Add(Me.Label10)
        Me.gpOtherInfo.Location = New System.Drawing.Point(8, 570)
        Me.gpOtherInfo.Name = "gpOtherInfo"
        Me.gpOtherInfo.Size = New System.Drawing.Size(918, 77)
        Me.gpOtherInfo.TabIndex = 4
        Me.gpOtherInfo.TabStop = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(587, 48)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(45, 14)
        Me.Label34.TabIndex = 63
        Me.Label34.Text = "Status:"
        '
        'rbInactive
        '
        Me.rbInactive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbInactive.AutoSize = True
        Me.rbInactive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbInactive.Location = New System.Drawing.Point(701, 45)
        Me.rbInactive.Name = "rbInactive"
        Me.rbInactive.Size = New System.Drawing.Size(68, 18)
        Me.rbInactive.TabIndex = 2
        Me.rbInactive.TabStop = True
        Me.rbInactive.Text = "In-Active"
        Me.rbInactive.UseVisualStyleBackColor = True
        '
        'rbActive
        '
        Me.rbActive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbActive.AutoSize = True
        Me.rbActive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbActive.Location = New System.Drawing.Point(639, 45)
        Me.rbActive.Name = "rbActive"
        Me.rbActive.Size = New System.Drawing.Size(56, 18)
        Me.rbActive.TabIndex = 1
        Me.rbActive.TabStop = True
        Me.rbActive.Text = "Active"
        Me.rbActive.UseVisualStyleBackColor = True
        '
        'CB_MembershipType
        '
        Me.CB_MembershipType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_MembershipType.FormattingEnabled = True
        Me.CB_MembershipType.Location = New System.Drawing.Point(635, 13)
        Me.CB_MembershipType.Name = "CB_MembershipType"
        Me.CB_MembershipType.Size = New System.Drawing.Size(256, 22)
        Me.CB_MembershipType.TabIndex = 60
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(521, 17)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(110, 14)
        Me.Label35.TabIndex = 61
        Me.Label35.Text = "Membership Type:"
        '
        'ATCType_cmb
        '
        Me.ATCType_cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ATCType_cmb.FormattingEnabled = True
        Me.ATCType_cmb.Location = New System.Drawing.Point(173, 45)
        Me.ATCType_cmb.Name = "ATCType_cmb"
        Me.ATCType_cmb.Size = New System.Drawing.Size(251, 22)
        Me.ATCType_cmb.TabIndex = 2
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(6, 0)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(108, 14)
        Me.Label43.TabIndex = 59
        Me.Label43.Text = "Other Information:"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(8, 47)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(160, 14)
        Me.Label48.TabIndex = 32
        Me.Label48.Text = "BIR Aplhanumeric Tax Code:"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.AccountsManagementForms.My.Resources.Resources.SaveIconColored22x22
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(676, 658)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(121, 39)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = Global.AccountsManagementForms.My.Resources.Resources.CloseIconRed22x22
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(805, 658)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(121, 39)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label46.Location = New System.Drawing.Point(6, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(111, 14)
        Me.Label46.TabIndex = 54
        Me.Label46.Text = "Zero Rated Energy:"
        '
        'gpZeroRatedEnergy
        '
        Me.gpZeroRatedEnergy.Controls.Add(Me.Label46)
        Me.gpZeroRatedEnergy.Controls.Add(Me.rbZeroRatedEnergyNo)
        Me.gpZeroRatedEnergy.Controls.Add(Me.rbZeroRatedEnergyYes)
        Me.gpZeroRatedEnergy.Location = New System.Drawing.Point(7, 512)
        Me.gpZeroRatedEnergy.Name = "gpZeroRatedEnergy"
        Me.gpZeroRatedEnergy.Size = New System.Drawing.Size(450, 34)
        Me.gpZeroRatedEnergy.TabIndex = 3
        Me.gpZeroRatedEnergy.TabStop = False
        '
        'rbZeroRatedEnergyNo
        '
        Me.rbZeroRatedEnergyNo.AutoSize = True
        Me.rbZeroRatedEnergyNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbZeroRatedEnergyNo.Location = New System.Drawing.Point(235, 10)
        Me.rbZeroRatedEnergyNo.Name = "rbZeroRatedEnergyNo"
        Me.rbZeroRatedEnergyNo.Size = New System.Drawing.Size(38, 18)
        Me.rbZeroRatedEnergyNo.TabIndex = 2
        Me.rbZeroRatedEnergyNo.TabStop = True
        Me.rbZeroRatedEnergyNo.Text = "No"
        Me.rbZeroRatedEnergyNo.UseVisualStyleBackColor = True
        '
        'rbZeroRatedEnergyYes
        '
        Me.rbZeroRatedEnergyYes.AutoSize = True
        Me.rbZeroRatedEnergyYes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbZeroRatedEnergyYes.Location = New System.Drawing.Point(174, 12)
        Me.rbZeroRatedEnergyYes.Name = "rbZeroRatedEnergyYes"
        Me.rbZeroRatedEnergyYes.Size = New System.Drawing.Size(44, 18)
        Me.rbZeroRatedEnergyYes.TabIndex = 1
        Me.rbZeroRatedEnergyYes.TabStop = True
        Me.rbZeroRatedEnergyYes.Text = "Yes"
        Me.rbZeroRatedEnergyYes.UseVisualStyleBackColor = True
        '
        'gpZeroRatedMF
        '
        Me.gpZeroRatedMF.Controls.Add(Me.Label45)
        Me.gpZeroRatedMF.Controls.Add(Me.rbZeroRatedMFNo)
        Me.gpZeroRatedMF.Controls.Add(Me.rbZeroRatedMFYes)
        Me.gpZeroRatedMF.Location = New System.Drawing.Point(8, 472)
        Me.gpZeroRatedMF.Name = "gpZeroRatedMF"
        Me.gpZeroRatedMF.Size = New System.Drawing.Size(450, 34)
        Me.gpZeroRatedMF.TabIndex = 2
        Me.gpZeroRatedMF.TabStop = False
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label45.Location = New System.Drawing.Point(6, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(142, 14)
        Me.Label45.TabIndex = 44
        Me.Label45.Text = "Zero Rated Market Fees:"
        '
        'rbZeroRatedMFNo
        '
        Me.rbZeroRatedMFNo.AutoSize = True
        Me.rbZeroRatedMFNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbZeroRatedMFNo.Location = New System.Drawing.Point(234, 12)
        Me.rbZeroRatedMFNo.Name = "rbZeroRatedMFNo"
        Me.rbZeroRatedMFNo.Size = New System.Drawing.Size(38, 18)
        Me.rbZeroRatedMFNo.TabIndex = 2
        Me.rbZeroRatedMFNo.TabStop = True
        Me.rbZeroRatedMFNo.Text = "No"
        Me.rbZeroRatedMFNo.UseVisualStyleBackColor = True
        '
        'rbZeroRatedMFYes
        '
        Me.rbZeroRatedMFYes.AutoSize = True
        Me.rbZeroRatedMFYes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbZeroRatedMFYes.Location = New System.Drawing.Point(172, 12)
        Me.rbZeroRatedMFYes.Name = "rbZeroRatedMFYes"
        Me.rbZeroRatedMFYes.Size = New System.Drawing.Size(44, 18)
        Me.rbZeroRatedMFYes.TabIndex = 17
        Me.rbZeroRatedMFYes.TabStop = True
        Me.rbZeroRatedMFYes.Text = "Yes"
        Me.rbZeroRatedMFYes.UseVisualStyleBackColor = True
        '
        'gpAddress
        '
        Me.gpAddress.Controls.Add(Me.txtBillingAddress)
        Me.gpAddress.Controls.Add(Me.Label23)
        Me.gpAddress.Controls.Add(Me.txtParticipantAddress)
        Me.gpAddress.Controls.Add(Me.Label38)
        Me.gpAddress.Controls.Add(Me.ddlRegion)
        Me.gpAddress.Controls.Add(Me.Label26)
        Me.gpAddress.Controls.Add(Me.txtZipCode)
        Me.gpAddress.Controls.Add(Me.Label25)
        Me.gpAddress.Controls.Add(Me.txtProvince)
        Me.gpAddress.Controls.Add(Me.Label5)
        Me.gpAddress.Controls.Add(Me.txtMunicipality)
        Me.gpAddress.Controls.Add(Me.Label21)
        Me.gpAddress.Controls.Add(Me.Label24)
        Me.gpAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpAddress.Location = New System.Drawing.Point(8, 248)
        Me.gpAddress.Name = "gpAddress"
        Me.gpAddress.Size = New System.Drawing.Size(450, 218)
        Me.gpAddress.TabIndex = 1
        Me.gpAddress.TabStop = False
        '
        'txtBillingAddress
        '
        Me.txtBillingAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillingAddress.Location = New System.Drawing.Point(173, 57)
        Me.txtBillingAddress.MaxLength = 240
        Me.txtBillingAddress.Multiline = True
        Me.txtBillingAddress.Name = "txtBillingAddress"
        Me.txtBillingAddress.Size = New System.Drawing.Size(256, 32)
        Me.txtBillingAddress.TabIndex = 2
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(74, 60)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(94, 14)
        Me.Label23.TabIndex = 44
        Me.Label23.Text = "Billing Address:"
        '
        'txtParticipantAddress
        '
        Me.txtParticipantAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParticipantAddress.Location = New System.Drawing.Point(173, 20)
        Me.txtParticipantAddress.MaxLength = 240
        Me.txtParticipantAddress.Multiline = True
        Me.txtParticipantAddress.Name = "txtParticipantAddress"
        Me.txtParticipantAddress.Size = New System.Drawing.Size(256, 32)
        Me.txtParticipantAddress.TabIndex = 1
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(49, 23)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(119, 14)
        Me.Label38.TabIndex = 43
        Me.Label38.Text = "Participant Address:"
        '
        'ddlRegion
        '
        Me.ddlRegion.BackColor = System.Drawing.Color.White
        Me.ddlRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlRegion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlRegion.FormattingEnabled = True
        Me.ddlRegion.Location = New System.Drawing.Point(172, 175)
        Me.ddlRegion.Name = "ddlRegion"
        Me.ddlRegion.Size = New System.Drawing.Size(256, 22)
        Me.ddlRegion.TabIndex = 6
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(120, 178)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(48, 14)
        Me.Label26.TabIndex = 41
        Me.Label26.Text = "Region:"
        '
        'txtZipCode
        '
        Me.txtZipCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZipCode.Location = New System.Drawing.Point(173, 149)
        Me.txtZipCode.MaxLength = 10
        Me.txtZipCode.Name = "txtZipCode"
        Me.txtZipCode.Size = New System.Drawing.Size(109, 20)
        Me.txtZipCode.TabIndex = 5
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(109, 151)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(59, 14)
        Me.Label25.TabIndex = 39
        Me.Label25.Text = "Zip Code:"
        '
        'txtProvince
        '
        Me.txtProvince.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProvince.Location = New System.Drawing.Point(173, 121)
        Me.txtProvince.MaxLength = 30
        Me.txtProvince.Name = "txtProvince"
        Me.txtProvince.Size = New System.Drawing.Size(255, 20)
        Me.txtProvince.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(110, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 14)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Province:"
        '
        'txtMunicipality
        '
        Me.txtMunicipality.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMunicipality.Location = New System.Drawing.Point(173, 95)
        Me.txtMunicipality.MaxLength = 30
        Me.txtMunicipality.Name = "txtMunicipality"
        Me.txtMunicipality.Size = New System.Drawing.Size(255, 20)
        Me.txtMunicipality.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(69, 98)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(99, 14)
        Me.Label21.TabIndex = 35
        Me.Label21.Text = "Municipality/City:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(6, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(58, 14)
        Me.Label24.TabIndex = 30
        Me.Label24.Text = "Address:"
        Me.Label24.Visible = False
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmBillParticipantsMgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(936, 709)
        Me.ControlBox = False
        Me.Controls.Add(Me.gpZeroRatedEnergy)
        Me.Controls.Add(Me.gpZeroRatedMF)
        Me.Controls.Add(Me.gpAddress)
        Me.Controls.Add(Me.gpOtherInfo)
        Me.Controls.Add(Me.gpRateInfo)
        Me.Controls.Add(Me.gpBankInfo)
        Me.Controls.Add(Me.gpMain)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.gpRep)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBillParticipantsMgt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Participants Management"
        Me.gpMain.ResumeLayout(False)
        Me.gpMain.PerformLayout()
        Me.gpRep.ResumeLayout(False)
        Me.gpRep.PerformLayout()
        Me.gpBankInfo.ResumeLayout(False)
        Me.gpBankInfo.PerformLayout()
        Me.gpRateInfo.ResumeLayout(False)
        Me.gpRateInfo.PerformLayout()
        Me.gpOtherInfo.ResumeLayout(False)
        Me.gpOtherInfo.PerformLayout()
        Me.gpZeroRatedEnergy.ResumeLayout(False)
        Me.gpZeroRatedEnergy.PerformLayout()
        Me.gpZeroRatedMF.ResumeLayout(False)
        Me.gpZeroRatedMF.PerformLayout()
        Me.gpAddress.ResumeLayout(False)
        Me.gpAddress.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gpMain As System.Windows.Forms.GroupBox
    Friend WithEvents txtIDNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtParticipantID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ddlType As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtMFWHTax As System.Windows.Forms.TextBox
    Friend WithEvents ddlPaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBankAccountNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents gpRep As System.Windows.Forms.GroupBox
    Friend WithEvents txtRepFName As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtRepPosition As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtRepContactNumbers As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtRepEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtMFWHVat As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtBankTransCode As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtBankBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtCheckPay As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtTIN As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents gpBankInfo As System.Windows.Forms.GroupBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtEcoZoneRegCertNo As System.Windows.Forms.TextBox
    Friend WithEvents gpRateInfo As System.Windows.Forms.GroupBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents gpOtherInfo As System.Windows.Forms.GroupBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtEnergyWHVAT As System.Windows.Forms.TextBox
    Friend WithEvents txtEnergyWHTax As System.Windows.Forms.TextBox
    Friend WithEvents txtVirtualAccountNo As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents chckEcoZoneEffectivityDate As System.Windows.Forms.CheckBox
    Friend WithEvents dtEcoZoneEffectivityDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtEffectiveDate As System.Windows.Forms.TextBox
    Friend WithEvents ATCType_cmb As System.Windows.Forms.ComboBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBusinesStyle As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents gpZeroRatedEnergy As System.Windows.Forms.GroupBox
    Friend WithEvents rbZeroRatedEnergyNo As System.Windows.Forms.RadioButton
    Friend WithEvents rbZeroRatedEnergyYes As System.Windows.Forms.RadioButton
    Friend WithEvents gpZeroRatedMF As System.Windows.Forms.GroupBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents rbZeroRatedMFNo As System.Windows.Forms.RadioButton
    Friend WithEvents rbZeroRatedMFYes As System.Windows.Forms.RadioButton
    Friend WithEvents gpAddress As System.Windows.Forms.GroupBox
    Friend WithEvents txtParticipantAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents ddlRegion As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtZipCode As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtProvince As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMunicipality As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents rbInactive As System.Windows.Forms.RadioButton
    Friend WithEvents rbActive As System.Windows.Forms.RadioButton
    Friend WithEvents txtRepLName As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtRepMName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtBillingAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents CB_MembershipType As System.Windows.Forms.ComboBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As Label
End Class
