﻿Imports System.Net

Public Class frmAlertConfig

    Public Sub New(ByVal svrLst As List(Of GroupInfo.ServerInfo))

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()


        btnSave.Text = p_clsMsgData.fn_GetData("F014")
        btnClose.Text = p_clsMsgData.fn_GetData("F021")
        btnHealthInit.Text = p_clsMsgData.fn_GetData("F226")

        ' 일반설정 탭 
        Me.Text = p_clsMsgData.fn_GetData("F199")
        Me.StatusLabel.Text = p_clsMsgData.fn_GetData("M048")

        ' Me.tbMain.TabPages.Clear()

        Dim ts As eXperDB.ODBC.structConnection = modCommon.AgentInfoRead()
        Dim dbType As DXODBC.enumODBCType = IIf(System.Environment.Is64BitProcess, eXperDB.ODBC.DXODBC.enumODBCType.PostgreUnicodeX64, eXperDB.ODBC.DXODBC.enumODBCType.PostgreUnicode)
        Dim tmpCn As New DXODBC(dbtype, ts.HostIP, ts.Port, ts.UserID, ts.Password, ts.DBName)
        Dim ClsQuery As New clsQuerys(tmpCn)
        Dim dt1 As DataTable = ClsQuery.SelectIniFixedThreshold()


        For i As Integer = 0 To svrLst.Count - 1

            Dim dt As DataTable = ClsQuery.SelectHealthLimited(svrLst.Item(i).InstanceID)
            Me.tbMain.TabPages.Add(svrLst.Item(i).ShowNm, svrLst.Item(i).ShowNm)
            Dim usrContorl As New AlertConfiguration(svrLst.Item(i).InstanceID, dt, dt1)
            usrContorl.Dock = DockStyle.Fill
            usrContorl.Tag = svrLst.Item(i).InstanceID
            usrContorl.Name = "AlertConfig" ' & svrLst.Item(i).InstanceID
            tbMain.TabPages(tbMain.TabPages.Count - 1).Controls.Add(usrContorl)
            'tbMain.TabPages.Item(i).BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
            If i = 0 Then
                tbMain.TabPages.Item(i).BackColor = System.Drawing.Color.Gray
            Else
                tbMain.TabPages.Item(i).BackColor = System.Drawing.Color.DimGray
            End If
            tbMain.TabPages.Item(i).ForeColor = System.Drawing.Color.White
            tbMain.TabPages.Item(i).Font = New Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular)
        Next

        ' modCommon.FontChange(Me, p_Font)
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click




        Dim ts As eXperDB.ODBC.structConnection = modCommon.AgentInfoRead()
        Dim dbType As DXODBC.enumODBCType = IIf(System.Environment.Is64BitProcess, eXperDB.ODBC.DXODBC.enumODBCType.PostgreUnicodeX64, eXperDB.ODBC.DXODBC.enumODBCType.PostgreUnicode)
        Dim tmpCn As New DXODBC(dbtype, ts.HostIP, ts.Port, ts.UserID, ts.Password, ts.DBName)
        Dim ClsQuery As New clsQuerys(tmpCn)
        Dim dt1 As DataTable = ClsQuery.SelectIniFixedThreshold()


        Dim tmpctl As AlertConfiguration = tbMain.SelectedTab.Controls.Find("AlertConfig", False)(0)
        Dim HealtLimited As AlertConfiguration.GetServerAlertConfig = tmpctl.GetValue(dt1)
        Dim InstanceID As Integer = HealtLimited.InstanceID
        Dim LastIp As String = GetLocalIP()

        'Dim Fixed_NULL As String = "NULL"
        ClsQuery.UpdateHealthLimited(InstanceID, "BUFFERHITRATIO", HealtLimited.BufferRatioWarning, HealtLimited.BufferRatioNormal, GetCriticalThreshold_2(HealtLimited.BufferRatioWarning, HealtLimited.BufferRatioNormal), LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "COMMITRATIO", HealtLimited.CommitRatioWarning, HealtLimited.CommitRatioNormal, GetCriticalThreshold_2(HealtLimited.CommitRatioWarning, HealtLimited.CommitRatioNormal), LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "ACTIVECONNECTION", HealtLimited.ConnectionsNormal, HealtLimited.ConnectionsWarning, GetCriticalThreshold_1(HealtLimited.ConnectionsNormal, HealtLimited.ConnectionsWarning), LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "CPUWAIT", HealtLimited.CPUwaitRatioNormal, HealtLimited.CPUwaitRatioWarning, GetCriticalThreshold_1(HealtLimited.CPUwaitRatioNormal, HealtLimited.CPUwaitRatioWarning), LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "SWAPUSAGE", HealtLimited.SWAPusedRatioNormal, HealtLimited.SWAPusedRatioWarning, GetCriticalThreshold_1(HealtLimited.SWAPusedRatioNormal, HealtLimited.SWAPusedRatioWarning), LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "DISKUSAGE", HealtLimited.DiskusedRatioNormal, HealtLimited.DiskusedRatioWarning, GetCriticalThreshold_1(HealtLimited.DiskusedRatioNormal, HealtLimited.DiskusedRatioWarning), LastIp)

        'Dim Fixed As Integer = 1
        ClsQuery.UpdateHealthLimited(InstanceID, "LOCKCNT", HealtLimited.LockedTrancCnt, 0, HealtLimited.LockedtranccntBool, LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "TRAXIDLECNT", HealtLimited.IdleTransCnt, 0, HealtLimited.IdletranscntBool, LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "LONGRUNSQL", HealtLimited.LongRunSqlSec, 0, HealtLimited.LongrunsqlsecBool, LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "UNUSEDINDEX", HealtLimited.UnusedIndexCnt, 0, HealtLimited.UnusedindexcntBool, LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "LASTVACUUM", HealtLimited.LastVacuumDay, 0, HealtLimited.LastvacuumDayBool, LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "LASTANALYZE", HealtLimited.LastAnalyzeDay, 0, HealtLimited.LastAnalyzedayBool, LastIp)
        ClsQuery.UpdateHealthLimited(InstanceID, "CONNECTIONFAIL", 0, HealtLimited.ConFailedCnt, HealtLimited.ConfailedcntBool, LastIp)

        MsgBox(p_clsMsgData.fn_GetData("M028"))

    End Sub

    Public Function GetCriticalThreshold_1(ByVal Normal As Integer, ByVal Warning As Integer) As String
        Dim CriticalThreshold As String = String.Empty

        'If (Normal = 100 And Warning = 100) Then
        '    CriticalThreshold = "9"
        'ElseIf (Normal = Warning) Then
        '    CriticalThreshold = "2"
        'ElseIf (Warning = 100) Then
        '    CriticalThreshold = "1"
        'Else
        '    CriticalThreshold = "0"
        'End If


        If Normal = Warning Then
            If Normal = 100 Then
                CriticalThreshold = "9"
            ElseIf Normal = 0 Then
                CriticalThreshold = "2"
            Else
                CriticalThreshold = "0"
            End If
        Else
            If Normal = 0 AndAlso Warning = 100 Then
                CriticalThreshold = "1"
            Else
                CriticalThreshold = "0"
            End If

        End If

        Return CriticalThreshold
    End Function


    Public Function GetCriticalThreshold_2(ByVal Warning As Integer, ByVal Normal As Integer) As String
        Dim CriticalThreshold As String = String.Empty

        If Normal = Warning Then
            If Normal = 0 Then
                CriticalThreshold = "9"
            ElseIf Normal = 100 Then
                CriticalThreshold = "2"
            Else
                CriticalThreshold = "0"
            End If
        Else
            If Normal = 0 AndAlso Warning = 100 Then
                CriticalThreshold = "1"
            Else
                CriticalThreshold = "0"
            End If

        End If

        'If (Normal = 0 And Warning = 0) Then
        '    CriticalThreshold = "9"
        'ElseIf (Normal = Warning) Then
        '    CriticalThreshold = "2"
        'ElseIf (Normal = 0) Then
        '    CriticalThreshold = "1"
        'Else
        '    CriticalThreshold = "0"
        'End If

        Return CriticalThreshold
    End Function


    'Private Sub AlertConfiguration1_ABCD(sender As Object, UsrEvent As AlertConfiguration.GetServerAlertConfig) Handles AlertConfiguration1.ABCD

    '    MsgBox(UsrEvent.InstanceID & vbCrLf & UsrEvent.BufferRatioNormal & vbCrLf & UsrEvent.BufferRatioWarning)

    'End Sub

    Public Function GetLocalIP() As String
        Dim ipEntry As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName)
        Dim IpAddr() As IPAddress = ipEntry.AddressList
        Dim ip As String = String.Empty

        For Each addr As IPAddress In ipEntry.AddressList
            If addr.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                Dim ipAddress As String = addr.ToString
                ip = ipAddress
                Exit For
            End If
        Next

        If ip = "" Then
            Throw New Exception("No 10. IP found!")
        End If

        Return ip

    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnHealthInit_Click(sender As Object, e As EventArgs) Handles btnHealthInit.Click
        Dim ts As eXperDB.ODBC.structConnection = modCommon.AgentInfoRead()
        Dim dbType As DXODBC.enumODBCType = IIf(System.Environment.Is64BitProcess, eXperDB.ODBC.DXODBC.enumODBCType.PostgreUnicodeX64, eXperDB.ODBC.DXODBC.enumODBCType.PostgreUnicode)
        Dim tmpCn As New DXODBC(dbtype, ts.HostIP, ts.Port, ts.UserID, ts.Password, ts.DBName)
        Dim ClsQuery As New clsQuerys(tmpCn)
        Dim dt As DataTable = ClsQuery.SelectHealthLimited(-1)

        Dim tmpctl As AlertConfiguration = tbMain.SelectedTab.Controls.Find("AlertConfig", False)(0)

        tmpctl.Setvalue(dt)
        'Dim HealtLimited As AlertConfiguration.GetServerAlertConfig = tmpctl.GetValue
        'Dim InstanceID As Integer = HealtLimited.InstanceID
    End Sub

    Private Sub tbMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbMain.SelectedIndexChanged
        'tbMain.TabPages(tbMain.SelectedIndex).Font = New Font("굴림체", tbMain.TabPages(tbMain.SelectedIndex).Font.Size, System.Drawing.FontStyle.Bold)
        For i As Integer = 0 To tbMain.TabCount - 1
            If i = tbMain.SelectedIndex Then
                tbMain.TabPages.Item(i).BackColor = System.Drawing.Color.Gray
            Else
                tbMain.TabPages.Item(i).BackColor = System.Drawing.Color.DimGray
            End If
        Next
    End Sub
End Class