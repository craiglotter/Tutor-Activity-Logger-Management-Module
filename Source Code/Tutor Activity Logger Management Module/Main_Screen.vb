Imports Microsoft.Win32
Imports System.IO
Imports System.Data.Sqlclient

Public Class Main_Screen
    Inherits System.Windows.Forms.Form

    Dim WithEvents Worker1 As Worker
    Public Delegate Sub WorkerhHandler(ByVal Result As String)
    Public Delegate Sub WorkerProgresshHandler()
    Public Delegate Sub WorkerFileCreatedhHandler()
    Public Delegate Sub WorkerErrorEncountered(ByVal ex As Exception)
    Public Delegate Sub WorkerMessageExtractedhHandler(ByVal str As String)

    Private application_exit As Boolean = False
    Private shutting_down As Boolean = False
    Private splash_loader As Splash_Screen
    Public dataloaded As Boolean = False
    Private testingconnection As Boolean = False
    Private loginaudited As Boolean = False
    Private error_reporting_level

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Worker1 = New Worker
        AddHandler Worker1.WorkerComplete, AddressOf WorkerHandler
        '   AddHandler Worker1.WorkerFolderCount, AddressOf WorkerFolderCountHandler
        'AddHandler Worker1.WorkerFileCount, AddressOf WorkerFileCountHandler
        'AddHandler Worker1.WorkerMessageExtracted, AddressOf WorkerMessageExtractedHandler
        'AddHandler Worker1.WorkerErrorEncountered, AddressOf WorkerErrorEncounteredHandler
    End Sub

    Public Sub New(ByVal splash As Splash_Screen, Optional ByVal currentuser As String = "Unknown User")
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Current_User.Text = currentuser.ToUpper

        splash_loader = splash
        Worker1 = New Worker
        AddHandler Worker1.WorkerComplete, AddressOf WorkerHandler
        'AddHandler Worker1.WorkerFolderCount, AddressOf WorkerFolderCountHandler
        ' AddHandler Worker1.WorkerFileCount, AddressOf WorkerFileCountHandler
        ' AddHandler Worker1.WorkerMessageExtracted, AddressOf WorkerMessageExtractedHandler
        AddHandler Worker1.WorkerErrorEncountered, AddressOf WorkerErrorEncounteredHandler
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents Current_User As System.Windows.Forms.Label
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalMonthCalls As System.Windows.Forms.Label
    Friend WithEvents lblTotalYearCalls As System.Windows.Forms.Label
    Friend WithEvents TotalYearCalls As System.Windows.Forms.Label
    Friend WithEvents TotalMonthCalls As System.Windows.Forms.Label
    Friend WithEvents lstCustomerType As MTGCComboBox
    Friend WithEvents lstCustomerTypeID As System.Windows.Forms.ComboBox
    Friend WithEvents lstCustomerTypePDText As System.Windows.Forms.ComboBox
    Friend WithEvents PersonalDetailsDescriptor As System.Windows.Forms.Label
    Friend WithEvents lstCustomerTypeCover As System.Windows.Forms.Label
    Friend WithEvents lstProblemTypeCover As System.Windows.Forms.Label
    Friend WithEvents lstProblemType As MTGCComboBox
    Friend WithEvents lstProblemSubTypeCover As System.Windows.Forms.Label
    Friend WithEvents lstProblemSubType As MTGCComboBox
    Friend WithEvents lstProblemTypeID As System.Windows.Forms.ComboBox
    Friend WithEvents lstProblemSubTypeID As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblStats As System.Windows.Forms.Label
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar As System.Windows.Forms.Label
    Friend WithEvents ConnectionTester As System.Windows.Forms.Timer
    Friend WithEvents ServerStatus As System.Windows.Forms.Label
    Friend WithEvents ConnectionTesterCounter As System.Windows.Forms.Label
    Friend WithEvents lstCallList As System.Windows.Forms.ListBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnAllCalls As System.Windows.Forms.Button
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents ArrowImageList As System.Windows.Forms.ImageList
    Friend WithEvents displayrecordcount As System.Windows.Forms.Label
    Friend WithEvents lblSegment As System.Windows.Forms.Label
    Friend WithEvents lblSegmentNext As System.Windows.Forms.Label
    Friend WithEvents lblSegmentPrevious As System.Windows.Forms.Label
    Friend WithEvents btnAllOpenCalls As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateUser As System.Windows.Forms.Button
    Friend WithEvents btnRemoveUser As System.Windows.Forms.Button
    Friend WithEvents btnCreateUser As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents databaselayout As System.Windows.Forms.RichTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUserPersonalDetailsIDDescriptor As System.Windows.Forms.TextBox
    Friend WithEvents txtUserDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtSQLstatement As System.Windows.Forms.TextBox
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents txtCallDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtCallSubDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnCreateCallSub As System.Windows.Forms.Button
    Friend WithEvents btnRemoveCallSub As System.Windows.Forms.Button
    Friend WithEvents btnUpdateCallSub As System.Windows.Forms.Button
    Friend WithEvents btnCreateCall As System.Windows.Forms.Button
    Friend WithEvents btnRemoveCall As System.Windows.Forms.Button
    Friend WithEvents btnUpdateCall As System.Windows.Forms.Button
    Friend WithEvents ContextMenu2 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Main_Screen))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Label8 = New System.Windows.Forms.Label
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label9 = New System.Windows.Forms.Label
        Me.ServerStatus = New System.Windows.Forms.Label
        Me.ConnectionTesterCounter = New System.Windows.Forms.Label
        Me.lblSegmentPrevious = New System.Windows.Forms.Label
        Me.lblSegmentNext = New System.Windows.Forms.Label
        Me.lblSegment = New System.Windows.Forms.Label
        Me.displayrecordcount = New System.Windows.Forms.Label
        Me.lstCallList = New System.Windows.Forms.ListBox
        Me.ContextMenu2 = New System.Windows.Forms.ContextMenu
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.btnAllOpenCalls = New System.Windows.Forms.Button
        Me.btnAllCalls = New System.Windows.Forms.Button
        Me.Current_User = New System.Windows.Forms.Label
        Me.StatusBar = New System.Windows.Forms.Label
        Me.btnUpdateUser = New System.Windows.Forms.Button
        Me.btnRemoveUser = New System.Windows.Forms.Button
        Me.btnCreateUser = New System.Windows.Forms.Button
        Me.btnCreateCallSub = New System.Windows.Forms.Button
        Me.btnRemoveCallSub = New System.Windows.Forms.Button
        Me.btnUpdateCallSub = New System.Windows.Forms.Button
        Me.btnCreateCall = New System.Windows.Forms.Button
        Me.btnRemoveCall = New System.Windows.Forms.Button
        Me.btnUpdateCall = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.databaselayout = New System.Windows.Forms.RichTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox5 = New System.Windows.Forms.PictureBox
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSQLstatement = New System.Windows.Forms.TextBox
        Me.PictureBox7 = New System.Windows.Forms.PictureBox
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblStats = New System.Windows.Forms.Label
        Me.TotalYearCalls = New System.Windows.Forms.Label
        Me.TotalMonthCalls = New System.Windows.Forms.Label
        Me.lblTotalYearCalls = New System.Windows.Forms.Label
        Me.lblTotalMonthCalls = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtCallSubDescription = New System.Windows.Forms.TextBox
        Me.txtCallDescription = New System.Windows.Forms.TextBox
        Me.lstProblemSubTypeCover = New System.Windows.Forms.Label
        Me.lstProblemSubType = New MTGCComboBox
        Me.lstProblemTypeCover = New System.Windows.Forms.Label
        Me.lstProblemType = New MTGCComboBox
        Me.lstProblemTypeID = New System.Windows.Forms.ComboBox
        Me.lstProblemSubTypeID = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lstCustomerTypeCover = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtUserPersonalDetailsIDDescriptor = New System.Windows.Forms.TextBox
        Me.PersonalDetailsDescriptor = New System.Windows.Forms.Label
        Me.txtUserDescription = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lstCustomerType = New MTGCComboBox
        Me.lstCustomerTypeID = New System.Windows.Forms.ComboBox
        Me.lstCustomerTypePDText = New System.Windows.Forms.ComboBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.Label2 = New System.Windows.Forms.Label
        Me.ConnectionTester = New System.Windows.Forms.Timer(Me.components)
        Me.ArrowImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DimGray
        Me.Label8.Location = New System.Drawing.Point(688, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(152, 16)
        Me.Label8.TabIndex = 33
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label8, "Current System Time")
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenu = Me.ContextMenu1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Resting..."
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Display Main Screen"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "Exit Application"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DimGray
        Me.Label9.Location = New System.Drawing.Point(816, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 16)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "BUILD 20051121.1"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label9, "Application Build Version")
        '
        'ServerStatus
        '
        Me.ServerStatus.BackColor = System.Drawing.Color.Transparent
        Me.ServerStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServerStatus.ForeColor = System.Drawing.Color.DimGray
        Me.ServerStatus.Location = New System.Drawing.Point(688, 16)
        Me.ServerStatus.Name = "ServerStatus"
        Me.ServerStatus.Size = New System.Drawing.Size(224, 16)
        Me.ServerStatus.TabIndex = 72
        Me.ServerStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.ServerStatus, "SQL Server Status")
        '
        'ConnectionTesterCounter
        '
        Me.ConnectionTesterCounter.BackColor = System.Drawing.Color.Transparent
        Me.ConnectionTesterCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConnectionTesterCounter.ForeColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.ConnectionTesterCounter.Location = New System.Drawing.Point(911, 16)
        Me.ConnectionTesterCounter.Name = "ConnectionTesterCounter"
        Me.ConnectionTesterCounter.Size = New System.Drawing.Size(14, 16)
        Me.ConnectionTesterCounter.TabIndex = 73
        Me.ConnectionTesterCounter.Text = "30"
        Me.ConnectionTesterCounter.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.ConnectionTesterCounter, "Countdown to next Server Status Check")
        '
        'lblSegmentPrevious
        '
        Me.lblSegmentPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSegmentPrevious.Location = New System.Drawing.Point(897, 24)
        Me.lblSegmentPrevious.Name = "lblSegmentPrevious"
        Me.lblSegmentPrevious.Size = New System.Drawing.Size(24, 16)
        Me.lblSegmentPrevious.TabIndex = 91
        Me.lblSegmentPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblSegmentPrevious, "Previous Page Number")
        '
        'lblSegmentNext
        '
        Me.lblSegmentNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSegmentNext.Location = New System.Drawing.Point(897, 480)
        Me.lblSegmentNext.Name = "lblSegmentNext"
        Me.lblSegmentNext.Size = New System.Drawing.Size(24, 16)
        Me.lblSegmentNext.TabIndex = 90
        Me.lblSegmentNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblSegmentNext, "Next Page Number")
        '
        'lblSegment
        '
        Me.lblSegment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSegment.Location = New System.Drawing.Point(897, 248)
        Me.lblSegment.Name = "lblSegment"
        Me.lblSegment.Size = New System.Drawing.Size(24, 16)
        Me.lblSegment.TabIndex = 89
        Me.lblSegment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblSegment, "Current Page Number")
        '
        'displayrecordcount
        '
        Me.displayrecordcount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.displayrecordcount.Location = New System.Drawing.Point(808, 504)
        Me.displayrecordcount.Name = "displayrecordcount"
        Me.displayrecordcount.Size = New System.Drawing.Size(88, 16)
        Me.displayrecordcount.TabIndex = 88
        Me.displayrecordcount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.displayrecordcount, "Indicates how many logs are currently being displayed of the total found from the" & _
        " List Request")
        '
        'lstCallList
        '
        Me.lstCallList.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstCallList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstCallList.ContextMenu = Me.ContextMenu2
        Me.lstCallList.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstCallList.Location = New System.Drawing.Point(808, 33)
        Me.lstCallList.Name = "lstCallList"
        Me.lstCallList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstCallList.Size = New System.Drawing.Size(88, 455)
        Me.lstCallList.TabIndex = 84
        Me.lstCallList.TabStop = False
        Me.ToolTip1.SetToolTip(Me.lstCallList, "Recorded Activity Logs")
        '
        'ContextMenu2
        '
        Me.ContextMenu2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem6})
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 0
        Me.MenuItem6.Text = "Delete Calls"
        '
        'btnAllOpenCalls
        '
        Me.btnAllOpenCalls.BackColor = System.Drawing.Color.AliceBlue
        Me.btnAllOpenCalls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllOpenCalls.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllOpenCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnAllOpenCalls.Location = New System.Drawing.Point(96, 72)
        Me.btnAllOpenCalls.Name = "btnAllOpenCalls"
        Me.btnAllOpenCalls.Size = New System.Drawing.Size(80, 18)
        Me.btnAllOpenCalls.TabIndex = 87
        Me.btnAllOpenCalls.TabStop = False
        Me.btnAllOpenCalls.Text = "OPEN CALLS"
        Me.ToolTip1.SetToolTip(Me.btnAllOpenCalls, "Show all unresolved calls logged on the system")
        '
        'btnAllCalls
        '
        Me.btnAllCalls.BackColor = System.Drawing.Color.AliceBlue
        Me.btnAllCalls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllCalls.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnAllCalls.Location = New System.Drawing.Point(8, 72)
        Me.btnAllCalls.Name = "btnAllCalls"
        Me.btnAllCalls.Size = New System.Drawing.Size(80, 18)
        Me.btnAllCalls.TabIndex = 85
        Me.btnAllCalls.TabStop = False
        Me.btnAllCalls.Text = "ALL CALLS"
        Me.ToolTip1.SetToolTip(Me.btnAllCalls, "Show all calls logged on the system")
        '
        'Current_User
        '
        Me.Current_User.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_User.Location = New System.Drawing.Point(80, 8)
        Me.Current_User.Name = "Current_User"
        Me.Current_User.Size = New System.Drawing.Size(192, 16)
        Me.Current_User.TabIndex = 69
        Me.Current_User.Text = "Unknown User"
        Me.ToolTip1.SetToolTip(Me.Current_User, "User currently logged in")
        '
        'StatusBar
        '
        Me.StatusBar.BackColor = System.Drawing.Color.White
        Me.StatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.ForeColor = System.Drawing.Color.LimeGreen
        Me.StatusBar.Location = New System.Drawing.Point(16, 568)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(624, 16)
        Me.StatusBar.TabIndex = 71
        Me.StatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.StatusBar, "Status Bar")
        '
        'btnUpdateUser
        '
        Me.btnUpdateUser.BackColor = System.Drawing.Color.AliceBlue
        Me.btnUpdateUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateUser.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnUpdateUser.Location = New System.Drawing.Point(160, 96)
        Me.btnUpdateUser.Name = "btnUpdateUser"
        Me.btnUpdateUser.Size = New System.Drawing.Size(64, 16)
        Me.btnUpdateUser.TabIndex = 83
        Me.btnUpdateUser.Text = "UPDATE"
        Me.ToolTip1.SetToolTip(Me.btnUpdateUser, "Update the [User_Type] Table")
        '
        'btnRemoveUser
        '
        Me.btnRemoveUser.BackColor = System.Drawing.Color.AliceBlue
        Me.btnRemoveUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveUser.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnRemoveUser.Location = New System.Drawing.Point(240, 96)
        Me.btnRemoveUser.Name = "btnRemoveUser"
        Me.btnRemoveUser.Size = New System.Drawing.Size(64, 16)
        Me.btnRemoveUser.TabIndex = 84
        Me.btnRemoveUser.Text = "REMOVE"
        Me.ToolTip1.SetToolTip(Me.btnRemoveUser, "Update the [User_Type] Table")
        '
        'btnCreateUser
        '
        Me.btnCreateUser.BackColor = System.Drawing.Color.AliceBlue
        Me.btnCreateUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateUser.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnCreateUser.Location = New System.Drawing.Point(320, 96)
        Me.btnCreateUser.Name = "btnCreateUser"
        Me.btnCreateUser.Size = New System.Drawing.Size(64, 16)
        Me.btnCreateUser.TabIndex = 85
        Me.btnCreateUser.Text = "CREATE"
        Me.ToolTip1.SetToolTip(Me.btnCreateUser, "Update the [User_Type] Table")
        '
        'btnCreateCallSub
        '
        Me.btnCreateCallSub.BackColor = System.Drawing.Color.AliceBlue
        Me.btnCreateCallSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateCallSub.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateCallSub.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnCreateCallSub.Location = New System.Drawing.Point(320, 152)
        Me.btnCreateCallSub.Name = "btnCreateCallSub"
        Me.btnCreateCallSub.Size = New System.Drawing.Size(64, 16)
        Me.btnCreateCallSub.TabIndex = 131
        Me.btnCreateCallSub.Text = "CREATE"
        Me.ToolTip1.SetToolTip(Me.btnCreateCallSub, "Update the [Call_Sub_Type] Table")
        '
        'btnRemoveCallSub
        '
        Me.btnRemoveCallSub.BackColor = System.Drawing.Color.AliceBlue
        Me.btnRemoveCallSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveCallSub.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveCallSub.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnRemoveCallSub.Location = New System.Drawing.Point(240, 152)
        Me.btnRemoveCallSub.Name = "btnRemoveCallSub"
        Me.btnRemoveCallSub.Size = New System.Drawing.Size(64, 16)
        Me.btnRemoveCallSub.TabIndex = 130
        Me.btnRemoveCallSub.Text = "REMOVE"
        Me.ToolTip1.SetToolTip(Me.btnRemoveCallSub, "Update the [Call_Sub_Type] Table")
        '
        'btnUpdateCallSub
        '
        Me.btnUpdateCallSub.BackColor = System.Drawing.Color.AliceBlue
        Me.btnUpdateCallSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateCallSub.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateCallSub.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnUpdateCallSub.Location = New System.Drawing.Point(160, 152)
        Me.btnUpdateCallSub.Name = "btnUpdateCallSub"
        Me.btnUpdateCallSub.Size = New System.Drawing.Size(64, 16)
        Me.btnUpdateCallSub.TabIndex = 129
        Me.btnUpdateCallSub.Text = "UPDATE"
        Me.ToolTip1.SetToolTip(Me.btnUpdateCallSub, "Update the [Call_Sub_Type] Table")
        '
        'btnCreateCall
        '
        Me.btnCreateCall.BackColor = System.Drawing.Color.AliceBlue
        Me.btnCreateCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateCall.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateCall.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnCreateCall.Location = New System.Drawing.Point(320, 72)
        Me.btnCreateCall.Name = "btnCreateCall"
        Me.btnCreateCall.Size = New System.Drawing.Size(64, 16)
        Me.btnCreateCall.TabIndex = 134
        Me.btnCreateCall.Text = "CREATE"
        Me.ToolTip1.SetToolTip(Me.btnCreateCall, "Update the [Call_Type] Table")
        '
        'btnRemoveCall
        '
        Me.btnRemoveCall.BackColor = System.Drawing.Color.AliceBlue
        Me.btnRemoveCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveCall.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveCall.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnRemoveCall.Location = New System.Drawing.Point(240, 72)
        Me.btnRemoveCall.Name = "btnRemoveCall"
        Me.btnRemoveCall.Size = New System.Drawing.Size(64, 16)
        Me.btnRemoveCall.TabIndex = 133
        Me.btnRemoveCall.Text = "REMOVE"
        Me.ToolTip1.SetToolTip(Me.btnRemoveCall, "Update the [Call_Type] Table")
        '
        'btnUpdateCall
        '
        Me.btnUpdateCall.BackColor = System.Drawing.Color.AliceBlue
        Me.btnUpdateCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateCall.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateCall.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnUpdateCall.Location = New System.Drawing.Point(160, 72)
        Me.btnUpdateCall.Name = "btnUpdateCall"
        Me.btnUpdateCall.Size = New System.Drawing.Size(64, 16)
        Me.btnUpdateCall.TabIndex = 132
        Me.btnUpdateCall.Text = "UPDATE"
        Me.ToolTip1.SetToolTip(Me.btnUpdateCall, "Update the [Call_Type] Table")
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.AliceBlue
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.SteelBlue
        Me.Button2.Location = New System.Drawing.Point(344, 80)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(64, 20)
        Me.Button2.TabIndex = 131
        Me.Button2.Text = "EXECUTE"
        Me.ToolTip1.SetToolTip(Me.Button2, "Update the [User_Type] Table")
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.AliceBlue
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.ForeColor = System.Drawing.Color.SteelBlue
        Me.Button8.Location = New System.Drawing.Point(232, 8)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(104, 20)
        Me.Button8.TabIndex = 132
        Me.Button8.Text = "CLEAR DATABASE"
        Me.ToolTip1.SetToolTip(Me.Button8, "Clear all Records from the database")
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label6.Location = New System.Drawing.Point(8, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(224, 16)
        Me.Label6.TabIndex = 95
        Me.Label6.Text = "Database Design"
        Me.ToolTip1.SetToolTip(Me.Label6, "Database Layout for Quick Reference when Compiling SQL commands")
        '
        'databaselayout
        '
        Me.databaselayout.BackColor = System.Drawing.SystemColors.ControlLight
        Me.databaselayout.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.databaselayout.ForeColor = System.Drawing.Color.SteelBlue
        Me.databaselayout.Location = New System.Drawing.Point(10, 32)
        Me.databaselayout.Name = "databaselayout"
        Me.databaselayout.ReadOnly = True
        Me.databaselayout.Size = New System.Drawing.Size(304, 296)
        Me.databaselayout.TabIndex = 94
        Me.databaselayout.TabStop = False
        Me.databaselayout.Text = ""
        Me.ToolTip1.SetToolTip(Me.databaselayout, "Database Layout for Quick Reference when Compiling SQL commands")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.LimeGreen
        Me.Label1.Location = New System.Drawing.Point(864, 568)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 16)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "Waiting"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox5
        '
        Me.PictureBox5.BackgroundImage = CType(resources.GetObject("PictureBox5.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(840, 568)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox5.TabIndex = 66
        Me.PictureBox5.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(824, 568)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox4.TabIndex = 65
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(808, 568)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox3.TabIndex = 64
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(792, 568)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 63
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(776, 568)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 62
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SteelBlue
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.lblSegmentPrevious)
        Me.Panel1.Controls.Add(Me.lblSegmentNext)
        Me.Panel1.Controls.Add(Me.lblSegment)
        Me.Panel1.Controls.Add(Me.displayrecordcount)
        Me.Panel1.Controls.Add(Me.PictureBox7)
        Me.Panel1.Controls.Add(Me.PictureBox6)
        Me.Panel1.Controls.Add(Me.lstCallList)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(0, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(968, 528)
        Me.Panel1.TabIndex = 68
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.databaselayout)
        Me.Panel4.Location = New System.Drawing.Point(465, 152)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(328, 344)
        Me.Panel4.TabIndex = 94
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button2)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txtSQLstatement)
        Me.GroupBox3.Location = New System.Drawing.Point(32, 384)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(424, 112)
        Me.GroupBox3.TabIndex = 92
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Non-Query SQL Executor"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.SteelBlue
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.AliceBlue
        Me.Label3.Location = New System.Drawing.Point(16, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(392, 48)
        Me.Label3.TabIndex = 130
        Me.Label3.Text = "To quickly make sql statement based changes to the database, use the function bel" & _
        "ow. Please note that this should only be used by expert users with a sound knowl" & _
        "edge of SQL. Database corruptions cannot be recovered from."
        '
        'txtSQLstatement
        '
        Me.txtSQLstatement.BackColor = System.Drawing.Color.AliceBlue
        Me.txtSQLstatement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSQLstatement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSQLstatement.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtSQLstatement.Location = New System.Drawing.Point(16, 80)
        Me.txtSQLstatement.Name = "txtSQLstatement"
        Me.txtSQLstatement.Size = New System.Drawing.Size(320, 20)
        Me.txtSQLstatement.TabIndex = 129
        Me.txtSQLstatement.Text = ""
        '
        'PictureBox7
        '
        Me.PictureBox7.BackColor = System.Drawing.Color.SteelBlue
        Me.PictureBox7.Location = New System.Drawing.Point(897, 448)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox7.TabIndex = 87
        Me.PictureBox7.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.SteelBlue
        Me.PictureBox6.Location = New System.Drawing.Point(897, 40)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox6.TabIndex = 86
        Me.PictureBox6.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Button8)
        Me.Panel2.Controls.Add(Me.btnAllOpenCalls)
        Me.Panel2.Controls.Add(Me.btnAllCalls)
        Me.Panel2.Controls.Add(Me.lblStats)
        Me.Panel2.Controls.Add(Me.TotalYearCalls)
        Me.Panel2.Controls.Add(Me.TotalMonthCalls)
        Me.Panel2.Controls.Add(Me.lblTotalYearCalls)
        Me.Panel2.Controls.Add(Me.lblTotalMonthCalls)
        Me.Panel2.Location = New System.Drawing.Point(465, 23)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(432, 114)
        Me.Panel2.TabIndex = 80
        '
        'lblStats
        '
        Me.lblStats.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStats.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblStats.Location = New System.Drawing.Point(8, 8)
        Me.lblStats.Name = "lblStats"
        Me.lblStats.Size = New System.Drawing.Size(224, 16)
        Me.lblStats.TabIndex = 80
        Me.lblStats.Text = "Quick Stats for"
        '
        'TotalYearCalls
        '
        Me.TotalYearCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.TotalYearCalls.Location = New System.Drawing.Point(168, 48)
        Me.TotalYearCalls.Name = "TotalYearCalls"
        Me.TotalYearCalls.Size = New System.Drawing.Size(72, 16)
        Me.TotalYearCalls.TabIndex = 78
        Me.TotalYearCalls.Text = "0"
        '
        'TotalMonthCalls
        '
        Me.TotalMonthCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.TotalMonthCalls.Location = New System.Drawing.Point(168, 32)
        Me.TotalMonthCalls.Name = "TotalMonthCalls"
        Me.TotalMonthCalls.Size = New System.Drawing.Size(72, 16)
        Me.TotalMonthCalls.TabIndex = 79
        Me.TotalMonthCalls.Text = "0"
        '
        'lblTotalYearCalls
        '
        Me.lblTotalYearCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblTotalYearCalls.Location = New System.Drawing.Point(8, 48)
        Me.lblTotalYearCalls.Name = "lblTotalYearCalls"
        Me.lblTotalYearCalls.Size = New System.Drawing.Size(176, 16)
        Me.lblTotalYearCalls.TabIndex = 74
        Me.lblTotalYearCalls.Text = "Total Calls for:"
        '
        'lblTotalMonthCalls
        '
        Me.lblTotalMonthCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblTotalMonthCalls.Location = New System.Drawing.Point(8, 32)
        Me.lblTotalMonthCalls.Name = "lblTotalMonthCalls"
        Me.lblTotalMonthCalls.Size = New System.Drawing.Size(176, 16)
        Me.lblTotalMonthCalls.TabIndex = 75
        Me.lblTotalMonthCalls.Text = "Total Calls for:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.btnCreateCall)
        Me.GroupBox2.Controls.Add(Me.btnRemoveCall)
        Me.GroupBox2.Controls.Add(Me.btnUpdateCall)
        Me.GroupBox2.Controls.Add(Me.btnCreateCallSub)
        Me.GroupBox2.Controls.Add(Me.btnRemoveCallSub)
        Me.GroupBox2.Controls.Add(Me.btnUpdateCallSub)
        Me.GroupBox2.Controls.Add(Me.txtCallSubDescription)
        Me.GroupBox2.Controls.Add(Me.txtCallDescription)
        Me.GroupBox2.Controls.Add(Me.lstProblemSubTypeCover)
        Me.GroupBox2.Controls.Add(Me.lstProblemSubType)
        Me.GroupBox2.Controls.Add(Me.lstProblemTypeCover)
        Me.GroupBox2.Controls.Add(Me.lstProblemType)
        Me.GroupBox2.Controls.Add(Me.lstProblemTypeID)
        Me.GroupBox2.Controls.Add(Me.lstProblemSubTypeID)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.AliceBlue
        Me.GroupBox2.Location = New System.Drawing.Point(32, 200)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(424, 176)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Call Types"
        '
        'txtCallSubDescription
        '
        Me.txtCallSubDescription.BackColor = System.Drawing.Color.AliceBlue
        Me.txtCallSubDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCallSubDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCallSubDescription.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtCallSubDescription.Location = New System.Drawing.Point(144, 128)
        Me.txtCallSubDescription.Name = "txtCallSubDescription"
        Me.txtCallSubDescription.Size = New System.Drawing.Size(256, 20)
        Me.txtCallSubDescription.TabIndex = 128
        Me.txtCallSubDescription.Text = ""
        '
        'txtCallDescription
        '
        Me.txtCallDescription.BackColor = System.Drawing.Color.AliceBlue
        Me.txtCallDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCallDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCallDescription.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtCallDescription.Location = New System.Drawing.Point(144, 48)
        Me.txtCallDescription.Name = "txtCallDescription"
        Me.txtCallDescription.Size = New System.Drawing.Size(256, 20)
        Me.txtCallDescription.TabIndex = 127
        Me.txtCallDescription.Text = ""
        '
        'lstProblemSubTypeCover
        '
        Me.lstProblemSubTypeCover.BackColor = System.Drawing.Color.Ivory
        Me.lstProblemSubTypeCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstProblemSubTypeCover.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProblemSubTypeCover.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstProblemSubTypeCover.Location = New System.Drawing.Point(144, 104)
        Me.lstProblemSubTypeCover.Name = "lstProblemSubTypeCover"
        Me.lstProblemSubTypeCover.Size = New System.Drawing.Size(237, 21)
        Me.lstProblemSubTypeCover.TabIndex = 88
        Me.lstProblemSubTypeCover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstProblemSubType
        '
        Me.lstProblemSubType.BackColor = System.Drawing.Color.Ivory
        Me.lstProblemSubType.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
        Me.lstProblemSubType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.lstProblemSubType.ColumnNum = 1
        Me.lstProblemSubType.ColumnWidth = "121"
        Me.lstProblemSubType.DisplayMember = "Text"
        Me.lstProblemSubType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstProblemSubType.DropDownArrowBackColor = System.Drawing.Color.FromArgb(CType(136, Byte), CType(169, Byte), CType(223, Byte))
        Me.lstProblemSubType.DropDownBackColor = System.Drawing.Color.FromArgb(CType(193, Byte), CType(210, Byte), CType(238, Byte))
        Me.lstProblemSubType.DropDownForeColor = System.Drawing.Color.Black
        Me.lstProblemSubType.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.lstProblemSubType.DropDownWidth = 141
        Me.lstProblemSubType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProblemSubType.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstProblemSubType.GridLineColor = System.Drawing.Color.Transparent
        Me.lstProblemSubType.GridLineHorizontal = False
        Me.lstProblemSubType.GridLineVertical = False
        Me.lstProblemSubType.HighlightBorderColor = System.Drawing.Color.Black
        Me.lstProblemSubType.HighlightBorderOnMouseEvents = True
        Me.lstProblemSubType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.lstProblemSubType.Location = New System.Drawing.Point(144, 104)
        Me.lstProblemSubType.ManagingFastMouseMoving = True
        Me.lstProblemSubType.ManagingFastMouseMovingInterval = 30
        Me.lstProblemSubType.Name = "lstProblemSubType"
        Me.lstProblemSubType.NormalBorderColor = System.Drawing.Color.Black
        Me.lstProblemSubType.Size = New System.Drawing.Size(256, 21)
        Me.lstProblemSubType.TabIndex = 86
        Me.lstProblemSubType.TabStop = False
        '
        'lstProblemTypeCover
        '
        Me.lstProblemTypeCover.BackColor = System.Drawing.Color.LightYellow
        Me.lstProblemTypeCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstProblemTypeCover.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProblemTypeCover.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstProblemTypeCover.Location = New System.Drawing.Point(144, 24)
        Me.lstProblemTypeCover.Name = "lstProblemTypeCover"
        Me.lstProblemTypeCover.Size = New System.Drawing.Size(237, 21)
        Me.lstProblemTypeCover.TabIndex = 85
        Me.lstProblemTypeCover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstProblemType
        '
        Me.lstProblemType.BackColor = System.Drawing.Color.Ivory
        Me.lstProblemType.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
        Me.lstProblemType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.lstProblemType.ColumnNum = 1
        Me.lstProblemType.ColumnWidth = "121"
        Me.lstProblemType.DisplayMember = "Text"
        Me.lstProblemType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstProblemType.DropDownArrowBackColor = System.Drawing.Color.FromArgb(CType(136, Byte), CType(169, Byte), CType(223, Byte))
        Me.lstProblemType.DropDownBackColor = System.Drawing.Color.FromArgb(CType(193, Byte), CType(210, Byte), CType(238, Byte))
        Me.lstProblemType.DropDownForeColor = System.Drawing.Color.Black
        Me.lstProblemType.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.lstProblemType.DropDownWidth = 141
        Me.lstProblemType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProblemType.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstProblemType.GridLineColor = System.Drawing.Color.Transparent
        Me.lstProblemType.GridLineHorizontal = False
        Me.lstProblemType.GridLineVertical = False
        Me.lstProblemType.HighlightBorderColor = System.Drawing.Color.Black
        Me.lstProblemType.HighlightBorderOnMouseEvents = True
        Me.lstProblemType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.lstProblemType.Location = New System.Drawing.Point(144, 24)
        Me.lstProblemType.ManagingFastMouseMoving = True
        Me.lstProblemType.ManagingFastMouseMovingInterval = 30
        Me.lstProblemType.Name = "lstProblemType"
        Me.lstProblemType.NormalBorderColor = System.Drawing.Color.Black
        Me.lstProblemType.Size = New System.Drawing.Size(256, 21)
        Me.lstProblemType.TabIndex = 83
        Me.lstProblemType.TabStop = False
        '
        'lstProblemTypeID
        '
        Me.lstProblemTypeID.Location = New System.Drawing.Point(144, 24)
        Me.lstProblemTypeID.Name = "lstProblemTypeID"
        Me.lstProblemTypeID.Size = New System.Drawing.Size(8, 21)
        Me.lstProblemTypeID.TabIndex = 89
        Me.lstProblemTypeID.Text = "ComboBox1"
        Me.lstProblemTypeID.Visible = False
        '
        'lstProblemSubTypeID
        '
        Me.lstProblemSubTypeID.Location = New System.Drawing.Point(144, 104)
        Me.lstProblemSubTypeID.Name = "lstProblemSubTypeID"
        Me.lstProblemSubTypeID.Size = New System.Drawing.Size(8, 21)
        Me.lstProblemSubTypeID.TabIndex = 90
        Me.lstProblemSubTypeID.Text = "ComboBox1"
        Me.lstProblemSubTypeID.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnCreateUser)
        Me.GroupBox1.Controls.Add(Me.btnRemoveUser)
        Me.GroupBox1.Controls.Add(Me.btnUpdateUser)
        Me.GroupBox1.Controls.Add(Me.lstCustomerTypeCover)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtUserPersonalDetailsIDDescriptor)
        Me.GroupBox1.Controls.Add(Me.PersonalDetailsDescriptor)
        Me.GroupBox1.Controls.Add(Me.txtUserDescription)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lstCustomerType)
        Me.GroupBox1.Controls.Add(Me.lstCustomerTypeID)
        Me.GroupBox1.Controls.Add(Me.lstCustomerTypePDText)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.AliceBlue
        Me.GroupBox1.Location = New System.Drawing.Point(32, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(424, 120)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Customer Types"
        '
        'lstCustomerTypeCover
        '
        Me.lstCustomerTypeCover.BackColor = System.Drawing.Color.LightYellow
        Me.lstCustomerTypeCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstCustomerTypeCover.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCustomerTypeCover.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstCustomerTypeCover.Location = New System.Drawing.Point(144, 24)
        Me.lstCustomerTypeCover.Name = "lstCustomerTypeCover"
        Me.lstCustomerTypeCover.Size = New System.Drawing.Size(237, 21)
        Me.lstCustomerTypeCover.TabIndex = 1
        Me.lstCustomerTypeCover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 16)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Details Descriptor:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUserPersonalDetailsIDDescriptor
        '
        Me.txtUserPersonalDetailsIDDescriptor.BackColor = System.Drawing.Color.AliceBlue
        Me.txtUserPersonalDetailsIDDescriptor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserPersonalDetailsIDDescriptor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserPersonalDetailsIDDescriptor.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtUserPersonalDetailsIDDescriptor.Location = New System.Drawing.Point(144, 72)
        Me.txtUserPersonalDetailsIDDescriptor.Name = "txtUserPersonalDetailsIDDescriptor"
        Me.txtUserPersonalDetailsIDDescriptor.Size = New System.Drawing.Size(256, 20)
        Me.txtUserPersonalDetailsIDDescriptor.TabIndex = 6
        Me.txtUserPersonalDetailsIDDescriptor.Text = ""
        '
        'PersonalDetailsDescriptor
        '
        Me.PersonalDetailsDescriptor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PersonalDetailsDescriptor.Location = New System.Drawing.Point(16, 48)
        Me.PersonalDetailsDescriptor.Name = "PersonalDetailsDescriptor"
        Me.PersonalDetailsDescriptor.Size = New System.Drawing.Size(128, 16)
        Me.PersonalDetailsDescriptor.TabIndex = 5
        Me.PersonalDetailsDescriptor.Text = "Display Text:"
        Me.PersonalDetailsDescriptor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUserDescription
        '
        Me.txtUserDescription.BackColor = System.Drawing.Color.AliceBlue
        Me.txtUserDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserDescription.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtUserDescription.Location = New System.Drawing.Point(144, 48)
        Me.txtUserDescription.Name = "txtUserDescription"
        Me.txtUserDescription.Size = New System.Drawing.Size(256, 20)
        Me.txtUserDescription.TabIndex = 4
        Me.txtUserDescription.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.LightYellow
        Me.Label4.Location = New System.Drawing.Point(16, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Existing Customer Types:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstCustomerType
        '
        Me.lstCustomerType.BackColor = System.Drawing.Color.Ivory
        Me.lstCustomerType.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
        Me.lstCustomerType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.lstCustomerType.ColumnNum = 1
        Me.lstCustomerType.ColumnWidth = "121"
        Me.lstCustomerType.DisplayMember = "Text"
        Me.lstCustomerType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstCustomerType.DropDownArrowBackColor = System.Drawing.Color.FromArgb(CType(136, Byte), CType(169, Byte), CType(223, Byte))
        Me.lstCustomerType.DropDownBackColor = System.Drawing.Color.FromArgb(CType(193, Byte), CType(210, Byte), CType(238, Byte))
        Me.lstCustomerType.DropDownForeColor = System.Drawing.Color.Black
        Me.lstCustomerType.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.lstCustomerType.DropDownWidth = 141
        Me.lstCustomerType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCustomerType.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstCustomerType.GridLineColor = System.Drawing.Color.Transparent
        Me.lstCustomerType.GridLineHorizontal = False
        Me.lstCustomerType.GridLineVertical = False
        Me.lstCustomerType.HighlightBorderColor = System.Drawing.Color.Black
        Me.lstCustomerType.HighlightBorderOnMouseEvents = True
        Me.lstCustomerType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.lstCustomerType.Location = New System.Drawing.Point(144, 24)
        Me.lstCustomerType.ManagingFastMouseMoving = True
        Me.lstCustomerType.ManagingFastMouseMovingInterval = 30
        Me.lstCustomerType.Name = "lstCustomerType"
        Me.lstCustomerType.NormalBorderColor = System.Drawing.Color.Black
        Me.lstCustomerType.Size = New System.Drawing.Size(256, 21)
        Me.lstCustomerType.TabIndex = 4
        Me.lstCustomerType.TabStop = False
        '
        'lstCustomerTypeID
        '
        Me.lstCustomerTypeID.Location = New System.Drawing.Point(144, 24)
        Me.lstCustomerTypeID.Name = "lstCustomerTypeID"
        Me.lstCustomerTypeID.Size = New System.Drawing.Size(40, 21)
        Me.lstCustomerTypeID.TabIndex = 80
        Me.lstCustomerTypeID.Text = "ComboBox1"
        Me.lstCustomerTypeID.Visible = False
        '
        'lstCustomerTypePDText
        '
        Me.lstCustomerTypePDText.Location = New System.Drawing.Point(200, 24)
        Me.lstCustomerTypePDText.Name = "lstCustomerTypePDText"
        Me.lstCustomerTypePDText.Size = New System.Drawing.Size(40, 21)
        Me.lstCustomerTypePDText.TabIndex = 81
        Me.lstCustomerTypePDText.Text = "ComboBox1"
        Me.lstCustomerTypePDText.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(807, 23)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(90, 474)
        Me.Panel3.TabIndex = 85
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem4})
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 0
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem5, Me.MenuItem9})
        Me.MenuItem4.Text = "Tasks"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 0
        Me.MenuItem5.Text = "Log Off"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 1
        Me.MenuItem9.Text = "Exit"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "Current User:"
        '
        'ConnectionTester
        '
        Me.ConnectionTester.Enabled = True
        Me.ConnectionTester.Interval = 10000
        '
        'ArrowImageList
        '
        Me.ArrowImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ArrowImageList.ImageSize = New System.Drawing.Size(25, 25)
        Me.ArrowImageList.ImageStream = CType(resources.GetObject("ArrowImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ArrowImageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.LightYellow
        Me.Label7.Location = New System.Drawing.Point(16, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(136, 16)
        Me.Label7.TabIndex = 135
        Me.Label7.Text = "Existing Problem Types:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Ivory
        Me.Label10.Location = New System.Drawing.Point(16, 104)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(136, 16)
        Me.Label10.TabIndex = 136
        Me.Label10.Text = "Existing Sub Types:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 16)
        Me.Label11.TabIndex = 137
        Me.Label11.Text = "Display Text:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(16, 128)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 16)
        Me.Label12.TabIndex = 138
        Me.Label12.Text = "Display Text:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.SteelBlue
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.AliceBlue
        Me.Label13.Location = New System.Drawing.Point(32, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(424, 40)
        Me.Label13.TabIndex = 131
        Me.Label13.Text = "To change the List Box options that appear in Tutor Activity Logger, simply edit " & _
        "the text boxes appearing in light blue and select the desired action button to u" & _
        "pdate the database. Please note that all changes are irreversible."
        '
        'Main_Screen
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(930, 610)
        Me.Controls.Add(Me.ConnectionTesterCounter)
        Me.Controls.Add(Me.ServerStatus)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Current_User)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label9)
        Me.ForeColor = System.Drawing.Color.SteelBlue
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(936, 642)
        Me.Menu = Me.MainMenu1
        Me.MinimumSize = New System.Drawing.Size(936, 642)
        Me.Name = "Main_Screen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tutor Activity Logger Management Module 1.0"
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private current_light As Integer = 0
    Private current_colour As Integer = 0
    Private currently_working As Boolean = False




    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            If ex.Message.IndexOf("Thread was being aborted") < 0 Then
                If error_reporting_level = "minimal" Then
                    Dim Display_Message1 As New Display_Message("The Application encountered the following problem: " & vbCrLf & ex.Message.ToString)
                    Display_Message1.ShowDialog()
                Else
                    Dim Display_Message1 As New Display_Message("The Application encountered the following problem: " & vbCrLf & identifier_msg & ":" & ex.ToString)
                    Display_Message1.ShowDialog()
                End If
                Dim dir As DirectoryInfo = New DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
                If dir.Exists = False Then
                    dir.Create()
                End If
                Dim filewriter As StreamWriter = New StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
                filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ":" & ex.ToString)
                filewriter.Flush()
                filewriter.Close()
            End If
        Catch exc As Exception
            MsgBox("An error occurred in Tutor Activity Logger Management Module's error handling routine. The application will try to recover from this serious error.", MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub

    Private Sub run_green_lights()
        If shutting_down = False Then

            Try
                Label1.ForeColor = Color.LimeGreen
                Label1.Text = "Waiting"


                current_light = current_light - 1
                If current_light < 1 Then
                    current_light = 5
                End If
                current_colour = 0
                Try


                    PictureBox1.Image = ImageList1.Images(1)
                    PictureBox2.Image = ImageList1.Images(1)
                    PictureBox3.Image = ImageList1.Images(1)
                    PictureBox4.Image = ImageList1.Images(1)
                    PictureBox5.Image = ImageList1.Images(1)
                Catch ex As Exception
                End Try


                Select Case current_light
                    Case 0

                        PictureBox1.Image = ImageList1.Images(0)
                    Case 1

                        PictureBox2.Image = ImageList1.Images(0)
                    Case 2

                        PictureBox3.Image = ImageList1.Images(0)
                    Case 3

                        PictureBox4.Image = ImageList1.Images(0)
                    Case 4

                        PictureBox5.Image = ImageList1.Images(0)
                    Case 5

                        PictureBox1.Image = ImageList1.Images(0)
                End Select

                current_light = current_light + 1
                If current_light > 5 Then
                    current_light = 1
                End If
            Catch err As System.InvalidOperationException
                current_light = current_light
            Catch ex As Exception
                Error_Handler(ex, "Running Lights")
            End Try
        End If
    End Sub

    Private Sub run_orange_lights()
        If shutting_down = False Then


            Try
                Label1.ForeColor = Color.DarkOrange
                Label1.Text = "Working"

                current_light = current_light - 1
                If current_light < 1 Then
                    current_light = 5
                End If
                current_colour = 1
                Try


                    PictureBox1.Image = ImageList1.Images(3)
                    PictureBox2.Image = ImageList1.Images(3)
                    PictureBox3.Image = ImageList1.Images(3)
                    PictureBox4.Image = ImageList1.Images(3)
                    PictureBox5.Image = ImageList1.Images(3)
                Catch ex As Exception
                End Try

                Select Case current_light
                    Case 0
                        PictureBox1.Image = ImageList1.Images(2)
                    Case 1
                        PictureBox2.Image = ImageList1.Images(2)
                    Case 2
                        PictureBox3.Image = ImageList1.Images(2)
                    Case 3
                        PictureBox4.Image = ImageList1.Images(2)
                    Case 4
                        PictureBox5.Image = ImageList1.Images(2)
                    Case 5
                        PictureBox1.Image = ImageList1.Images(2)
                End Select

                current_light = current_light + 1
                If current_light > 5 Then
                    current_light = 1
                End If
            Catch err As System.InvalidOperationException

            Catch ex As Exception
                Error_Handler(ex, "Running Lights")
            End Try
        End If
    End Sub

    Private Sub run_lights()
        If shutting_down = False Then


            Try
                If current_colour = 1 Then
                    Select Case current_light
                        Case 0
                            PictureBox5.Image = ImageList1.Images(3)
                            PictureBox1.Image = ImageList1.Images(2)
                        Case 1
                            PictureBox1.Image = ImageList1.Images(3)
                            PictureBox2.Image = ImageList1.Images(2)
                        Case 2
                            PictureBox2.Image = ImageList1.Images(3)
                            PictureBox3.Image = ImageList1.Images(2)
                        Case 3
                            PictureBox3.Image = ImageList1.Images(3)
                            PictureBox4.Image = ImageList1.Images(2)
                        Case 4
                            PictureBox4.Image = ImageList1.Images(3)
                            PictureBox5.Image = ImageList1.Images(2)
                        Case 5
                            PictureBox5.Image = ImageList1.Images(3)
                            PictureBox1.Image = ImageList1.Images(2)
                    End Select
                Else
                    Select Case current_light
                        Case 0
                            PictureBox5.Image = ImageList1.Images(1)
                            PictureBox1.Image = ImageList1.Images(0)
                        Case 1
                            PictureBox1.Image = ImageList1.Images(1)
                            PictureBox2.Image = ImageList1.Images(0)
                        Case 2
                            PictureBox2.Image = ImageList1.Images(1)
                            PictureBox3.Image = ImageList1.Images(0)
                        Case 3
                            PictureBox3.Image = ImageList1.Images(1)
                            PictureBox4.Image = ImageList1.Images(0)
                        Case 4
                            PictureBox4.Image = ImageList1.Images(1)
                            PictureBox5.Image = ImageList1.Images(0)
                        Case 5
                            PictureBox5.Image = ImageList1.Images(1)
                            PictureBox1.Image = ImageList1.Images(0)
                    End Select

                End If

                current_light = current_light + 1
                If current_light > 5 Then
                    current_light = 1
                End If
            Catch err As System.InvalidOperationException

            Catch ex As Exception
                Error_Handler(ex, "Running Lights")
            End Try
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            run_lights()
            Label8.Text = Format(Now(), "dd/MM/yyyy HH:mm:ss")

            ConnectionTesterCounter.ForeColor = ServerStatus.ForeColor
            If testingconnection = False Then


                ConnectionTesterCounter.Text = (CInt(ConnectionTesterCounter.Text) - 1).ToString
                If ConnectionTesterCounter.Text.Length < 2 Then
                    ConnectionTesterCounter.Text = "0" & ConnectionTesterCounter.Text
                End If

                If CInt(ConnectionTesterCounter.Text) = -1 Then
                    ConnectionTesterCounter.Text = 10
                End If

            End If

            If application_exit = True Then
                Me.Close()
            End If
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Private Sub Main_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            load_databaselayout()
            PictureBox6.Image = ArrowImageList.Images(0)
            PictureBox7.Image = ArrowImageList.Images(2)
            If File.Exists((Application.StartupPath & "\").Replace("\\", "\") & "config.ini") = False Then
                If File.Exists((Application.StartupPath & "\").Replace("\\", "\") & "default_config.ini") = True Then
                    File.Copy((Application.StartupPath & "\").Replace("\\", "\") & "default_config.ini", (Application.StartupPath & "\").Replace("\\", "\") & "config.ini")
                End If
            End If

            If File.Exists((Application.StartupPath & "\").Replace("\\", "\") & "config.ini") = True Then
                Dim server As String = ""
                Dim table As String = ""
                Dim user As String = ""

                Try


                    Dim filereader As IO.StreamReader = New StreamReader((Application.StartupPath & "\").Replace("\\", "\") & "config.ini")

                    server = filereader.ReadLine.Replace("Server:", "").Trim
                    table = filereader.ReadLine.Replace("Table:", "").Trim
                    user = filereader.ReadLine.Replace("User:", "").Trim
                    error_reporting_level = filereader.ReadLine.Replace("Errors:", "").Trim
                    Worker1.dbserver = server
                    Worker1.dbuser = user
                    Worker1.dbtable = table

                    filereader.Close()
                Catch ex As Exception
                    Dim displ As Display_Message = New Display_Message("No valid config.ini file could be located in the application startup folder. This application will no be forced to shutdown.")
                    displ.ShowDialog()
                    dataloaded = True
                    splash_loader.Visible = False
                    application_exit = True
                    Label8.Text = Format(Now(), "dd/MM/yyyy HH:mm:ss")
                    Timer2.Start()
                End Try
                If application_exit = False Then


                    Label8.Text = Format(Now(), "dd/MM/yyyy HH:mm:ss")
                    Timer2.Start()
                    ConnectionTester.Start()
                    force_check(3)


                    If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
                        Clear_Inputs(True)
                        colourbuttons(btnAllCalls)
                        Load_Calls("All", False, 1, "False")
                    End If
                    dataloaded = True
                    splash_loader.Visible = False

                    Try
                        Dim ApplicationName As String
                        ApplicationName = "Tutor Activity Logger Management Module"
                        Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
                        Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)
                        If Process.GetProcessesByName(aProcName).Length > 1 Then
                            Me.Hide()
                            Dim Display_Message1 As New Display_Message("Another Instance of " & ApplicationName & " is already running. Only one instance of " & ApplicationName & " is allowed to run at any time. This instance of the program will now commence shut down operations.")
                            Display_Message1.ShowDialog()
                            application_exit = True
                        End If
                    Catch ex As Exception
                        Error_Handler(ex, "Checking Multiple Application Instances")
                    End Try
                End If
            Else
                Dim displ As Display_Message = New Display_Message("No required config.ini file could be located in the application startup folder. This application will no be forced to shutdown.")
                displ.ShowDialog()
                dataloaded = True
                splash_loader.Visible = False
                application_exit = True
                Label8.Text = Format(Now(), "dd/MM/yyyy HH:mm:ss")
                Timer2.Start()
            End If




        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Private Sub exit_application()
        Try
            shutting_down = True
            Timer2.Stop()
            ConnectionTester.Stop()
            If Worker1.WorkerThread Is Nothing = False Then
                Worker1.WorkerThread.Abort()
                Worker1.Dispose()
            End If
            NotifyIcon1.Dispose()
            Application.Exit()
        Catch ex As Exception
            Error_Handler(ex, "Shutting Down Application")
        Finally
            Application.Exit()
        End Try
    End Sub

    Private Sub Main_Screen_closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            exit_application()
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub


    Public Sub WorkerHandler(ByVal Result As String)
        Try
            If Result.StartsWith("Server") = False Then
                If Result.IndexOf("Retrieval") = -1 Then



                    If CInt(Result.Split(" ")(0)) > 0 Then
                        If CInt(Result.Split(" ")(0)) = 1 Then
                            StatusBar.Text = Result.Split(" ")(0) & " Record Affected:" & Result.Replace(Result.Split(" ")(0), "")
                        Else
                            StatusBar.Text = Result.Split(" ")(0) & " Records Affected:" & Result.Replace(Result.Split(" ")(0), "")
                        End If

                        StatusBar.ForeColor = Color.LimeGreen
                        '   lstLogID.Text = Worker1.ActivityLog.Log_ID
                        '  lstLogID.ForeColor = Color.Green
                        Clear_Inputs(True)
                        Load_Calls(displayrecordcount.Tag, True, CInt(lblSegment.Text), lblSegment.Tag)
                    Else
                        StatusBar.Text = Result.Split(" ")(0) & " Records Affected:" & Result.Replace(Result.Split(" ")(0), "").Replace("Succeeded", "Failed")
                        StatusBar.ForeColor = Color.Red
                        '   lstLogID.Text = "Unassigned"
                        '    lstLogID.ForeColor = Color.Red
                    End If
                Else
                    If CInt(Result.Split(" ")(0)) > 0 Then
                        StatusBar.Text = Worker1.ActivityLog.Log_ID & " Activity Log Successfully Retrieved"
                        StatusBar.ForeColor = Color.LimeGreen
                        'lstLogID.ForeColor = Color.Green
                        'lstLogID.Text = Worker1.ActivityLog.Log_ID
                        'lstLogTutor.Text = Worker1.ActivityLog.Log_Tutor
                        'lstLogDate.Text = Worker1.ActivityLog.Log_Date
                        'lstLogTime.Text = Worker1.ActivityLog.Log_Time
                        'txtProblemDuration.Text = Worker1.ActivityLog.Log_Duration

                        Dim r As IEnumerator = lstCustomerTypeID.Items.GetEnumerator()
                        Dim counter As Integer = 0
                        While r.MoveNext = True
                            Dim obj As MTGCComboBoxItem = r.Current
                            If obj.Text = Worker1.ActivityLog.Log_User_Type.Trim Then
                                lstCustomerTypeID.SelectedIndex = counter
                                Exit While
                            End If
                            counter = counter + 1
                        End While
                        r = Nothing



                        'txtCustomerDetailsID.Text = Worker1.ActivityLog.Log_Personal_Details_ID
                        '  txtCustomerDetailsSurname.Text = Worker1.ActivityLog.Log_Personal_Details_Surname
                        'txtCustomerDetailsFirstName.Text = Worker1.ActivityLog.Log_Personal_Details_Firstname



                        r = lstProblemTypeID.Items.GetEnumerator()
                        counter = 0
                        While r.MoveNext = True
                            Dim obj As MTGCComboBoxItem = r.Current
                            If obj.Text = Worker1.ActivityLog.Log_Call_Type.Trim Then
                                lstProblemTypeID.SelectedIndex = counter
                                lstProblemType.SelectedIndex = lstProblemTypeID.SelectedIndex
                                Exit While
                            End If
                            counter = counter + 1
                        End While
                        r = Nothing

                        r = lstProblemSubTypeID.Items.GetEnumerator()
                        counter = 0
                        While r.MoveNext = True
                            Dim obj As MTGCComboBoxItem = r.Current
                            If obj.Text = Worker1.ActivityLog.Log_Call_Sub_Type.Trim Then
                                lstProblemSubTypeID.SelectedIndex = counter
                                lstProblemSubType.SelectedIndex = lstProblemSubTypeID.SelectedIndex
                                Exit While
                            End If
                            counter = counter + 1
                        End While
                        r = Nothing
                        'Select Case Worker1.ActivityLog.Log_Resolve_Status
                        '    Case "Unresolved"
                        '        lstResolveStatus.SelectedIndex = 0
                        '    Case "Resolved"
                        '        lstResolveStatus.SelectedIndex = 1
                        'End Select


                        'lstResolveDate.Text = Worker1.ActivityLog.Log_Resolve_Date
                        'lstResolveTime.Text = Worker1.ActivityLog.Log_Resolve_Time
                        'lstResolveTutor.Text = Worker1.ActivityLog.Log_Resolve_Tutor
                        'txtProblemDescription.Text = Worker1.ActivityLog.Log_Problem_Description
                        'txtProblemResolution.Text = Worker1.ActivityLog.Log_Problem_Resolution

                        '  lstLogModifyDate.Text = Worker1.ActivityLog.Log_Modify_Date
                        '   lstLogModifyTime.Text = Worker1.ActivityLog.Log_Modify_Time
                        '  lstLogModifyTutor.Text = Worker1.ActivityLog.Log_Modify_Tutor

                    Else
                        StatusBar.Text = Worker1.ActivityLog.Log_ID & " Activity Log Retrieval Failed"
                        StatusBar.ForeColor = Color.Red
                        '  lstLogID.ForeColor = Color.Red
                    End If
                End If
            Else
                testingconnection = False
                ConnectionTesterCounter.Text = 10
                'ConnectionTester.Start()
                ServerStatus.Text = Worker1.serverstatus.Text
                ServerStatus.ForeColor = Worker1.serverstatus.ForeColor
                ConnectionTesterCounter.ForeColor = Worker1.serverstatus.ForeColor
                ServerStatus.Refresh()
                If ServerStatus.Text = "SQL Server Status: Online" Then
                    LockControls(True)
                    If lstCustomerType.Items.Count < 1 Then
                        Clear_Inputs(True)
                        colourbuttons(btnAllCalls)
                        Load_Calls("All", True, 1, "False")
                    End If
                End If

            End If
          
        Catch ex As Exception
            Error_Handler(ex)
        Finally
            currently_working = False
            NotifyIcon1.Text = "Resting... "
            run_green_lights()
        End Try
    End Sub

 

  

 
    Public Sub WorkerErrorEncounteredHandler(ByVal ex As Exception)
        Try
            Error_Handler(ex, "Worker Error Encountered")
        Catch exc As Exception
            Error_Handler(exc)
        End Try
    End Sub

    Private Sub run_worker(Optional ByVal threadselect As Integer = 1)
        run_orange_lights()

        Select Case threadselect
            Case 1
                Worker1.ChooseThreads(1)
            Case 2

                '    Dim cinfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")

                '    Worker1.ActivityLog.Clear_Data()


                'Worker1.ActivityLog.Log_User_Type = lstCustomerTypeID.SelectedItem.text
                ''  Worker1.ActivityLog.Log_Personal_Details_ID = txtCustomerDetailsID.Text.Trim.ToUpper

                '' Worker1.ActivityLog.Log_Personal_Details_Firstname = cinfo.TextInfo.ToTitleCase(txtCustomerDetailsFirstName.Text.Trim.Replace("  ", " "))
                'Worker1.ActivityLog.Log_Call_Type = lstProblemTypeID.SelectedItem.text
                'Worker1.ActivityLog.Log_Call_Sub_Type = lstProblemSubTypeID.SelectedItem.text

                'StatusBar.Text = ""
                'Worker1.ChooseThreads(2)
            Case 3

                If testingconnection = False Then
                    ConnectionTesterCounter.Text = 0
                    'ConnectionTester.Stop()
                    ServerStatus.ForeColor = Color.Orange
                    ServerStatus.Text = "SQL Server Status: Checking"
                    ToolTip1.SetToolTip(ServerStatus, "SQL Server connection status on " & Worker1.dbserver & " (user: " & Worker1.dbuser & ")")
                    ServerStatus.Refresh()
                    testingconnection = True
                    LockControls(False)
                    Worker1.ChooseThreads(3)
                End If
            Case 51
                Worker1.ActivityLog.Clear_Data()
                If lstCustomerTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.User_ID = lstCustomerTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.User_Description = txtUserDescription.Text
                Worker1.ActivityLog.User_Personal_Details_ID_Descriptor = txtUserPersonalDetailsIDDescriptor.Text.ToString.Trim
                StatusBar.Text = ""
                Worker1.ChooseThreads(51)
            Case 52
                Worker1.ActivityLog.Clear_Data()
                If lstCustomerTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.User_ID = lstCustomerTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.User_Description = txtUserDescription.Text
                Worker1.ActivityLog.User_Personal_Details_ID_Descriptor = txtUserPersonalDetailsIDDescriptor.Text.ToString.Trim
                StatusBar.Text = ""
                Worker1.ChooseThreads(52)
            Case 53
                Worker1.ActivityLog.Clear_Data()
                If lstCustomerTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.User_ID = lstCustomerTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.User_Description = txtUserDescription.Text
                Worker1.ActivityLog.User_Personal_Details_ID_Descriptor = txtUserPersonalDetailsIDDescriptor.Text.ToString.Trim
                StatusBar.Text = ""
                Worker1.ChooseThreads(53)
            Case 61
                Worker1.ActivityLog.Clear_Data()
                If lstProblemTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.Call_ID = lstProblemTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.Call_Description = txtCallDescription.Text
                StatusBar.Text = ""
                Worker1.ChooseThreads(61)
            Case 62
                Worker1.ActivityLog.Clear_Data()
                If lstProblemTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.Call_ID = lstProblemTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.Call_Description = txtCallDescription.Text
                StatusBar.Text = ""
                Worker1.ChooseThreads(62)
            Case 63
                Worker1.ActivityLog.Clear_Data()
                If lstProblemTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.Call_ID = lstProblemTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.Call_Description = txtCallDescription.Text
                StatusBar.Text = ""
                Worker1.ChooseThreads(63)
            Case 71
                Worker1.ActivityLog.Clear_Data()
                If lstProblemSubTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.Call_Sub_ID = lstProblemSubTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.Call_Sub_Description = txtCallSubDescription.Text
                Worker1.ActivityLog.Call_Sub_Call_ID = lstProblemTypeID.SelectedItem.text.ToString.Trim
                StatusBar.Text = ""
                Worker1.ChooseThreads(71)
            Case 72
                Worker1.ActivityLog.Clear_Data()
                If lstProblemSubTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.Call_Sub_ID = lstProblemSubTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.Call_Sub_Description = txtCallSubDescription.Text
                Worker1.ActivityLog.Call_Sub_Call_ID = lstProblemTypeID.SelectedItem.text.ToString.Trim
                StatusBar.Text = ""
                Worker1.ChooseThreads(72)
            Case 73
                Worker1.ActivityLog.Clear_Data()
                If lstProblemSubTypeID.Items.Count > 0 Then
                    Worker1.ActivityLog.Call_Sub_ID = lstProblemSubTypeID.SelectedItem.text.ToString.Trim
                End If
                Worker1.ActivityLog.Call_Sub_Description = txtCallSubDescription.Text
                Worker1.ActivityLog.Call_Sub_Call_ID = lstProblemTypeID.SelectedItem.text.ToString.Trim
                StatusBar.Text = ""
                Worker1.ChooseThreads(73)
            Case 8
                    Worker1.ActivityLog.Clear_Data()
                    Worker1.SQLstatement = txtSQLstatement.Text.Trim
                    StatusBar.Text = ""
                    Worker1.ChooseThreads(8)
        End Select

        currently_working = True
    End Sub



    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        Try
            Me.Close()
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Protected Friend Sub show_application()
        Try
            Me.Opacity = 1

            Me.BringToFront()
            Me.Refresh()
            Me.WindowState = FormWindowState.Normal

        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Private Sub NotifyIcon1_dblclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        show_application()
    End Sub
    Private Sub NotifyIcon1_snglclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        show_application()
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        show_application()
    End Sub

    Private Sub Main_Screen_resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Try

            If Me.WindowState = FormWindowState.Minimized Then
                NotifyIcon1.Visible = True
                Me.Opacity = 0
            End If
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Private Sub force_check(Optional ByVal threadselect As Integer = 1)
        Try
            NotifyIcon1.Text = "Processing Request..."
            If currently_working = False Then

                Select Case threadselect
                    Case 1
                        run_worker(threadselect)
                    Case 2
                        If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
                            'If txtProblemDescription.Text.Trim = "" Or txtProblemResolution.Text.Trim = "" Then
                            '    Dim errorl As String = ""
                            '    If txtProblemDescription.Text.Trim = "" Then
                            '        errorl = errorl & vbCrLf & "- Problem Description is currently blank"
                            '    End If
                            '    If txtProblemResolution.Text.Trim = "" Then
                            '        errorl = errorl & vbCrLf & "- Problem Resolution is currently blank"
                            '    End If
                            '    MsgBox("You need to ensure that all inputs marked with a red asterisk are filled in before saving an Activity Log." & vbCrLf & errorl, MsgBoxStyle.Information, "Save Request Rejected")
                            'Else
                                run_worker(threadselect)
                            'End If
                        Else
                            NotifyIcon1.Text = "Resting..."
                        End If
                    Case 3
                        run_worker(threadselect)
                    Case 51
                        run_worker(threadselect)
                    Case 52
                        run_worker(threadselect)
                    Case 53
                        run_worker(threadselect)
                    Case 61
                        run_worker(threadselect)
                    Case 62
                        run_worker(threadselect)
                    Case 63
                        run_worker(threadselect)
                    Case 71
                        run_worker(threadselect)
                    Case 72
                        run_worker(threadselect)
                    Case 73
                        run_worker(threadselect)
                    Case 8
                        If txtSQLstatement.Text.Trim.Length > 0 Then


                        Dim result As DialogResult = MsgBox("Are you sure you wish to execute the following statement against the database?" & vbCrLf & txtSQLstatement.Text.Trim, MsgBoxStyle.OKCancel, "Execute Query Confirmation")
                        If result = DialogResult.OK Then
                            run_worker(threadselect)
                        Else
                            NotifyIcon1.Text = "Resting..."
                            End If
                        Else
                            NotifyIcon1.Text = "Resting..."
                        End If
                End Select



            End If
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        force_check()
    End Sub


    Private Function DosShellCommand(ByVal AppToRun As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process

            myProcess.StartInfo.FileName = "cmd.exe"
            myProcess.StartInfo.UseShellExecute = False



            myProcess.StartInfo.CreateNoWindow = True

            myProcess.StartInfo.RedirectStandardInput = False
            myProcess.StartInfo.RedirectStandardOutput = False
            myProcess.StartInfo.RedirectStandardError = False

            myProcess.StartInfo.FileName = AppToRun

            myProcess.Start()

        Catch ex As Exception
            Error_Handler(ex)
        End Try
        Return s
    End Function


    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Try
            Dim apptorun As String
            apptorun = """" & (Application.StartupPath & "\Tutor Activity Logger Management Module.exe").Replace("\\", "\") & """"
            DosShellCommand(apptorun)
            exit_application()
        Catch ex As Exception
            Error_Handler(ex, "Logging Off")
        End Try
    End Sub



    Private Sub fill_in_data()
      
        If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
            Try



                If loginaudited = False And Worker1.serverstatus.Text = "SQL Server Status: Online" Then
                    Dim result As String
                    Try

                        Dim conn1 As OleDb.OleDbConnection = Worker1.Get_Connection()

                        conn1.Open()
                        Try
                            Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                            sql = New OleDb.OleDbCommand
                            sql.CommandText = "INSERT INTO [Audit_Logins] ([Audit_Tutor],[Audit_Date],[Audit_Time]) values ('" & Current_User.Text.Trim & " (M)','" & Format(Now(), "dd/MM/yyyy") & "','" & Format(Now(), "HH:mm:ss") & "')"
                            sql.Connection = conn1
                            result = sql.ExecuteNonQuery().ToString & " Insert Succeeded"
                            sql.Dispose()
                            result = "Login Tracking Audit Succeeded"
                        Catch ex As Exception
                            Error_Handler(ex)
                            result = "Login Tracking Audit Failed"
                        Finally
                            conn1.Close()
                            conn1.Dispose()
                        End Try
                    Catch ex As Exception
                        Error_Handler(ex)
                        result = "Login Tracking Audit Failed"
                    Finally
                        StatusBar.Text = result
                        If StatusBar.Text = "Login Tracking Audit Failed" Then
                            StatusBar.ForeColor = Color.Red
                        Else
                            StatusBar.ForeColor = Color.LimeGreen
                            loginaudited = True
                        End If
                    End Try
                End If

                Dim conn As OleDb.OleDbConnection = Worker1.Get_Connection()
                conn.Open()
                Try
                    lblStats.Text = "Quick Stats for Tutor Activity Logger"
                    lblTotalYearCalls.Text = "Total Calls for " & Format(Now, "yyyy") & ":"
                    lblTotalMonthCalls.Text = "Total Calls for " & Format(Now, "MMMM yyyy") & ":"



                    Dim stats(1) As String
                    
                    stats(0) = "Select count([Log_ID]) as Total_Calls from [Log_Records] where [Log_Date] like '%/" & Year(Now) & "%'"
                    stats(1) = "Select count([Log_ID]) as Total_Calls from [Log_Records] where [Log_Date] like '%/" & Month(Now) & "/" & Year(Now) & "%'"
                    Dim controls(1) As Control
                  
                    controls(0) = TotalYearCalls
                    controls(1) = TotalMonthCalls
                    Dim counter As Integer = 0
                    Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                    Dim datareader As OleDb.OleDbDataReader
                    For Each str As String In stats
                        sql = New OleDb.OleDbCommand
                        sql.CommandText = str
                        sql.Connection = conn

                        datareader = sql.ExecuteReader(CommandBehavior.Default)
                        If datareader.HasRows = True Then
                            While datareader.Read = True
                                controls(counter).Text = (datareader.Item("Total_Calls"))
                            End While
                        End If
                        datareader.Close()
                        sql.Dispose()
                        counter = counter + 1
                    Next


                    lstCustomerType.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
                    lstCustomerType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
                    lstCustomerType.ColumnNum = 1
                    lstCustomerType.ColumnWidth = "80"

                    If lstCustomerType.Items.Count > 0 Then
                        lstCustomerType.Items.Clear()
                        lstCustomerTypeID.Items.Clear()
                        lstCustomerTypePDText.Items.Clear()
                        lstCustomerTypeCover.Text = ""
                        txtUserDescription.Text = ""
                        txtUserPersonalDetailsIDDescriptor.Text = ""
                    End If
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "Select [User_ID],[User_Description], [User_Personal_Details_ID_Descriptor] from [User_Type] order by [User_ID] asc"
                    sql.Connection = conn

                    datareader = sql.ExecuteReader(CommandBehavior.Default)
                    If datareader.HasRows = True Then
                        While datareader.Read = True
                            lstCustomerType.Items.Add(New MTGCComboBoxItem(datareader.Item("User_Description").ToString.Trim))
                            lstCustomerTypeID.Items.Add(New MTGCComboBoxItem(datareader.Item("User_ID").ToString.Trim))
                            lstCustomerTypePDText.Items.Add(New MTGCComboBoxItem(datareader.Item("User_Personal_Details_ID_Descriptor").ToString.Trim))
                        End While
                    End If
                    datareader.Close()
                    sql.Dispose()
                    If lstCustomerType.Items.Count > 0 Then
                        lstCustomerType.SelectedIndex = 0
                        lstCustomerTypeID.SelectedIndex = 0
                        lstCustomerTypePDText.SelectedIndex = 0
                    End If

                    If lstProblemType.Items.Count > 0 Then
                        lstProblemType.Items.Clear()
                        lstProblemTypeID.Items.Clear()
                        lstProblemTypeCover.Text = ""
                        txtCallDescription.Text = ""
                    End If
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "Select [Call_ID],[Call_Description] from [Call_Type] order by [Call_ID] asc"
                    sql.Connection = conn

                    datareader = sql.ExecuteReader(CommandBehavior.Default)
                    If datareader.HasRows = True Then
                        While datareader.Read = True
                            lstProblemType.Items.Add(New MTGCComboBoxItem(datareader.Item("Call_Description").ToString.Trim))
                            lstProblemTypeID.Items.Add(New MTGCComboBoxItem(datareader.Item("Call_ID").ToString.Trim))
                        End While
                    End If
                    datareader.Close()
                    sql.Dispose()
                    If lstProblemType.Items.Count > 0 Then
                        lstProblemType.SelectedIndex = 0
                        lstProblemTypeID.SelectedIndex = 0
                    End If


                  
                    lstCustomerTypeCover.Select()
                Catch ex As Exception
                    Error_Handler(ex, "Autofill Main Screen Data")
                Finally
                    conn.Close()
                    conn.Dispose()
                End Try
            Catch ex As Exception
                Error_Handler(ex, "Autofill Main Screen Data")
            End Try
        End If
    End Sub

    Private Sub lstCustomerTypeID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCustomerTypeID.SelectedIndexChanged
        Try
            lstCustomerType.SelectedIndex = lstCustomerTypeID.SelectedIndex
            lstCustomerTypeCover.Text = lstCustomerType.Text
            lstCustomerTypePDText.SelectedIndex = lstCustomerTypeID.SelectedIndex

            txtUserPersonalDetailsIDDescriptor.Text = lstCustomerTypePDText.SelectedItem.text
            txtUserDescription.Text = lstCustomerType.Text
            txtUserDescription.Select(0, 0)
        Catch ex As Exception
            Error_Handler(ex, "Changing User Type")
        End Try
    End Sub

    Private Sub lstCustomerType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCustomerType.SelectedIndexChanged
        Try
            lstCustomerTypeCover.Text = lstCustomerType.Text
            lstCustomerTypeID.SelectedIndex = lstCustomerType.SelectedIndex
            lstCustomerTypePDText.SelectedIndex = lstCustomerType.SelectedIndex
            txtUserPersonalDetailsIDDescriptor.Text = lstCustomerTypePDText.SelectedItem.text
            txtUserDescription.Text = lstCustomerType.Text
            txtUserDescription.Select(0, 0)
        Catch ex As Exception
            Error_Handler(ex, "Changing User Type")
        End Try
    End Sub

    Private Sub lstProblemType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProblemType.SelectedIndexChanged

        If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
            Try

                lstProblemTypeCover.Text = lstProblemType.Text
                lstProblemTypeID.SelectedIndex = lstProblemType.SelectedIndex
                txtCallDescription.Text = lstProblemType.Text
                txtCallDescription.Select(0, 0)


                If lstProblemSubType.Items.Count > 0 Then
                    lstProblemSubType.Items.Clear()
                    lstProblemSubTypeID.Items.Clear()
                    lstProblemSubTypeCover.Text = ""
                    txtCallSubDescription.Text = ""
                End If
                Dim conn As OleDb.OleDbConnection = Worker1.Get_Connection()
                conn.Open()
                Try
                    Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                    Dim datareader As OleDb.OleDbDataReader
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "Select [Call_Sub_ID],[Call_Sub_Description] from [Call_Sub_Type] where [Call_Sub_Call_ID] = '" & lstProblemTypeID.SelectedItem.Text & "' order by [Call_Sub_ID] asc"
                    sql.Connection = conn

                    datareader = sql.ExecuteReader(CommandBehavior.Default)
                    If datareader.HasRows = True Then
                        While datareader.Read = True
                            lstProblemSubType.Items.Add(New MTGCComboBoxItem(datareader.Item("Call_Sub_Description").ToString.Trim))
                            lstProblemSubTypeID.Items.Add(New MTGCComboBoxItem(datareader.Item("Call_Sub_ID").ToString.Trim))
                        End While
                    End If
                    datareader.Close()
                    sql.Dispose()
                    If lstProblemSubType.Items.Count > 0 Then
                        lstProblemSubType.SelectedIndex = 0
                        lstProblemSubTypeID.SelectedIndex = 0
                        lstProblemSubTypeCover.Text = lstProblemSubType.Text
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "Changing Problem Type")
                Finally
                    conn.Close()
                    conn.Dispose()
                End Try

            Catch ex As Exception
                Error_Handler(ex, "Changing Problem Type")
            End Try
        End If
    End Sub

    

    Private Sub lstProblemSubType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProblemSubType.SelectedIndexChanged
        Try
            lstProblemSubTypeCover.Text = lstProblemSubType.Text
            lstProblemSubTypeID.SelectedIndex = lstProblemSubType.SelectedIndex
            txtCallSubDescription.Text = lstProblemSubType.Text
            txtCallSubDescription.Select(0, 0)
        Catch ex As Exception
            Error_Handler(ex, "Changing Problem Sub Type")
        End Try
    End Sub



    Private Function Check_For_Dirty() As Boolean
        Dim result As Boolean = False
        Try

            Dim ctrl As Object
            For Each ctrl In GroupBox1.Controls
                If (ctrl.GetType.ToString).IndexOf("TextBox") <> -1 Then
                    If ctrl.text.length > 0 And ctrl.name.startswith("txt") = True Then

                        result = True
                    End If
                End If
            Next
            For Each ctrl In GroupBox2.Controls
                If ctrl.text.length > 0 And ctrl.name.startswith("txt") = True Then

                    result = True
                End If
            Next
        Catch ex As Exception
            Error_Handler(ex, "New Log")
        End Try
        Return result
    End Function

    Private Sub Clear_Inputs(Optional ByVal ForceClear As Boolean = False)
        Try
         
                    fill_in_data()
            
            lstCustomerType.SelectedIndex = 0
            lstProblemType.SelectedIndex = 0
            lstProblemSubType.SelectedIndex = 0

            lstCustomerTypeCover.Select()

        Catch ex As Exception
            Error_Handler(ex, "New Log")
        End Try
    End Sub





    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        Try
            exit_application()
        Catch ex As Exception
            Error_Handler(ex, "Exiting Application")
        End Try
    End Sub


    Private Sub LockControls(ByVal unlocked As Boolean)
        Try
            lstCallList.Enabled = unlocked
            ' btnMyCalls.Enabled = unlocked
            btnAllCalls.Enabled = unlocked
            ' btnMyOpenCalls.Enabled = unlocked
            btnAllOpenCalls.Enabled = unlocked
            '  btnSaveLog.Enabled = unlocked
            '  btnNewLog.Enabled = unlocked
            ' MenuItem6.Enabled = unlocked
            lstCustomerType.Enabled = unlocked
            lstProblemType.Enabled = unlocked
            lstProblemSubType.Enabled = unlocked
        Catch ex As Exception
            Error_Handler(ex, "Locking Controls")
        End Try
    End Sub

    Private Sub ConnectionTester_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectionTester.Tick
        force_check(3)
    End Sub




    Private Sub Load_Calls(Optional ByVal Tutor As String = "All", Optional ByVal force As Boolean = False, Optional ByVal Segment As Integer = 1, Optional ByVal OpenCalls As String = "False")
        Try
            If (testingconnection = False And currently_working = False And Worker1.serverstatus.Text = "SQL Server Status: Online") Or force = True Then
                lstCallList.Items.Clear()
                Dim conn As OleDb.OleDbConnection = Worker1.Get_Connection
                conn.Open()
                Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                Dim datareader As OleDb.OleDbDataReader
                sql = New OleDb.OleDbCommand
                Dim filler As String = ""
                If OpenCalls = "True" Then
                    If Tutor = "All" Then
                        filler = " where [Log_Resolve_Status] = 'Unresolved' "
                    Else
                        filler = " and [Log_Resolve_Status] = 'Unresolved' "
                    End If

                End If
                If Tutor = "All" Then
                    sql.CommandText = "Select [Log_ID] from [Log_Records]" & filler & "order by [Log_ID] desc"
                Else
                    sql.CommandText = "Select [Log_ID] from [Log_Records] where [Log_Tutor] = '" & Current_User.Text & "'" & filler & " order by [Log_ID] desc"
                End If

                sql.Connection = conn
                Dim segmentend, segmentstart As Integer
                segmentend = 35 * Segment
                segmentstart = 35 * (Segment - 1)
                datareader = sql.ExecuteReader(CommandBehavior.Default)
                Dim runner As Integer = 1
                If datareader.HasRows = True Then
                    While datareader.Read = True

                        If runner > segmentstart And runner <= segmentend Then
                            lstCallList.Items.Add("   " & datareader.Item("Log_ID").ToString.Trim)
                        End If
                        runner = runner + 1
                    End While

                End If
                runner = runner - 1
                lblSegment.Text = Segment
                lblSegment.Tag = OpenCalls
                lblSegmentNext.Text = Segment + 1
                lblSegmentPrevious.Text = Segment - 1
                If (Segment - 1) < 1 Then
                    PictureBox6.Visible = False
                    lblSegmentPrevious.Visible = False
                    PictureBox6.Enabled = False
                Else
                    PictureBox6.Visible = True
                    lblSegmentPrevious.Visible = True
                    PictureBox6.Enabled = True
                End If
                If Not (Segment * 35) < runner Then
                    PictureBox7.Visible = False
                    lblSegmentNext.Visible = False
                    PictureBox7.Enabled = False
                Else
                    PictureBox7.Visible = True
                    lblSegmentNext.Visible = True
                    PictureBox7.Enabled = True
                End If
                displayrecordcount.Text = "(" & lstCallList.Items.Count & " of " & runner & ")"
                displayrecordcount.Tag = Tutor
                datareader.Close()
                sql.Dispose()


                conn.Close()
            End If
        Catch ex As Exception
            Error_Handler(ex, "Loading My Calls")
        End Try
    End Sub






    Private Sub colourbuttons(ByVal inputbutton As Button)
        Try
            Dim choice() As Button = {btnAllCalls, btnAllOpenCalls}



            Dim u As Button
            For Each u In choice
                u.BackColor = Color.AliceBlue
            Next
            Dim result As Integer = (choice.IndexOf(choice, inputbutton))
            If result <> -1 Then
                choice(result).BackColor = Color.PowderBlue
            End If
        Catch ex As Exception
            Error_Handler(ex, "Colour Buttons")
        End Try
    End Sub

    Private Sub btnAllCalls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllCalls.Click
        Try
            colourbuttons(sender)
            Load_Calls("All", False, 1)
        Catch ex As Exception
            Error_Handler(ex, "Display Records Button")
        End Try
    End Sub

    Private Sub PictureBox7_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.MouseHover
        Try
            PictureBox7.Image = ArrowImageList.Images(3)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Overlay")
        End Try
    End Sub
    Private Sub PictureBox7_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.MouseLeave
        Try
            PictureBox7.Image = ArrowImageList.Images(2)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Overlay")
        End Try
    End Sub
    Private Sub PictureBox6_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.MouseHover
        Try
            PictureBox6.Image = ArrowImageList.Images(1)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Overlay")
        End Try
    End Sub
    Private Sub PictureBox6_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.MouseLeave
        Try
            PictureBox6.Image = ArrowImageList.Images(0)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Overlay")
        End Try
    End Sub


    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        Try
            Load_Calls(displayrecordcount.Tag.ToString(), False, CInt(lblSegmentNext.Text), lblSegment.Tag)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Click")
        End Try
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Try
            Load_Calls(displayrecordcount.Tag.ToString(), False, CInt(lblSegmentPrevious.Text), lblSegment.Tag)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Click")
        End Try
    End Sub

   

    Private Sub btnAllOpenCalls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllOpenCalls.Click
        Try
            colourbuttons(sender)
            Load_Calls("All", False, 1, "True")
        Catch ex As Exception
            Error_Handler(ex, "Display Records Button")
        End Try
    End Sub

    Private Sub load_databaselayout()
        Try
            If File.Exists((Application.StartupPath & "\").Replace("\\", "\") & "Database_Layout.txt") = True Then
                databaselayout.LoadFile((Application.StartupPath & "\").Replace("\\", "\") & "Database_Layout.txt", RichTextBoxStreamType.PlainText)
            Else
                databaselayout.Text = "No valid Database_Layout.txt Document could be located."
            End If
        Catch ex As Exception
            Error_Handler(ex, "Loading Database Layout Text")
        End Try
    End Sub

    Private Sub btnCreateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateUser.Click
        force_check(51)
    End Sub

    Private Sub btnRemoveUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveUser.Click
        force_check(52)
    End Sub

    Private Sub btnUpdateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateUser.Click
        force_check(53)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        force_check(8)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        txtSQLstatement.Text = "Delete from [Log_Records]"
        force_check(8)
    End Sub


    Private Sub btnUpdateCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCall.Click
        force_check(63)
    End Sub

    Private Sub btnUpdateCallSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCallSub.Click
        force_check(73)
    End Sub

    Private Sub btnRemoveCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveCall.Click
        force_check(62)
    End Sub


    Private Sub btnRemoveCallSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveCallSub.Click
        force_check(72)
    End Sub

    Private Sub btnCreateCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateCall.Click
        force_check(61)
    End Sub

    Private Sub btnCreateCallSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateCallSub.Click
        force_check(71)
    End Sub

    Private Sub databaselayout_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles databaselayout.SelectionChanged
        Try
            databaselayout.SelectionLength = 0
        Catch ex As Exception

        End Try

    End Sub




    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem6.Click
        Try

        
        Dim sqlstring As String = ""
            sqlstring = "Delete from [Log_Records] where "
        Dim ienum As IEnumerator = lstCallList.SelectedItems.GetEnumerator()
        While ienum.MoveNext = True
                sqlstring = sqlstring & "[Log_ID] = '" & ienum.Current.ToString().Trim & "' or "
            End While
            sqlstring = sqlstring.Remove(sqlstring.Length - 3, 3)
            txtSQLstatement.Text = sqlstring
            force_check(8)
        Catch ex As Exception
            Error_Handler(ex, "Deleting Specified Records")
        End Try
    End Sub
End Class
