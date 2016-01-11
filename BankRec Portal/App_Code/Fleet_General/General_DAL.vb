Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Imports System.Collections.Generic

Public Class General_DAL
    Inherits Entities.Modules.PortalModuleBase
    Dim cn, cn2 As SqlConnection

    Public Sub New()
        cn = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("SiteSqlServer"))
        cn2 = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("AppSqlServer"))
    End Sub

#Region "Activities Log"
    Public Function insertActivity(ByVal activity As APP_ActivitiesLogInfo) As Boolean
        Dim ret As Boolean = False
        Try
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            Dim params() As SqlParameter = {New SqlParameter("@actionid", activity.Modules_ActionsID), _
                                            New SqlParameter("@userid", activity.UserID), _
                                            New SqlParameter("@actiondate", activity.ActionDate), _
                                            New SqlParameter("@Description", activity.Description), _
                                            New SqlParameter("@actiontime", activity.ActionTime)}
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "APP_InsertxActivityLog", params)
            ret = True
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
        Finally
            cn.Close()
        End Try
        Return ret
    End Function

#End Region

#Region "Users Management"
    'APP_FetchUsersList
    Public Function fetchAllUsers(ByVal BU As String) As DataSet
        Try
            Dim params() As SqlParameter = {New SqlParameter("@BU", BU)}
            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchUsersList", params)
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        Finally
            cn.Close()
        End Try
    End Function


    Public Function fetchAllUsers2(ByVal username As String, ByVal fname As String, ByVal lname As String, ByVal CollegeID As Integer) As DataSet
        Try
            Dim params() As SqlParameter = {New SqlParameter("@username", username), _
                                            New SqlParameter("@fname", fname), _
                                            New SqlParameter("@lname", lname),
                                             New SqlParameter("@SchoolID", CollegeID)}
            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchUsersList2", params)
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        Finally
            cn.Close()
        End Try
    End Function


    Public Function fetchCorpUsers() As DataSet
        Try
            Dim param As SqlParameter = New SqlParameter("@CorpID", CInt(UserInfo.Profile.Cell))
            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchCorpList", param)
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        Finally
            cn.Close()
        End Try
    End Function

    Public Function fetchRoles() As DataSet
        Try
            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchRoles")
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        Finally
            cn.Close()
        End Try
    End Function
    '
    Public Function fetchUserOrganisationTypeID(ByVal SchoolID As Integer) As Integer
        Dim ret As Integer
        Try

            Dim param As SqlParameter = New SqlParameter("@ID", SchoolID)
            Dim ds As DataSet = SqlHelper.ExecuteDataset(cn2, CommandType.StoredProcedure, "sp_Schools_fetchOrgTypeID", param)
            If Not ds Is Nothing And ds.Tables(0).Rows.Count > 0 Then
                ret = CInt(ds.Tables(0).Rows(0).Item(0))
            Else
                ret = 0

            End If
            ds.Dispose()
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            ret = 0
        Finally
            cn.Close()
        End Try
        Return ret
    End Function
    Public Function fetchUserOrganisationInfo(ByVal UserID As Integer) As DataSet
        Try
            Dim param As SqlParameter = New SqlParameter("@UserID", UserID)
            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_Users_Mgmt_FetchUserSchool", param)
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        Finally
            cn.Close()
        End Try
    End Function
    Public Function FetchSchools4UserRegistration() As DataSet
        Try
            Return SqlHelper.ExecuteDataset(cn2, CommandType.StoredProcedure, "sp_Schools_Fetch4UserReg")
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        Finally
            cn2.Close()
        End Try
    End Function


    Public Function InsertUserDetailIntoUserMgmt(ByVal newuser As UserInfo, ByVal rolename As String) As Boolean
        Dim ret As Boolean = False
        Try
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            Dim params() As SqlParameter = {New SqlParameter("@Username", newuser.Username), _
                                            New SqlParameter("@FirstName", newuser.FirstName), _
                                            New SqlParameter("@LastName", newuser.LastName), _
                                            New SqlParameter("@SchoolName", newuser.Membership.PasswordQuestion), _
                                            New SqlParameter("@RoleName", rolename), _
                                            New SqlParameter("@UserID", newuser.UserID), _
                                            New SqlParameter("@Status", 1), _
                                            New SqlParameter("@SchoolID", CInt(newuser.Profile.Cell))}
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "APP_Users_Mgmt_Add", params)
            ret = True
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            ret = False
        Finally
            cn.Close()
        End Try
        Return ret
    End Function
    Public Function InsertUserBUMAnagement(ByVal newuser As UserInfo, ByVal BU As String) As Boolean
        Dim ret As Boolean = False
        Try
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            Dim params() As SqlParameter = {New SqlParameter("@BU", BU),
                                            New SqlParameter("@UserID", newuser.UserID)
                                            }
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "C2G_CreateBUUSER", params)
            ret = True
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            ret = False
        Finally
            cn.Close()
        End Try
        Return ret
    End Function
    Public Function FetchtUserBUMAnagement(ByVal UserId As UserInfo) As String
        Dim ret As Boolean = False
        Dim BU As String = ""
        Try
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            Dim params() As SqlParameter = {
                                            New SqlParameter("@UserID", UserId.UserID)
                                            }
            BU = SqlHelper.ExecuteScalar(cn, CommandType.StoredProcedure, "C2G_FetchBU", params).ToString()
            'ret = True
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            ret = False
        Finally
            cn.Close()
        End Try
        Return BU
    End Function
    Public Function UpdateUserBUMAnagement(ByVal newuser As UserInfo, ByVal BU As String) As Boolean
        Dim ret As Boolean = False

        Try
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            Dim params() As SqlParameter = {New SqlParameter("@BU", BU),
                                            New SqlParameter("@UserID", newuser.UserID)
                                            }
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "C2G_UpdateBUUSER", params).ToString()
            ret = True
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            ret = False
        Finally
            cn.Close()
        End Try
        Return ret
    End Function


    Public Function UpdateUserDetailInUserMgmt(ByVal newuser As UserInfo, ByVal rolename As String) As Boolean
        Dim ret As Boolean = False
        Dim status As Integer

        Try
            If newuser.Membership.Approved Then
                status = 1
            Else
                status = 0
            End If
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If

            Dim params() As SqlParameter = {New SqlParameter("@FirstName", newuser.FirstName), _
                                            New SqlParameter("@LastName", newuser.LastName), _
                                            New SqlParameter("@SchoolName", "N/A"), _
                                            New SqlParameter("@RoleName", rolename), _
                                            New SqlParameter("@UserID", newuser.UserID), _
                                            New SqlParameter("@Status", status), _
                                            New SqlParameter("@SchoolID", newuser.Profile.Cell)}
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "APP_Users_Mgmt_Edit", params)
            ret = True
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            ret = False
        Finally
            cn.Close()
        End Try
        Return ret
    End Function
