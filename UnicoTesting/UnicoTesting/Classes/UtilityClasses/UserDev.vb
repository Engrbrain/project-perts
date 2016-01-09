Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DotNetNuke.Security.Membership
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Security.Roles

Public Class UserDev
    Protected username As String
    Private cn As String = System.Configuration.ConfigurationSettings.AppSettings("ConnectionString_Data_Land")

    Public Sub New()

    End Sub

    Public Shared Function Get_Username() As String
        Dim devmode As String = System.Configuration.ConfigurationManager.AppSettings("developementmode")
        Dim devusername As String = ""

        'Dim username As String
        If devmode = "1" Then
            devusername = System.Configuration.ConfigurationManager.AppSettings("devusername")
        Else
            devusername = UserController.GetCurrentUserInfo.Username
        End If
        Return devusername

    End Function

    Public Shared Function Get_Userid() As Integer

        Dim devmode As String = System.Configuration.ConfigurationManager.AppSettings("developementmode")
        Dim devuserid As String = System.Configuration.ConfigurationManager.AppSettings("devuserid")
        If devmode = "1" Then
            devuserid = devuserid
        Else
            devuserid = UserController.GetCurrentUserInfo.UserID
        End If

        Return devuserid
    End Function

    Public Shared Function Get_UserLastname() As String

        Dim devmode As String = System.Configuration.ConfigurationManager.AppSettings("developementmode")
        Dim Lastname As String = ""


        If devmode = "1" Then
            Lastname = System.Configuration.ConfigurationManager.AppSettings("devlastname")
        Else
            Lastname = UserController.GetCurrentUserInfo.LastName
        End If

        Return Lastname
    End Function

    Public Shared Function Get_UserFirstName() As String

        Dim devmode As String = System.Configuration.ConfigurationManager.AppSettings("developementmode")
        Dim firstname As String

        If devmode = "1" Then
            firstname = System.Configuration.ConfigurationManager.AppSettings("devfirstname")

        Else
            firstname = UserController.GetCurrentUserInfo.FirstName
        End If

        Return firstname
    End Function

    'UserController.GetCurrentUserInfo.IsInRole(_leadPanelAdmin)

    Public Shared Function IsInRole(ByVal rolename As String) As Boolean

        Dim devmode As String = System.Configuration.ConfigurationManager.AppSettings("developementmode")
        If devmode = "1" Then
            If rolename = System.Configuration.ConfigurationManager.AppSettings("devrolename") Then
                Return True
            End If
        Else
            Return UserController.GetCurrentUserInfo.IsInRole(rolename)
        End If

        Return False
    End Function

    Public Shared Function Get_UserEmail() As String

        Dim devmode As String = System.Configuration.ConfigurationManager.AppSettings("developementmode")
        Dim email As String

        If devmode = "1" Then
            email = System.Configuration.ConfigurationManager.AppSettings("devemail")

        Else
            email = UserController.GetCurrentUserInfo.Email
        End If

        Return email
    End Function


    Public Shared Function Get_UserPassword() As String

        Dim devmode As String = System.Configuration.ConfigurationManager.AppSettings("developementmode")
        Dim password As String

        If devmode = "1" Then
            password = System.Configuration.ConfigurationManager.AppSettings("devpassword")

        Else
            Dim uInfo As New UserInfo
            password = UserController.GetPassword(uInfo, "")
        End If

        Return password
    End Function





End Class
