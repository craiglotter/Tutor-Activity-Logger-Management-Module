Imports System.IO
Imports Microsoft.Win32

Public Class Worker

    Inherits System.ComponentModel.Component

    ' Declares the variables you will use to hold your thread objects.

    Public WorkerThread As System.Threading.Thread

    Public dbuser As String
    Public dbpassword As String
    Public dbtable As String
    Public dbserver As String

    Public SQLstatement As String

    Public username As String
    Public password As String
    Public context As String
    Public group As String
    Public error_level As String = "none"
    Public ActivityLog As Activity_Log
    Public serverstatus As Windows.Forms.TextBox

    Public result As String = ""

    Public Event WorkerErrorEncountered(ByVal ex As Exception)
    Public Event WorkerComplete(ByVal Result As String)
    


#Region " Component Designer generated code "

    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        Container.Add(Me)
    End Sub

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
      
        username = ""
        password = ""
        context = ""
        group = ""
        error_level = "none"
        ActivityLog = New Activity_Log
        serverstatus = New Windows.Forms.TextBox
        dbuser = ""
        dbpassword = "helpdesktutor"
        dbtable = ""
        dbserver = ""
        SQLstatement = ""
    End Sub

    'Component overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Private Sub Error_Handler(ByVal exc As Exception)
        Try
            RaiseEvent WorkerErrorEncountered(exc)
        Catch ex As Exception
            MsgBox("An error occurred in Tutor Activity Logger Management Module's error handling routine. The application will try to recover from this serious error.", MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub


    Public Sub ChooseThreads(ByVal threadNumber As Integer)
        Try
            ' Determines which thread to start based on the value it receives.

            Select Case threadNumber
                Case 1
                    ' Sets the thread using the AddressOf the subroutine where
                    ' the thread will start.
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerExecute)
                    ' Starts the thread.
                    WorkerThread.Start()
                Case 2
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerLogCall)
                    WorkerThread.Start()
                Case 3
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerConnectionTest)
                    WorkerThread.Start()
                Case 4
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerRetrieveCall)
                    WorkerThread.Start()
                Case 51
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerInsertUserType)
                    WorkerThread.Start()
                Case 52
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerDeleteUserType)
                    WorkerThread.Start()
                Case 53
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerUpdateUserType)
                    WorkerThread.Start()
                Case 61
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerInsertCallType)
                    WorkerThread.Start()
                Case 62
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerDeleteCallType)
                    WorkerThread.Start()
                Case 63
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerUpdateCallType)
                    WorkerThread.Start()
                Case 71
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerInsertCallSubType)
                    WorkerThread.Start()
                Case 72
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerDeleteCallSubType)
                    WorkerThread.Start()
                Case 73
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerUpdateCallSubType)
                    WorkerThread.Start()
                Case 8
                    WorkerThread = New System.Threading.Thread(AddressOf WorkerExecuteNonQuery)
                    WorkerThread.Start()
            End Select
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub



    Public Sub WorkerExecute()
        Try
           
            Dim apptorun As String
            Dim finfo As FileInfo = New FileInfo((Application.StartupPath & "\UCT Novell Login Shell.exe").Replace("\\", "\"))
            If finfo.Exists = True Then
                apptorun = """" & (Application.StartupPath & "\UCT Novell Login Shell.exe").Replace("\\", "\") & """ """ & username & """ """ & password & """ """ & context & """ """ & group & """ """ & error_level & """"

                result = DosShellCommand(apptorun)
            Else
                result = "Failure. Login Script Executable cannot be found"
            End If
            finfo = Nothing
        Catch ex As Exception
            Error_Handler(ex)
            result = "Failure. Unknown Reason."
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub


    Public Sub WorkerLogCall()
        Try
           
            Dim conn As OleDb.OleDbConnection = Get_Connection()
                conn.Open()
            Try
                Dim Log_ID As String = ""
                Dim temp_ID As String = ""
                Dim lngtemp_id As Long = -1
                Dim insertlog As Boolean = False
                Dim counter As Integer = 0
                Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                Dim datareader As OleDb.OleDbDataReader
                If ActivityLog.Log_ID = "Unassigned" Then
                   
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "Select max([Log_ID]) as maxid from [Log_Records]"
                    sql.Connection = conn

                    datareader = sql.ExecuteReader(CommandBehavior.Default)
                    If datareader.HasRows = True Then
                        If datareader.Read = True Then
                            If Not datareader.Item("maxid").GetType.ToString.Equals("System.DBNull") Then
                                temp_ID = (datareader.Item("maxid"))

                                lngtemp_id = CLng(temp_ID.Remove(0, 2))
                                lngtemp_id = lngtemp_id + 1
                                Log_ID = lngtemp_id.ToString
                            Else
                                Log_ID = "1"
                            End If

                            While Log_ID.Length < 8
                                Log_ID = "0" & Log_ID
                            End While
                            Log_ID = "AL" & Log_ID
                            ActivityLog.Log_ID = Log_ID
                            insertlog = True
                        End If
                    End If
                    datareader.Close()
                    sql.Dispose()
                Else
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "Delete from [Log_Records] where [Log_ID] = '" & ActivityLog.Log_ID & "'"
                    sql.Connection = conn
                    If sql.ExecuteNonQuery() > 0 Then
                        insertlog = True
                    End If
                    sql.Dispose()

                End If

                If insertlog = True Then
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = ActivityLog.Generate_SQL_Insert()
                    sql.Connection = conn
                    result = sql.ExecuteNonQuery().ToString & " Insert Succeeded"
                    sql.Dispose()
                End If

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Insert Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try



        Catch ex As Exception
                Error_Handler(ex)
            result = "0 Insert Failed"
            Finally
                RaiseEvent WorkerComplete(result)
            End Try
    End Sub

    Public Sub WorkerRetrieveCall()
        Try

            Dim conn As OleDb.OleDbConnection = Get_Connection()
            conn.Open()
            Try
                Dim Log_ID As String = ""
                Dim temp_ID As String = ""
                Dim lngtemp_id As Long = -1
                Dim insertlog As Boolean = False

                Dim counter As Integer = 0
                Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                Dim datareader As OleDb.OleDbDataReader
                sql = New OleDb.OleDbCommand
                sql.CommandText = "Select * from [Log_Records] where [Log_ID] = '" & ActivityLog.Log_ID & "'"
                sql.Connection = conn

                datareader = sql.ExecuteReader(CommandBehavior.Default)
                If datareader.HasRows = True Then
                    If datareader.Read = True Then
                        ActivityLog.Log_ID = datareader.Item("Log_ID").ToString.Trim
                        ActivityLog.Log_Tutor = datareader.Item("Log_Tutor").ToString.Trim
                        ActivityLog.Log_Date = datareader.Item("Log_Date").ToString.Trim
                        ActivityLog.Log_Duration = datareader.Item("Log_Duration").ToString.Trim
                        ActivityLog.Log_Time = datareader.Item("Log_Time").ToString.Trim
                        ActivityLog.Log_User_Type = datareader.Item("Log_User_Type").ToString.Trim
                        ActivityLog.Log_Personal_Details_ID = datareader.Item("Log_Personal_Details_ID").ToString.Trim
                        ActivityLog.Log_Personal_Details_Surname = datareader.Item("Log_Personal_Details_Surname").ToString.Trim
                        ActivityLog.Log_Personal_Details_Firstname = datareader.Item("Log_Personal_Details_Firstname").ToString.Trim
                        ActivityLog.Log_Call_Type = datareader.Item("Log_Call_Type").ToString.Trim
                        ActivityLog.Log_Call_Sub_Type = datareader.Item("Log_Call_Sub_Type").ToString.Trim
                        ActivityLog.Log_Resolve_Status = datareader.Item("Log_Resolve_Status").ToString.Trim
                        ActivityLog.Log_Resolve_Date = datareader.Item("Log_Resolve_Date").ToString.Trim
                        ActivityLog.Log_Resolve_Time = datareader.Item("Log_Resolve_Time").ToString.Trim
                        ActivityLog.Log_Resolve_Tutor = datareader.Item("Log_Resolve_Tutor").ToString.Trim
                        ActivityLog.Log_Problem_Description = datareader.Item("Log_Problem_Description").ToString.Trim
                        ActivityLog.Log_Problem_Resolution = datareader.Item("Log_Problem_Resolution").ToString.Trim
                        ActivityLog.Log_Modify_Date = datareader.Item("Log_Modify_Date").ToString.Trim
                        ActivityLog.Log_Modify_Time = datareader.Item("Log_Modify_Time").ToString.Trim
                        ActivityLog.Log_Modify_Tutor = datareader.Item("Log_Modify_Tutor").ToString.Trim
                    End If
                    result = "1 Retrieval Succeeded"
                End If
                datareader.Close()
                sql.Dispose()


            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Retrieval Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try



        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Retrieval Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Protected Friend Function Get_Connection() As OleDb.OleDbConnection
        'Standard(Security)
        '"Provider=sqloledb;Data Source=Aron1;Initial Catalog=pubs;User Id=sa;Password=asdasd;" 

        'Trusted(Connection)
        '"Provider=sqloledb;Data Source=Aron1;Initial Catalog=pubs;Integrated Security=SSPI;" 
        '(use serverName\instanceName as Data Source to use an specifik SQLServer instance, only SQLServer2000)

        'Prompt for username and password:
        'oConn.Provider = "sqloledb"
        'oConn.Properties("Prompt") = adPromptAlways
        'oConn.Open("Data Source=Aron1;Initial Catalog=pubs;")

        'Connect via an IP address:
        '"Provider=sqloledb;Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=pubs;User ID=sa;Password=asdasd;" 
        '(DBMSSOCN=TCP/IP instead of Named Pipes, at the end of the Data Source is the port to use (1433 is the default))

        Dim connection_string As String
        If dbserver.IndexOf(".") = -1 Then
            connection_string = "Provider=sqloledb;Data Source=" & dbserver & ";Initial Catalog=" & dbtable & ";User Id=" & dbuser & ";Password=" & dbpassword & ";"
        Else
            connection_string = "Provider=sqloledb;Data Source=" & dbserver & ",1433;Network Library=DBMSSOCN;Initial Catalog=" & dbtable & ";User Id=" & dbuser & ";Password=" & dbpassword & ";"
        End If
        'Dim connection_string As String = "User ID=" & dbuser & ";password=" & dbpassword & ";Data Source=" & dbserver & ";Tag with column collation when possible=False;Initial Catalog=" & dbtable & ";Use Procedure for Prepare=1;Auto Translate=True;Persist Security Info=False;Provider=""SQLOLEDB.1"";Use Encryption for Data=False;Packet Size=4096"

        Dim conn As OleDb.OleDbConnection = New OleDb.OleDbConnection(connection_string)
        Return conn
    End Function

    Private Function DosShellCommand(ByVal AppToRun As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process

            myProcess.StartInfo.FileName = "cmd.exe"
            myProcess.StartInfo.UseShellExecute = False

            Dim sErr As StreamReader
            Dim sOut As StreamReader
            Dim sIn As StreamWriter


            myProcess.StartInfo.CreateNoWindow = True

            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.StartInfo.RedirectStandardOutput = True
            myProcess.StartInfo.RedirectStandardError = True

            myProcess.StartInfo.FileName = AppToRun

            myProcess.Start()
            sIn = myProcess.StandardInput
            sIn.AutoFlush = True

            sOut = myProcess.StandardOutput()
            sErr = myProcess.StandardError

            sIn.Write(AppToRun & System.Environment.NewLine)
            sIn.Write("exit" & System.Environment.NewLine)
            s = sOut.ReadToEnd()

            If Not myProcess.HasExited Then
                myProcess.Kill()
            End If



            sIn.Close()
            sOut.Close()
            sErr.Close()
            myProcess.Close()


        Catch ex As Exception
            Error_Handler(ex)
        End Try
        Return s
    End Function

    Private Sub WorkerConnectionTest()

        Dim conn As OleDb.OleDbConnection = Get_Connection()

        Try
            serverstatus.ForeColor = Color.Orange
            serverstatus.Text = "SQL Server Status: Checking"
            conn.Open()
            conn.Close()
            conn.Dispose()
            serverstatus.ForeColor = Color.Green
            serverstatus.Text = "SQL Server Status: Online"
            result = "Server Check Succeeded"
        Catch ex As Exception
            serverstatus.ForeColor = Color.Red
            serverstatus.Text = "SQL Server Status: Offline"
            Error_Handler(ex)
            result = "Server Check Failed"
        Finally

            conn.Dispose()
            RaiseEvent WorkerComplete(result)
        End Try

    End Sub

    Public Sub WorkerUpdateUserType()
        Try
            Dim conn As OleDb.OleDbConnection
            Try
                conn = Get_Connection()
                conn.Open()

                Dim sql As OleDb.OleDbCommand
                sql = New OleDb.OleDbCommand
                sql.CommandText = "UPDATE [User_Type] SET [User_ID] = '" & ActivityLog.User_ID & "', [User_Description] = '" & ActivityLog.User_Description & "', [User_Personal_Details_ID_Descriptor] = '" & ActivityLog.User_Personal_Details_ID_Descriptor & "' WHERE [User_ID] = '" & ActivityLog.User_ID & "'"
                sql.Connection = conn
                result = sql.ExecuteNonQuery().ToString & " Update Succeeded"
                sql.Dispose()

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Update Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Update Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Public Sub WorkerUpdateCallType()
        Try
            Dim conn As OleDb.OleDbConnection
            Try
                conn = Get_Connection()
                conn.Open()

                Dim sql As OleDb.OleDbCommand
                sql = New OleDb.OleDbCommand
                sql.CommandText = "UPDATE [Call_Type] SET [Call_ID] = '" & ActivityLog.Call_ID & "', [Call_Description] = '" & ActivityLog.Call_Description & "' WHERE [Call_ID] = '" & ActivityLog.Call_ID & "'"
                sql.Connection = conn
                result = sql.ExecuteNonQuery().ToString & " Update Succeeded"
                sql.Dispose()

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Update Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Update Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Public Sub WorkerUpdateCallSubType()
        Try
            Dim conn As OleDb.OleDbConnection
            Try
                conn = Get_Connection()
                conn.Open()

                Dim sql As OleDb.OleDbCommand
                sql = New OleDb.OleDbCommand
                sql.CommandText = "UPDATE [Call_Sub_Type] SET [Call_Sub_ID] = '" & ActivityLog.Call_Sub_ID & "', [Call_Sub_Description] = '" & ActivityLog.Call_Sub_Description & "', [Call_Sub_Call_ID] = '" & ActivityLog.Call_Sub_Call_ID & "' WHERE [Call_Sub_ID] = '" & ActivityLog.Call_Sub_ID & "'"
                sql.Connection = conn
                result = sql.ExecuteNonQuery().ToString & " Update Succeeded"
                sql.Dispose()

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Update Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Update Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub





    Public Sub WorkerDeleteUserType()
        Try
            Dim conn As OleDb.OleDbConnection
            Try
                conn = Get_Connection()
                conn.Open()

                Dim sql As OleDb.OleDbCommand
                sql = New OleDb.OleDbCommand
                sql.CommandText = "DELETE FROM [User_Type] WHERE [User_ID] = '" & ActivityLog.User_ID & "'"
                sql.Connection = conn
                result = sql.ExecuteNonQuery().ToString & " Delete Succeeded"
                sql.Dispose()

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Delete Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Delete Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Public Sub WorkerDeleteCallType()
        Try
            Dim conn As OleDb.OleDbConnection
            Try
                conn = Get_Connection()
                conn.Open()

                Dim sql As OleDb.OleDbCommand
                sql = New OleDb.OleDbCommand
                sql.CommandText = "DELETE FROM [Call_Type] WHERE [Call_ID] = '" & ActivityLog.Call_ID & "'"
                sql.Connection = conn
                result = sql.ExecuteNonQuery().ToString & " Delete Succeeded"
                sql.Dispose()

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Delete Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Delete Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Public Sub WorkerDeleteCallSubType()
        Try
            Dim conn As OleDb.OleDbConnection
            Try
                conn = Get_Connection()
                conn.Open()

                Dim sql As OleDb.OleDbCommand
                sql = New OleDb.OleDbCommand
                sql.CommandText = "DELETE FROM [Call_Sub_Type] WHERE [Call_Sub_ID] = '" & ActivityLog.Call_Sub_ID & "'"
                sql.Connection = conn
                result = sql.ExecuteNonQuery().ToString & " Delete Succeeded"
                sql.Dispose()

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Delete Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Delete Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Public Sub WorkerInsertUserType()
        Try

            Dim conn As OleDb.OleDbConnection = Get_Connection()
            conn.Open()
            Try
                Dim Log_ID As String = ""
                Dim temp_ID As String = ""
                Dim lngtemp_id As Long = -1
                Dim insertlog As Boolean = False
                Dim counter As Integer = 0
                Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                Dim datareader As OleDb.OleDbDataReader

                sql = New OleDb.OleDbCommand
                sql.CommandText = "Select max([User_ID]) as maxid from [User_Type]"
                sql.Connection = conn

                datareader = sql.ExecuteReader(CommandBehavior.Default)
                If datareader.HasRows = True Then
                    If datareader.Read = True Then
                        If Not datareader.Item("maxid").GetType.ToString.Equals("System.DBNull") Then
                            temp_ID = (datareader.Item("maxid"))

                            lngtemp_id = CLng(temp_ID.Remove(0, 2))
                            lngtemp_id = lngtemp_id + 1
                            Log_ID = lngtemp_id.ToString
                        Else
                            Log_ID = "1"
                        End If

                        While Log_ID.Length < 8
                            Log_ID = "0" & Log_ID
                        End While
                        Log_ID = "UT" & Log_ID
                        ActivityLog.User_ID = Log_ID
                        insertlog = True
                    End If
                End If
                datareader.Close()
                sql.Dispose()

                If insertlog = True Then
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "INSERT INTO [User_Type] ([User_ID], [User_Description], [User_Personal_Details_ID_Descriptor]) VALUES ('" & ActivityLog.User_ID & "','" & ActivityLog.User_Description & "','" & ActivityLog.User_Personal_Details_ID_Descriptor & "')"
                    sql.Connection = conn
                    result = sql.ExecuteNonQuery().ToString & " Insert Succeeded"
                    sql.Dispose()
                End If

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Insert Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Insert Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Public Sub WorkerInsertCallType()
        Try

            Dim conn As OleDb.OleDbConnection = Get_Connection()
            conn.Open()
            Try
                Dim Log_ID As String = ""
                Dim temp_ID As String = ""
                Dim lngtemp_id As Long = -1
                Dim insertlog As Boolean = False
                Dim counter As Integer = 0
                Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                Dim datareader As OleDb.OleDbDataReader

                sql = New OleDb.OleDbCommand
                sql.CommandText = "Select max([Call_ID]) as maxid from [Call_Type]"
                sql.Connection = conn

                datareader = sql.ExecuteReader(CommandBehavior.Default)
                If datareader.HasRows = True Then
                    If datareader.Read = True Then
                        If Not datareader.Item("maxid").GetType.ToString.Equals("System.DBNull") Then
                            temp_ID = (datareader.Item("maxid"))

                            lngtemp_id = CLng(temp_ID.Remove(0, 2))
                            lngtemp_id = lngtemp_id + 1
                            Log_ID = lngtemp_id.ToString
                        Else
                            Log_ID = "1"
                        End If

                        While Log_ID.Length < 8
                            Log_ID = "0" & Log_ID
                        End While
                        Log_ID = "UT" & Log_ID
                        ActivityLog.Call_ID = Log_ID
                        insertlog = True
                    End If
                End If
                datareader.Close()
                sql.Dispose()

                If insertlog = True Then
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "INSERT INTO [Call_Type] ([Call_ID], [Call_Description]) VALUES ('" & ActivityLog.Call_ID & "','" & ActivityLog.Call_Description & "')"
                    sql.Connection = conn
                    result = sql.ExecuteNonQuery().ToString & " Insert Succeeded"
                    sql.Dispose()
                End If

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Insert Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Insert Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Public Sub WorkerInsertCallSubType()
        Try

            Dim conn As OleDb.OleDbConnection = Get_Connection()
            conn.Open()
            Try
                Dim Log_ID As String = ""
                Dim temp_ID As String = ""
                Dim lngtemp_id As Long = -1
                Dim insertlog As Boolean = False
                Dim counter As Integer = 0
                Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                Dim datareader As OleDb.OleDbDataReader

                sql = New OleDb.OleDbCommand
                sql.CommandText = "Select max([Call_Sub_ID]) as maxid from [Call_Sub_Type]"
                sql.Connection = conn

                datareader = sql.ExecuteReader(CommandBehavior.Default)
                If datareader.HasRows = True Then
                    If datareader.Read = True Then
                        If Not datareader.Item("maxid").GetType.ToString.Equals("System.DBNull") Then
                            temp_ID = (datareader.Item("maxid"))

                            lngtemp_id = CLng(temp_ID.Remove(0, 2))
                            lngtemp_id = lngtemp_id + 1
                            Log_ID = lngtemp_id.ToString
                        Else
                            Log_ID = "1"
                        End If

                        While Log_ID.Length < 8
                            Log_ID = "0" & Log_ID
                        End While
                        Log_ID = "UT" & Log_ID
                        ActivityLog.Call_Sub_ID = Log_ID
                        insertlog = True
                    End If
                End If
                datareader.Close()
                sql.Dispose()

                If insertlog = True Then
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "INSERT INTO [Call_Sub_Type] ([Call_Sub_ID], [Call_Sub_Description], [Call_Sub_Call_ID]) VALUES ('" & ActivityLog.Call_Sub_ID & "','" & ActivityLog.Call_Sub_Description & "','" & ActivityLog.Call_Sub_Call_ID & "')"
                    sql.Connection = conn
                    result = sql.ExecuteNonQuery().ToString & " Insert Succeeded"
                    sql.Dispose()
                End If

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 Insert Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 Insert Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub

    Public Sub WorkerExecuteNonQuery()
        Try

            Dim conn As OleDb.OleDbConnection
            Try
                conn = Get_Connection()
                conn.Open()

                Dim sql As OleDb.OleDbCommand
                sql = New OleDb.OleDbCommand
                sql.CommandText = SQLstatement
                sql.Connection = conn
                result = sql.ExecuteNonQuery().ToString & " SQL Statement Succeeded"
                sql.Dispose()

            Catch ex As Exception
                Error_Handler(ex)
                result = "0 SQL Statement Failed"
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        Catch ex As Exception
            Error_Handler(ex)
            result = "0 SQL Statement Failed"
        Finally
            RaiseEvent WorkerComplete(result)
        End Try
    End Sub
End Class