#End Region

#Region "DashBoard"

    Public Function fetchList2Display(ByVal roleid As Integer) As DataSet

        Try

            Dim params As SqlParameter = New SqlParameter("@RoleID", roleid)

            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "DashBoardList2", params)

        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        End Try
    End Function

    Public Function fetchList() As DataSet
        Try
            'Dim params As SqlParameter = New SqlParameter("@GradeID", ID)

            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "DashBoardList")

        Catch ex As Exception
            System.Diagnostics.Debug.Write(ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function GetDashBoard(ByVal ID As Integer) As DataSet
        Try
            Dim params As SqlParameter = New SqlParameter("@ID", ID)
            'Dim ds As DataSet
            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "DashBoardGet", params)
        Catch ex As Exception
            System.Diagnostics.Debug.Write(ex.Message)
            Return Nothing
        End Try

    End Function
    'fetchPost4Detail
    Public Function Insert_DashBoard(ByVal _DashBoard As DashBoard_TableObject.DashBoardInfo) As Boolean
        Try
            With _DashBoard
                Dim params() As SqlParameter = {New SqlParameter("@Title", .Title), _
                                                New SqlParameter("@Description", .Description), _
                                                New SqlParameter("@PicUrl", .PicUrl), _
                                                New SqlParameter("@DestinationUrl", .DestinationUrl), _
                                                New SqlParameter("@BodyClass", .BodyClass), _
                                                New SqlParameter("@HeaderClass", .HeaderClass), _
                                                New SqlParameter("@Status", .Status), _
                                                New SqlParameter("@Display", .Display), _
                                                New SqlParameter("@RoleID", .RoleID), _
                                                New SqlParameter("@DisplayOrder", .DisplayOrder)}

                'Dim ds As DataSet
                SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DashBoardAdd", params)
                Return True
            End With
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return False
        End Try

    End Function
    Public Function Update_DashBoard(ByVal _DashBoard As DashBoard_TableObject.DashBoardInfo) As Boolean
        Try
            With _DashBoard
                Dim params() As SqlParameter = {New SqlParameter("@ID", .ID), _
                                                New SqlParameter("@Title", .Title), _
                                                New SqlParameter("@Description", .Description), _
                                                New SqlParameter("@PicUrl", .PicUrl), _
                                                New SqlParameter("@DestinationUrl", .DestinationUrl), _
                                                New SqlParameter("@BodyClass", .BodyClass), _
                                                New SqlParameter("@HeaderClass", .HeaderClass), _
                                                New SqlParameter("@Status", .Status), _
                                                New SqlParameter("@Display", .Display), _
                                                New SqlParameter("@DisplayOrder", .DisplayOrder)}

                'Dim ds As DataSet
                SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DashBoardUpdate", params)
                Return True
            End With
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return False
        End Try

    End Function
    Public Function delete_DashBoard(ByVal ID As Integer) As Integer
        Try
            Dim params As SqlParameter = New SqlParameter("@ID", ID)

            Return SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DashBoardDelete", params)
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        End Try

    End Function


