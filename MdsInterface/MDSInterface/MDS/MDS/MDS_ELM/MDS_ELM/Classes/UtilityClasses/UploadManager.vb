Imports System
Imports System.Web
Imports System.Diagnostics
Imports System.IO

Public Class UploadManager
    
    Public Function UploadFile(ByRef f As System.Web.UI.WebControls.FileUpload, ByRef message As String) As Boolean
        If Not f.HasFile Then
            message = "No file is selected."
            Return False
        End If

        Dim sessionid As String

        ' Get the current HTTPContext
        Dim context As HttpContext = HttpContext.Current
        sessionid = context.Session.SessionID

        Dim fileExtension As String = System.IO.Path.GetExtension(f.FileName).ToLower()
        Dim allowedExtensions As String() = {".jpeg", ".jpg", ".bmp", ".png"}
        Dim counter As Integer

        For counter = 0 To allowedExtensions.Length Step 1
            Try
                If fileExtension = allowedExtensions(counter) Then
                    Dim myImage As System.Drawing.Bitmap = New System.Drawing.Bitmap(90, 80)

                    Try
                        Dim path As String = context.Server.MapPath("~/UploadedImages/")
                        Dim filename As String = sessionid & Now.Millisecond.ToString & ".jpg"
                        Dim fullpath As String = path & filename
                        f.PostedFile.SaveAs(fullpath)

                        myImage = New System.Drawing.Bitmap(fullpath)
                        'Dim ImgSize As Integer = myImage.( )


                        ' If (myImage.Width >= 70 And myImage.Width <= 110) And (myImage.Height >= 65 And myImage.Height <= 120) Then
                        'message = "/UploadedImages" + filename
                        message = "~/UploadedImages/" + filename
                        Return True


                    Catch ex As Exception
                        AppException.LogError(ex.Message, ex.StackTrace.ToString)
                        message = "Image could not be uploaded at the moment."
                        Return False
                    Finally
                        myImage.Dispose()
                    End Try

                End If



            Catch ex As Exception
                message = "Invalid File Type."
                Return False
            End Try

        Next

        message = "Invalid Picture format! The Passport Photograph must a jpeg."
        Return False

    End Function

    Public Function UploadDocuments(ByRef f As System.Web.UI.WebControls.FileUpload, ByRef message As String) As Boolean
        Try

       
            If Not f.HasFile Then
                message = "No file is selected."
                Return False
            End If

            Dim sessionid As String

            ' Get the current HTTPContext
            Dim context As HttpContext = HttpContext.Current
            sessionid = context.Session.SessionID

            Dim fileExtension As String = System.IO.Path.GetExtension(f.FileName).ToLower()
            Dim allowedExtensions As String() = {".jpeg", ".jpg", ".bmp", ".png", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt"}
            Dim counter As Integer

            For counter = 0 To allowedExtensions.Length Step 1
                Try
                    If fileExtension = allowedExtensions(counter) Then
                        Dim myImage As System.Drawing.Bitmap = New System.Drawing.Bitmap(90, 80)

                        Try
                            Dim path As String = context.Server.MapPath("~/UploadedImages/")
                            Dim filename As String = sessionid & Now.Millisecond.ToString & fileExtension
                            Dim fullpath As String = path & filename
                            f.PostedFile.SaveAs(fullpath)

                            ' myImage = New System.Drawing.Bitmap(fullpath)
                            'Dim ImgSize As Integer = myImage.( )


                            ' If (myImage.Width >= 70 And myImage.Width <= 110) And (myImage.Height >= 65 And myImage.Height <= 120) Then
                            'message = "/UploadedImages" + filename
                            message = "~/UploadedImages/" + filename
                            Return True


                        Catch ex As Exception
                            AppException.LogError(ex.Message, ex.StackTrace.ToString)
                            message = "Image could not be uploaded at the moment."
                            Return False
                        Finally
                            myImage.Dispose()
                        End Try

                    End If



                Catch ex As Exception
                    message = "Invalid File Type."
                    Return False
                End Try

            Next

            message = "Invalid Picture format! The Passport Photograph must a jpeg."
            Return False
        Catch ex As Exception
            message = ex.ToString & ex.StackTrace.ToString
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Function
    Public Function UploadPhotogragh(ByRef f As System.Web.UI.WebControls.FileUpload, ByRef message As String) As Boolean
        If Not f.HasFile Then
            message = "No file is selected."
            Return False
        End If

        Dim sessionid As String

        ' Get the current HTTPContext
        Dim context As HttpContext = HttpContext.Current
        sessionid = context.Session.SessionID

        Dim fileExtension As String = System.IO.Path.GetExtension(f.FileName).ToLower()
        Dim allowedExtensions As String() = {".jpeg", ".jpg", ".bmp", ".png"}
        Dim counter As Integer

        For counter = 0 To allowedExtensions.Length Step 1
            Try
                If fileExtension = allowedExtensions(counter) Then
                    Dim myImage As System.Drawing.Bitmap = New System.Drawing.Bitmap(480, 672)

                    Try
                        Dim path As String = context.Server.MapPath("~/UploadedImages/")
                        Dim filename As String = sessionid & Now.Millisecond.ToString & ".jpg"
                        Dim fullpath As String = path & filename
                        f.PostedFile.SaveAs(fullpath)

                        myImage = New System.Drawing.Bitmap(fullpath)
                        'Dim ImgSize As Integer = myImage.( )


                        ' If (myImage.Width >= 460 And myImage.Width <= 675) And (myImage.Height >= 500 And myImage.Height <= 680) Then
                        'message = "/UploadedImages" + filename
                        message = "~/UploadedImages/" + filename
                        Return True
                        'Else
                        'message = "Invalid Picture size! The Passport Photograph must be of size 120 X 140px"
                        ' Return False
                        ' End If

                    Catch ex As Exception
                        'NavyWebDAL.AppException.LogError(ex.Message, ex.StackTrace.ToString)
                        message = "Image could not be uploaded at the moment."
                        Return False
                    Finally
                        myImage.Dispose()
                    End Try

                End If



            Catch ex As Exception
                message = "Invalid File Type."
                Return False
            End Try

        Next

        message = "Invalid Picture format! The Passport Photograph must a jpeg."
        Return False

    End Function
    Public Shared Function ReturnPicturePath(ByVal picturename As String, ByVal picByte As Byte(), ByVal rootFolder As Boolean) As String

        ' Dim _appPhotoImage As String = "eImmigration\TemImages\"
        Dim _context As HttpContext = HttpContext.Current

        'Path to write Image to
        Dim relPath As String = picturename
        Dim mpath As String = _context.Server.MapPath("~/" & relPath)
        Dim binWriter As BinaryWriter = Nothing
        Dim _Status As Boolean = False


        Try
            binWriter = New BinaryWriter(File.Open(mpath, FileMode.Create))
            binWriter.Write(picByte)
            _Status = True
        Catch ex As Exception
            'NavyWebDAL.AppException.LogError(ex.Message, ex.StackTrace.ToString)
            _Status = False
        Finally
            binWriter.Close()
            binWriter = Nothing
        End Try

        If _Status Then
            If rootFolder = False Then
                relPath = "~/" & relPath
            End If
            Return relPath
        Else
            Return Nothing
        End If
    End Function
    Public Function GetDefaultImage() As Byte()
        Dim sy() As Byte
        Dim dy As New System.Text.ASCIIEncoding
        sy = dy.GetBytes("seyi")
        Return sy
    End Function
    Public Function UploadDocumentFile(ByRef f As System.Web.UI.WebControls.FileUpload, ByRef message As String) As Boolean
        If Not f.HasFile Then
            message = "No file is selected."
            Return False
        End If

        Dim sessionid As String

        ' Get the current HTTPContext
        Dim context As HttpContext = HttpContext.Current
        sessionid = context.Session.SessionID

        Dim fileExtension As String = System.IO.Path.GetExtension(f.FileName).ToLower()
        Dim allowedExtensions As String() = {".jpeg", ".jpg"}
        Dim counter As Integer

        For counter = 0 To allowedExtensions.Length Step 1
            If fileExtension = allowedExtensions(counter) Then
                Dim myImage As System.Drawing.Bitmap = New System.Drawing.Bitmap(120, 140)

                Try
                    Dim path As String = context.Server.MapPath("~/UploadedImages/")
                    Dim filename As String = sessionid & Now.Millisecond.ToString & ".jpg"
                    Dim fullpath As String = path & filename
                    f.PostedFile.SaveAs(fullpath)

                    myImage = New System.Drawing.Bitmap(fullpath)

                    '  If (myImage.Width >= 300 And myImage.Width <= 400) And (myImage.Height >= 400 And myImage.Height <= 700) Then
                    'message = "/UploadedImages" + filename
                    message = "~/UploadedImages/" + filename
                    Return True
                    'Else
                    'message = "Invalid Picture size! The Passport Photograph must be of size 400 X 700px"
                    'Return False
                    'End If

                Catch ex As Exception
                    'NavyWebDAL.AppException.LogError(ex.Message, ex.StackTrace.ToString)
                    message = "Image could not be uploaded at the moment."
                    Return False
                Finally
                    myImage.Dispose()
                End Try

            End If
        Next
        message = "Invalid Picture format! The Passport Photograph must a jpeg."
        Return False

    End Function

    Public Shared Function ReturnDocumentPath(ByVal picturename As String, ByVal picByte As Byte(), ByVal rootFolder As Boolean) As String

        ' Dim _appPhotoImage As String = "eImmigration\TemImages\"
        Dim _context As HttpContext = HttpContext.Current

        'Path to write Image to
        Dim relPath As String = picturename
        Dim mpath As String = _context.Server.MapPath("~/" & relPath)
        Dim binWriter As BinaryWriter = Nothing
        Dim _Status As Boolean = False


        Try
            binWriter = New BinaryWriter(File.Open(mpath, FileMode.Create))
            binWriter.Write(picByte)
            _Status = True
        Catch ex As Exception
            'NavyWebDAL.AppException.LogError(ex.Message, ex.StackTrace.ToString)
            _Status = False
        Finally
            binWriter.Close()
            binWriter = Nothing
        End Try

        If _Status Then
            If rootFolder = False Then
                relPath = "~/" & relPath
            End If
            Return relPath
        Else
            Return Nothing
        End If
    End Function



    'Public Sub ResizeImageToFile(ByVal myImage As Image, ByVal filename As String, ByVal maxImageWidth As Integer, ByVal maxImageHeight As Integer)

    '    ' check first if image needs resizing
    '    If myImage.Width > maxImageWidth OrElse myImage.Height > maxImageHeight Then
    '        Dim scaleFactor As Double = 1
    '        Dim imgWidth As Integer = myImage.Width
    '        Dim imgHeight As Integer = myImage.Height
    '        Dim deltaWidth As Integer = imgWidth - maxImageWidth
    '        Dim deltaHeight As Integer = imgHeight - maxImageHeight

    '        ' calculate the scaling factor
    '        If deltaHeight > deltaWidth Then
    '            scaleFactor = CDbl(maxImageHeight) / CDbl(imgHeight)
    '            Else()
    '            scaleFactor = CDbl(maxImageWidth) / CDbl(imgWidth)
    '        End If

    '        imgWidth = CInt((CDbl(imgWidth) * scaleFactor))
    '        imgHeight = CInt((CDbl(imgHeight) * scaleFactor))

    '        Dim myCallback As New Image.GetThumbnailImageAbort(ThumbnailCallback)
    '        Dim newImage As Image = myImage.GetThumbnailImage(imgWidth, imgHeight, myCallback, IntPtr.Zero)
    '        newImage.Save(filename)
    '        Else()
    '        myImage.Save(filename)
    '    End If
    'End Sub


End Class