#End Region


#Region "Lecture Note And Assignment"

    Public Function FLTStudents_ByIndexNo(ByVal indexno As String, ByVal collegeid As Integer) As DataSet
        Try
            Dim params() As SqlParameter = {New SqlParameter("@indexno", indexno), New SqlParameter("@CollegeID", collegeid)}
            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "[FLTStudents_ByIndexNo]", params)

        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        End Try
    End Function

    Public Function GenerateIDCardByProgrammeAndLevel(ByVal levelid As Integer, ByVal programid As Integer, ByVal collegeid As Integer) As DataSet
        Try
            Dim params() As SqlParameter = {New SqlParameter("@levelid", levelid), New SqlParameter("@programid", programid), New SqlParameter("@CollegeID", collegeid)}
            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "[FLTStudents_ByLevel_ByProgram]", params)

        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        End Try
    End Function

    Public Function GetStaffIDByUsername(ByVal username As String) As Integer
        Try
            Dim params As SqlParameter = New SqlParameter("@username", username)
            Return SqlHelper.ExecuteScalar(cn, CommandType.StoredProcedure, "OLA_StaffIDByUsername", params)
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return 0
        End Try

    End Function

    Public Function FLTCollegeStaff_Courses_Fetch(ByVal username As Integer, ByVal levelID As Integer) As DataSet
        Try
            Dim staffid As New SqlParameter
            Dim _levelid As New SqlParameter

            Return SqlHelper.ExecuteDataset(cn, "FLTCollegeStaff_Courses_Fetch", username, levelID)
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return Nothing
        End Try
    End Function

    Public Function Insert_LectureNote_CheckIfExists(ByVal _LectureNote_Object As LectureNote_Object) As Integer
        Dim ret As Integer
        Try
            With _LectureNote_Object
                Dim params() As SqlParameter = {New SqlParameter("@SessionID", .SessionID), _
                                                New SqlParameter("@UserID", .UserID), _
                                                New SqlParameter("@TeacherID", .TeacherID), _
                                                New SqlParameter("@ProgrammeSubjectID", .SubjectID), _
                                                New SqlParameter("@DateAdded", .DateAdded), _
                                                New SqlParameter("@Week", .Week), _
                                                New SqlParameter("@Url", .Url)}
                ret = CInt(SqlHelper.ExecuteScalar(cn, CommandType.StoredProcedure, "FLTLectureNote_CheckIfExists", params))
                Return ret
            End With
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return -99
        End Try
    End Function

    Public Function Update_LectureNote_CheckIfExists(ByVal _LectureNote_Object As LectureNote_Object) As Integer
        Try
            With _LectureNote_Object
                Dim params() As SqlParameter = {New SqlParameter("@ID", .ID), _
                                                New SqlParameter("@TermID", .TermID), _
                                                New SqlParameter("@UserID", .UserID), _
                                                New SqlParameter("@TeacherID", .TeacherID), _
                                                New SqlParameter("@SubjectID", .SubjectID), _
                                                New SqlParameter("@ClassID", .ClassID), _
                                                New SqlParameter("@DateAdded", .DateAdded), _
                                                New SqlParameter("@Week", .Week), _
                                                New SqlParameter("@Url", .Url)}
                Return CInt(SqlHelper.ExecuteScalar(cn, CommandType.StoredProcedure, "OLA_CheckLectureNoteUpdate", params))
            End With
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return 0
        End Try

    End Function

    Public Function Insert_LectureNote(ByVal _LectureNote_Object As LectureNote_Object, ByVal FileUploadObject As FileUpload) As Boolean

        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim sqlTran As SqlTransaction = cn.BeginTransaction()
        Try
            'upload doc
            Dim strDate, strTime As String
            Dim strNewFilename As String
            strDate = Format(CDate(DateAndTime.Now.ToShortDateString.ToUpper), "ddMMyyyy")
            strTime = Format(DateAndTime.Now, "hhmmss").ToUpper

            Dim fileExtension As String
            Dim path As String = HttpContext.Current.Request.MapPath("~/LectureNotes/")
            Dim f As DirectoryInfo = New DirectoryInfo(path)
            If (f.Exists = False) Then
                f.Create()
            End If

            With _LectureNote_Object
                fileExtension = System.IO.Path.GetExtension(FileUploadObject.FileName).ToLower()
                strNewFilename = String.Format("{0}_{1}_{2}{3}", .SessionID, .Week, System.Guid.NewGuid.ToString(), fileExtension)
                FileUploadObject.PostedFile.SaveAs(path & strNewFilename)
                .Url = strNewFilename
                Dim params() As SqlParameter = {New SqlParameter("@SessionID", .SessionID), _
                                                New SqlParameter("@UserID", .UserID), _
                                                New SqlParameter("@TeacherID", .TeacherID), _
                                                New SqlParameter("@ProgrammeSubjectID", .SubjectID), _
                                                New SqlParameter("@DateAdded", .DateAdded), _
                                                New SqlParameter("@Week", .Week), _
                                                New SqlParameter("@Url", .Url)}

                SqlHelper.ExecuteNonQuery(sqlTran, CommandType.StoredProcedure, "FLTLectureNote_Insert", params)
                sqlTran.Commit()
                Return True
            End With
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            sqlTran.Rollback()
            Return False
        End Try

    End Function

    Public Function Update_LectureNote(ByVal _LectureNote_Object As LectureNote_Object) As Boolean
        Try
            With _LectureNote_Object
                Dim params() As SqlParameter = {New SqlParameter("@ID", .ID), _
                                                New SqlParameter("@TermID", .TermID), _
                                                New SqlParameter("@UserID", .UserID), _
                                                New SqlParameter("@TeacherID", .TeacherID), _
                                                New SqlParameter("@SubjectID", .SubjectID), _
                                                New SqlParameter("@ClassID", .ClassID), _
                                                New SqlParameter("@DateAdded", .DateAdded), _
                                                New SqlParameter("@Week", .Week), _
                                                New SqlParameter("@Url", .Url)}
                SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "OLA_LectureNoteUpdate", params)
                Return True
            End With
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return False
        End Try

    End Function

    Public Function Fetch_LectureNotes(ByVal Teacher_ID As Integer, ByVal SessionID As Integer, ByVal Subject_ID As Integer) As DataSet
        Try

            Dim params() As SqlParameter = {New SqlParameter("@Teacher_ID", Teacher_ID), _
                                                 New SqlParameter("@SessionID", SessionID), _
                                                 New SqlParameter("@ProgrammeSubjectID", Subject_ID)}

            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "FLTLectureNote_Fetch", params)
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return Nothing
        End Try

    End Function

    Public Function Fetch_LectureNotes4Student(ByVal SessionID As Integer, ByVal Subject_ID As Integer) As DataSet
        Try

            Dim params() As SqlParameter = {New SqlParameter("@SessionID", SessionID), _
                                            New SqlParameter("@ProgrammeSubjectID", Subject_ID)}

            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "FLTLectureNote_FetchByStudent", params)
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return Nothing
        End Try

    End Function

    Public Function Fetch_Assignment4Student(ByVal Subject_ID As String) As DataSet
        Try

            Dim params() As SqlParameter = {New SqlParameter("@CDate", Now.ToString("yyyy/MM/dd")), _
                                                 New SqlParameter("@ProgrammeSubjectID", Subject_ID)}

            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "FLTAssignment_FetchByStudent", params)
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return Nothing
        End Try

    End Function

    Public Function Delete_LectureNotes(ByVal ID As Integer) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(cn, "FLTLectureNote_Delete", ID)
            Return True
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return False
        End Try
    End Function

    Public Function Delete_Assignment(ByVal ID As Integer) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(cn, "FLTAssignment_Delete", ID)
            Return True
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return False
        End Try
    End Function

    Public Function Fetch_Assignment(ByVal Teacher_ID As Integer, ByVal Subject_ID As Integer, ByVal SessionID As Integer) As DataSet
        Try

            Dim params() As SqlParameter = {New SqlParameter("@Teacher_ID", Teacher_ID), _
                                                 New SqlParameter("@SessionID", SessionID), _
                                                 New SqlParameter("@ProgrammeSubjectID", Subject_ID)}

            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "FLTAssignment_Fetch", params)
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return Nothing
        End Try

    End Function

    Public Function Fetch_Assignment_By_ID(ByVal ID As Integer) As DataSet
        Try

            Dim params() As SqlParameter = {New SqlParameter("@ID", ID)}

            Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "FLTAssignment_FetchByID", params)
        Catch ex As Exception
            'Write Error into log File
            General_BLL.WriteLog(ex.Message + " :" + ex.StackTrace)
            Return Nothing
        End Try

    End Function


    'Public Function Insert_Assignment_CheckIfExists(ByVal _Assignment_Object As Assignment_Object) As Integer
    '    Try
    '        With _Assignment_Object
    '            Dim params() As SqlParameter = {New SqlParameter("@TeacherID", .TeacherID), _
    '                                            New SqlParameter("@ArmID", .ArmID), _
    '                                            New SqlParameter("@SubjectID", .SubjectID), _
    '                                            New SqlParameter("@SubmissionDate", .SubmissionDate), _
    '                                            New SqlParameter("@AssignmentType", .AssignmentType), _
    '                                            New SqlParameter("@AssignmentTitle", .AssignmentTitle), _
    '                                            New SqlParameter("@Assignment", .Assignment), _
    '                                            New SqlParameter("@DateCreated", .DateCreated), _
    '                                            New SqlParameter("@TimeCreated", .TimeCreated)}

    '            Return CInt(SqlHelper.ExecuteScalar(cn, CommandType.StoredProcedure, "UDO_sp_LectureNote_CheckIfExist", params))
    '        End With
    '    Catch ex As Exception
    '        General_BLL.WriteLog(ex.Message + ex.StackTrace)
    '        Return 0
    '    End Try

    'End Function

    'Public Function Update_Assignment_CheckIfExists(ByVal _Assignment_Object As Assignment_Object) As Integer
    '    Try
    '        With _Assignment_Object
    '            Dim params() As SqlParameter = {New SqlParameter("@ID", .ID), _
    '                                            New SqlParameter("@TeacherID", .TeacherID), _
    '                                            New SqlParameter("@ArmID", .ArmID), _
    '                                            New SqlParameter("@SubjectID", .SubjectID), _
    '                                            New SqlParameter("@SubmissionDate", .SubmissionDate), _
    '                                            New SqlParameter("@AssignmentType", .AssignmentType), _
    '                                            New SqlParameter("@AssignmentTitle", .AssignmentTitle), _
    '                                            New SqlParameter("@Assignment", .Assignment), _
    '                                            New SqlParameter("@DateCreated", .DateCreated), _
    '                                            New SqlParameter("@TimeCreated", .TimeCreated)}

    '            Return CInt(SqlHelper.ExecuteScalar(cn, CommandType.StoredProcedure, "UDO_sp_LectureNote_Update_CheckIfExist", params))
    '        End With
    '    Catch ex As Exception
    '        General_BLL.WriteLog(ex.Message + ex.StackTrace)
    '        Return 0
    '    End Try

    'End Function

    Public Function Insert_Assignment(ByVal _Assignment_Object As Assignment_Object, ByVal FileUploadObject As FileUpload) As Boolean
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim sqlTran As SqlTransaction = cn.BeginTransaction()
        Try
            'upload doc
            Dim strDate, strTime As String
            Dim strNewFilename As String
            strDate = Format(CDate(DateAndTime.Now.ToShortDateString.ToUpper), "ddMMyyyy")
            strTime = Format(DateAndTime.Now, "hhmmss").ToUpper

            Dim fileExtension As String
            Dim path As String = HttpContext.Current.Request.MapPath("~/Assignments/")
            Dim f As DirectoryInfo = New DirectoryInfo(path)
            If (f.Exists = False) Then
                f.Create()
            End If

            With _Assignment_Object
                If .AssignmentType = 2 Then
                    fileExtension = System.IO.Path.GetExtension(FileUploadObject.FileName).ToLower()
                    strNewFilename = String.Format("{0}_{1}_{2}{3}", .SessionID, strDate, System.Guid.NewGuid.ToString(), fileExtension)
                    FileUploadObject.PostedFile.SaveAs(path & strNewFilename)
                    .Assignment = strNewFilename
                End If

                Dim params() As SqlParameter = {New SqlParameter("@SessionID", .SessionID), _
                                                New SqlParameter("@UserID", .UserID), _
                                                New SqlParameter("@TeacherID", .TeacherID), _
                                                New SqlParameter("@ProgrammeSubjectID", .SubjectID), _
                                                New SqlParameter("@SubmissionDate", .SubmissionDate), _
                                                New SqlParameter("@AssignmentType", .AssignmentType), _
                                                New SqlParameter("@AssignmentTitle", .AssignmentTitle), _
                                                New SqlParameter("@Assignment", .Assignment), _
                                                New SqlParameter("@DateCreated", .DateCreated), _
                                                New SqlParameter("@TimeCreated", .TimeCreated)}

                SqlHelper.ExecuteNonQuery(sqlTran, CommandType.StoredProcedure, "FLTAssignment_Insert", params)
                sqlTran.Commit()
                Return True
            End With
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            sqlTran.Rollback()
            Return False
        End Try

    End Function

    Public Function Update_Assignment(ByVal _Assignment_Object As Assignment_Object, ByVal FileUploadObject As FileUpload) As Boolean
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim sqlTran As SqlTransaction = cn.BeginTransaction()
        Try
            'upload doc
            Dim strDate, strTime As String
            Dim strNewFilename As String
            strDate = Format(CDate(DateAndTime.Now.ToShortDateString.ToUpper), "ddMMyyyy")
            strTime = Format(DateAndTime.Now, "hhmmss").ToUpper

            Dim fileExtension As String
            Dim path As String = HttpContext.Current.Request.MapPath("~/Assignments/")
            Dim f As DirectoryInfo = New DirectoryInfo(path)
            If (f.Exists = False) Then
                f.Create()
            End If

            With _Assignment_Object
                If .AssignmentType = 2 Then
                    If FileUploadObject.HasFile Then
                        fileExtension = System.IO.Path.GetExtension(FileUploadObject.FileName).ToLower()
                        strNewFilename = String.Format("{0}_{2}_{3}{4}", .SessionID, strDate, System.Guid.NewGuid.ToString(), fileExtension)
                        FileUploadObject.PostedFile.SaveAs(path & strNewFilename)
                        .Assignment = strNewFilename
                    End If
                End If

                Dim params() As SqlParameter = {New SqlParameter("@ID", .ID), _
                                                New SqlParameter("@SessionID", .SessionID), _
                                                New SqlParameter("@UserID", .UserID), _
                                                New SqlParameter("@TeacherID", .TeacherID), _
                                                New SqlParameter("@ProgrammeSubjectID", .SubjectID), _
                                                New SqlParameter("@SubmissionDate", .SubmissionDate), _
                                                New SqlParameter("@AssignmentType", .AssignmentType), _
                                                New SqlParameter("@AssignmentTitle", .AssignmentTitle), _
                                                New SqlParameter("@Assignment", .Assignment), _
                                                New SqlParameter("@DateCreated", .DateCreated), _
                                                New SqlParameter("@TimeCreated", .TimeCreated)}

                SqlHelper.ExecuteNonQuery(sqlTran, CommandType.StoredProcedure, "FLTAssignment_Update", params)
                sqlTran.Commit()
                Return True
            End With
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            sqlTran.Rollback()
            Return False
        End Try

    End Function


#End Region

End Class


