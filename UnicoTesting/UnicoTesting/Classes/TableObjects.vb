Imports Microsoft.ApplicationBlocks.Data

Public Class TableObjects

#Region "GL_TRS"
    '/// <summary>
    '/// Domain entity for BRANCH.
    '/// </summary>
    Public Class GL_TRS

        Private _id As Integer
        Private _aCCT_NO As String = String.Empty
        Private _dEPT_ID As String = String.Empty
        Private _tRS_DESC As String = String.Empty
        Private _tRS_REF As String = String.Empty
        Private _tRS_DATE As Date
        Private _pERIOD As String = String.Empty
        Private _pREV_PERIOD As String = String.Empty
        Private _bATCH_NO As String = String.Empty
        Private _tRS_SYSTEM As String = String.Empty
        Private _tRS_PRT As String = String.Empty
        Private _tRS_ID As String = String.Empty
        Private _lOCKED As String = String.Empty
        Private _tRS_AMT As Double
        Private _sEQ As Double
        Private _uNIT_CODE As String = String.Empty
        Private _eMPLOYEE_CODE As String = String.Empty
        Private _cOMPANY_ID As String = String.Empty
        Private _cURRENCY_CODE As String = String.Empty
        Private _eXCHANGE_RATE As Double
        Private _pROCESSED As String = String.Empty
        Private _eNTRY_DATE As Date
        Private _useFlag As Integer
        Private _dateOfExtract As Date
        Private _extractReference As String = String.Empty
        Private _SAP_GL_ACCOUNT As String = String.Empty
        Private _USERNAME As String = String.Empty



#Region "Public Properties"
        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get

        End Property
        Public Property USERNAME() As String

            Get
                Return _USERNAME
            End Get
            Set(ByVal Value As String)
                _USERNAME = Value
            End Set
        End Property
        Public Property SAP_GL_ACCOUNT() As String

            Get
                Return _SAP_GL_ACCOUNT
            End Get
            Set(ByVal Value As String)
                _SAP_GL_ACCOUNT = Value
            End Set
        End Property

        Public Property ACCT_NO() As String

            Get
                Return _aCCT_NO
            End Get
            Set(ByVal Value As String)
                _aCCT_NO = Value
            End Set
        End Property

        Public Property DEPT_ID() As String

            Get
                Return _dEPT_ID
            End Get
            Set(ByVal Value As String)
                _dEPT_ID = Value
            End Set
        End Property

        Public Property TRS_DESC() As String

            Get
                Return _tRS_DESC
            End Get
            Set(ByVal Value As String)
                _tRS_DESC = Value
            End Set
        End Property

        Public Property TRS_REF() As String

            Get
                Return _tRS_REF
            End Get
            Set(ByVal Value As String)
                _tRS_REF = Value
            End Set
        End Property

        Public Property TRS_DATE() As Date

            Get
                Return _tRS_DATE
            End Get
            Set(ByVal Value As Date)
                _tRS_DATE = Value
            End Set
        End Property
        Public Property PERIOD() As String

            Get
                Return _pERIOD
            End Get
            Set(ByVal Value As String)
                _pERIOD = Value
            End Set
        End Property
        Public Property PREV_PERIOD() As String

            Get
                Return _pREV_PERIOD
            End Get
            Set(ByVal Value As String)
                _pREV_PERIOD = Value
            End Set
        End Property
        Public Property BATCH_NO() As String

            Get
                Return _bATCH_NO
            End Get
            Set(ByVal Value As String)
                _bATCH_NO = Value
            End Set
        End Property
        Public Property TRS_SYSTEM() As String

            Get
                Return _tRS_SYSTEM
            End Get
            Set(ByVal Value As String)
                _tRS_SYSTEM = Value
            End Set
        End Property
        Public Property TRS_PRT() As String

            Get
                Return _tRS_PRT
            End Get
            Set(ByVal Value As String)
                _tRS_PRT = Value
            End Set
        End Property
        Public Property TRS_ID() As String

            Get
                Return _tRS_ID
            End Get
            Set(ByVal Value As String)
                _tRS_ID = Value
            End Set
        End Property
        Public Property LOCKED() As String

            Get
                Return _lOCKED
            End Get
            Set(ByVal Value As String)
                _lOCKED = Value
            End Set
        End Property
        Public Property TRS_AMT() As Double

            Get
                Return _tRS_AMT
            End Get
            Set(ByVal Value As Double)
                _tRS_AMT = Value
            End Set
        End Property
        Public Property SEQ() As Double

            Get
                Return _sEQ
            End Get
            Set(ByVal Value As Double)
                _sEQ = Value
            End Set
        End Property
        Public Property UNIT_CODE() As String

            Get
                Return _uNIT_CODE
            End Get
            Set(ByVal Value As String)
                _uNIT_CODE = Value
            End Set
        End Property
        Public Property EMPLOYEE_CODE() As String

            Get
                Return _eMPLOYEE_CODE
            End Get
            Set(ByVal Value As String)
                _eMPLOYEE_CODE = Value
            End Set
        End Property
        Public Property COMPANY_ID() As String

            Get
                Return _cOMPANY_ID
            End Get
            Set(ByVal Value As String)
                _cOMPANY_ID = Value
            End Set
        End Property
        Public Property CURRENCY_CODE() As String

            Get
                Return _cURRENCY_CODE
            End Get
            Set(ByVal Value As String)
                _cURRENCY_CODE = Value
            End Set
        End Property
        Public Property EXCHANGE_RATE() As Double

            Get
                Return _eXCHANGE_RATE
            End Get
            Set(ByVal Value As Double)
                _eXCHANGE_RATE = Value
            End Set
        End Property
        Public Property PROCESSED() As String

            Get
                Return _pROCESSED
            End Get
            Set(ByVal Value As String)
                _pROCESSED = Value
            End Set
        End Property
        Public Property ENTRY_DATE() As Date

            Get
                Return _eNTRY_DATE
            End Get
            Set(ByVal Value As Date)
                _eNTRY_DATE = Value
            End Set
        End Property
        Public Property UseFlag() As Integer

            Get
                Return _useFlag
            End Get
            Set(ByVal Value As Integer)
                _useFlag = Value
            End Set
        End Property
        Public Property DateOfExtract() As Date

            Get
                Return _dateOfExtract
            End Get
            Set(ByVal Value As Date)
                _dateOfExtract = Value
            End Set
        End Property
        Public Property ExtractReference() As String

            Get
                Return _extractReference
            End Get
            Set(ByVal Value As String)
                _extractReference = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "ACCOUNT_RECIEVABLE"
    '/// <summary>
    '/// Domain entity for ACCOUNT_RECIEVABLE.
    '/// </summary>
    Public Class ACCOUNT_RECIEVABLE

        Private _id As Integer
        Private _aCCT_NO As String = String.Empty
        Private _dEPT_ID As String = String.Empty
        Private _tRS_DESC As String = String.Empty
        Private _tRS_REF As String = String.Empty
        Private _tRS_DATE As DateTime
        Private _pERIOD As String = String.Empty
        Private _pREV_PERIOD As String = String.Empty
        Private _bATCH_NO As String = String.Empty
        Private _tRS_SYSTEM As String = String.Empty
        Private _tRS_PRT As String = String.Empty
        Private _tRS_ID As String = String.Empty
        Private _lOCKED As String = String.Empty
        Private _tRS_AMT As Decimal
        Private _sEQ As Decimal
        Private _uNIT_CODE As String = String.Empty
        Private _eMPLOYEE_CODE As String = String.Empty
        Private _cOMPANY_ID As String = String.Empty
        Private _cURRENCY_CODE As String = String.Empty
        Private _eXCHANGE_RATE As Decimal
        Private _pROCESSED As String = String.Empty
        Private _eNTRY_DATE As DateTime
        Private _useFlag As Integer
        Private _dateOfExtract As DateTime
        Private _extractReference As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property ACCT_NO As String

            Get
                Return _aCCT_NO
            End Get
            Set(ByVal Value As String)
                _aCCT_NO = Value
            End Set
        End Property

        Public Property DEPT_ID As String

            Get
                Return _dEPT_ID
            End Get
            Set(ByVal Value As String)
                _dEPT_ID = Value
            End Set
        End Property

        Public Property TRS_DESC As String

            Get
                Return _tRS_DESC
            End Get
            Set(ByVal Value As String)
                _tRS_DESC = Value
            End Set
        End Property

        Public Property TRS_REF As String

            Get
                Return _tRS_REF
            End Get
            Set(ByVal Value As String)
                _tRS_REF = Value
            End Set
        End Property

        Public Property TRS_DATE As DateTime

            Get
                Return _tRS_DATE
            End Get
            Set(ByVal Value As DateTime)
                _tRS_DATE = Value
            End Set
        End Property

        Public Property PERIOD As String

            Get
                Return _pERIOD
            End Get
            Set(ByVal Value As String)
                _pERIOD = Value
            End Set
        End Property

        Public Property PREV_PERIOD As String

            Get
                Return _pREV_PERIOD
            End Get
            Set(ByVal Value As String)
                _pREV_PERIOD = Value
            End Set
        End Property

        Public Property BATCH_NO As String

            Get
                Return _bATCH_NO
            End Get
            Set(ByVal Value As String)
                _bATCH_NO = Value
            End Set
        End Property

        Public Property TRS_SYSTEM As String

            Get
                Return _tRS_SYSTEM
            End Get
            Set(ByVal Value As String)
                _tRS_SYSTEM = Value
            End Set
        End Property

        Public Property TRS_PRT As String

            Get
                Return _tRS_PRT
            End Get
            Set(ByVal Value As String)
                _tRS_PRT = Value
            End Set
        End Property

        Public Property TRS_ID As String

            Get
                Return _tRS_ID
            End Get
            Set(ByVal Value As String)
                _tRS_ID = Value
            End Set
        End Property

        Public Property LOCKED As String

            Get
                Return _lOCKED
            End Get
            Set(ByVal Value As String)
                _lOCKED = Value
            End Set
        End Property

        Public Property TRS_AMT As Decimal

            Get
                Return _tRS_AMT
            End Get
            Set(ByVal Value As Decimal)
                _tRS_AMT = Value
            End Set
        End Property

        Public Property SEQ As Decimal

            Get
                Return _sEQ
            End Get
            Set(ByVal Value As Decimal)
                _sEQ = Value
            End Set
        End Property

        Public Property UNIT_CODE As String

            Get
                Return _uNIT_CODE
            End Get
            Set(ByVal Value As String)
                _uNIT_CODE = Value
            End Set
        End Property

        Public Property EMPLOYEE_CODE As String

            Get
                Return _eMPLOYEE_CODE
            End Get
            Set(ByVal Value As String)
                _eMPLOYEE_CODE = Value
            End Set
        End Property

        Public Property COMPANY_ID As String

            Get
                Return _cOMPANY_ID
            End Get
            Set(ByVal Value As String)
                _cOMPANY_ID = Value
            End Set
        End Property

        Public Property CURRENCY_CODE As String

            Get
                Return _cURRENCY_CODE
            End Get
            Set(ByVal Value As String)
                _cURRENCY_CODE = Value
            End Set
        End Property

        Public Property EXCHANGE_RATE As Decimal

            Get
                Return _eXCHANGE_RATE
            End Get
            Set(ByVal Value As Decimal)
                _eXCHANGE_RATE = Value
            End Set
        End Property

        Public Property PROCESSED As String

            Get
                Return _pROCESSED
            End Get
            Set(ByVal Value As String)
                _pROCESSED = Value
            End Set
        End Property

        Public Property ENTRY_DATE As DateTime

            Get
                Return _eNTRY_DATE
            End Get
            Set(ByVal Value As DateTime)
                _eNTRY_DATE = Value
            End Set
        End Property

        Public Property UseFlag As Integer

            Get
                Return _useFlag
            End Get
            Set(ByVal Value As Integer)
                _useFlag = Value
            End Set
        End Property

        Public Property DateOfExtract As DateTime

            Get
                Return _dateOfExtract
            End Get
            Set(ByVal Value As DateTime)
                _dateOfExtract = Value
            End Set
        End Property

        Public Property ExtractReference As String

            Get
                Return _extractReference
            End Get
            Set(ByVal Value As String)
                _extractReference = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "UNICO_MAPPING_TABLE"
    '/// <summary>
    '/// Domain entity for BRANCH.
    '/// </summary>
    Public Class UNICO_MAPPING_TABLE

        Private _id As Integer
        Private _aCCT_NO As String = String.Empty
        Private _dEPT_ID As String = String.Empty
        Private _tRS_DESC As String = String.Empty
        Private _tRS_REF As String = String.Empty
        Private _tRS_DATE As Date
        Private _pERIOD As String = String.Empty
        Private _pREV_PERIOD As String = String.Empty
        Private _bATCH_NO As String = String.Empty
        Private _tRS_SYSTEM As String = String.Empty
        Private _tRS_PRT As String = String.Empty
        Private _tRS_ID As String = String.Empty
        Private _lOCKED As String = String.Empty
        Private _tRS_AMT As Double
        Private _sEQ As Double
        Private _uNIT_CODE As String = String.Empty
        Private _eMPLOYEE_CODE As String = String.Empty
        Private _cOMPANY_ID As String = String.Empty
        Private _cURRENCY_CODE As String = String.Empty
        Private _eXCHANGE_RATE As Double
        Private _pROCESSED As String = String.Empty
        Private _eNTRY_DATE As String = String.Empty
        Private _useFlag As Integer
        Private _dateOfExtract As Date
        Private _documentNumber As Integer
        Private _extractReference As String = String.Empty
        Private _SAP_GL_ACCOUNT As String = String.Empty
        Private _USERNAME As String = String.Empty



#Region "Public Properties"
        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get

        End Property
        Public Property SAP_GL_ACCOUNT() As String

            Get
                Return _SAP_GL_ACCOUNT
            End Get
            Set(ByVal Value As String)
                _SAP_GL_ACCOUNT = Value
            End Set
        End Property

        Public Property USERNAME() As String

            Get
                Return _USERNAME
            End Get
            Set(ByVal Value As String)
                _USERNAME = Value
            End Set
        End Property
        Public Property DocumentNumber() As Integer

            Get
                Return _documentNumber
            End Get
            Set(ByVal Value As Integer)
                _documentNumber = Value
            End Set
        End Property

        Public Property ACCT_NO() As String

            Get
                Return _aCCT_NO
            End Get
            Set(ByVal Value As String)
                _aCCT_NO = Value
            End Set
        End Property

        Public Property DEPT_ID() As String

            Get
                Return _dEPT_ID
            End Get
            Set(ByVal Value As String)
                _dEPT_ID = Value
            End Set
        End Property

        Public Property TRS_DESC() As String

            Get
                Return _tRS_DESC
            End Get
            Set(ByVal Value As String)
                _tRS_DESC = Value
            End Set
        End Property

        Public Property TRS_REF() As String

            Get
                Return _tRS_REF
            End Get
            Set(ByVal Value As String)
                _tRS_REF = Value
            End Set
        End Property

        Public Property TRS_DATE() As Date

            Get
                Return _tRS_DATE
            End Get
            Set(ByVal Value As Date)
                _tRS_DATE = Value
            End Set
        End Property
        Public Property PERIOD() As String

            Get
                Return _pERIOD
            End Get
            Set(ByVal Value As String)
                _pERIOD = Value
            End Set
        End Property
        Public Property PREV_PERIOD() As String

            Get
                Return _pREV_PERIOD
            End Get
            Set(ByVal Value As String)
                _pREV_PERIOD = Value
            End Set
        End Property
        Public Property BATCH_NO() As String

            Get
                Return _bATCH_NO
            End Get
            Set(ByVal Value As String)
                _bATCH_NO = Value
            End Set
        End Property
        Public Property TRS_SYSTEM() As String

            Get
                Return _tRS_SYSTEM
            End Get
            Set(ByVal Value As String)
                _tRS_SYSTEM = Value
            End Set
        End Property
        Public Property TRS_PRT() As String

            Get
                Return _tRS_PRT
            End Get
            Set(ByVal Value As String)
                _tRS_PRT = Value
            End Set
        End Property
        Public Property TRS_ID() As String

            Get
                Return _tRS_ID
            End Get
            Set(ByVal Value As String)
                _tRS_ID = Value
            End Set
        End Property
        Public Property LOCKED() As String

            Get
                Return _lOCKED
            End Get
            Set(ByVal Value As String)
                _lOCKED = Value
            End Set
        End Property
        Public Property TRS_AMT() As Double

            Get
                Return _tRS_AMT
            End Get
            Set(ByVal Value As Double)
                _tRS_AMT = Value
            End Set
        End Property
        Public Property SEQ() As Double

            Get
                Return _sEQ
            End Get
            Set(ByVal Value As Double)
                _sEQ = Value
            End Set
        End Property
        Public Property UNIT_CODE() As String

            Get
                Return _uNIT_CODE
            End Get
            Set(ByVal Value As String)
                _uNIT_CODE = Value
            End Set
        End Property
        Public Property EMPLOYEE_CODE() As String

            Get
                Return _eMPLOYEE_CODE
            End Get
            Set(ByVal Value As String)
                _eMPLOYEE_CODE = Value
            End Set
        End Property
        Public Property COMPANY_ID() As String

            Get
                Return _cOMPANY_ID
            End Get
            Set(ByVal Value As String)
                _cOMPANY_ID = Value
            End Set
        End Property
        Public Property CURRENCY_CODE() As String

            Get
                Return _cURRENCY_CODE
            End Get
            Set(ByVal Value As String)
                _cURRENCY_CODE = Value
            End Set
        End Property
        Public Property EXCHANGE_RATE() As Double

            Get
                Return _eXCHANGE_RATE
            End Get
            Set(ByVal Value As Double)
                _eXCHANGE_RATE = Value
            End Set
        End Property
        Public Property PROCESSED() As String

            Get
                Return _pROCESSED
            End Get
            Set(ByVal Value As String)
                _pROCESSED = Value
            End Set
        End Property
        Public Property ENTRY_DATE() As String

            Get
                Return _eNTRY_DATE
            End Get
            Set(ByVal Value As String)
                _eNTRY_DATE = Value
            End Set
        End Property
        Public Property UseFlag() As Integer

            Get
                Return _useFlag
            End Get
            Set(ByVal Value As Integer)
                _useFlag = Value
            End Set
        End Property
        Public Property DateOfExtract() As Date

            Get
                Return _dateOfExtract
            End Get
            Set(ByVal Value As Date)
                _dateOfExtract = Value
            End Set
        End Property
        Public Property ExtractReference() As String

            Get
                Return _extractReference
            End Get
            Set(ByVal Value As String)
                _extractReference = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "UNICO_BAD_TABLE"
    '/// <summary>
    '/// Domain entity for BRANCH.
    '/// </summary>
    Public Class UNICO_BAD_TABLE

        Private _id As Integer
        Private _aCCT_NO As String = String.Empty
        Private _dEPT_ID As String = String.Empty
        Private _tRS_DESC As String = String.Empty
        Private _tRS_REF As String = String.Empty
        Private _tRS_DATE As Date
        Private _pERIOD As String = String.Empty
        Private _pREV_PERIOD As String = String.Empty
        Private _bATCH_NO As String = String.Empty
        Private _tRS_SYSTEM As String = String.Empty
        Private _tRS_PRT As String = String.Empty
        Private _tRS_ID As String = String.Empty
        Private _lOCKED As String = String.Empty
        Private _tRS_AMT As Double
        Private _sEQ As Double
        Private _uNIT_CODE As String = String.Empty
        Private _eMPLOYEE_CODE As String = String.Empty
        Private _cOMPANY_ID As String = String.Empty
        Private _cURRENCY_CODE As String = String.Empty
        Private _eXCHANGE_RATE As Double
        Private _pROCESSED As String = String.Empty
        Private _eNTRY_DATE As Date
        Private _useFlag As Integer
        Private _dateOfExtract As Date
        Private _documentNumber As Integer
        Private _extractReference As String = String.Empty
        Private _SAP_GL_ACCOUNT As String = String.Empty
        Private _USERNAME As String = String.Empty



#Region "Public Properties"
        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get

        End Property
        Public Property SAP_GL_ACCOUNT() As String

            Get
                Return _SAP_GL_ACCOUNT
            End Get
            Set(ByVal Value As String)
                _SAP_GL_ACCOUNT = Value
            End Set
        End Property

        Public Property USERNAME() As String

            Get
                Return _USERNAME
            End Get
            Set(ByVal Value As String)
                _USERNAME = Value
            End Set
        End Property
        Public Property DocumentNumber() As Integer

            Get
                Return _documentNumber
            End Get
            Set(ByVal Value As Integer)
                _documentNumber = Value
            End Set
        End Property

        Public Property ACCT_NO() As String

            Get
                Return _aCCT_NO
            End Get
            Set(ByVal Value As String)
                _aCCT_NO = Value
            End Set
        End Property

        Public Property DEPT_ID() As String

            Get
                Return _dEPT_ID
            End Get
            Set(ByVal Value As String)
                _dEPT_ID = Value
            End Set
        End Property

        Public Property TRS_DESC() As String

            Get
                Return _tRS_DESC
            End Get
            Set(ByVal Value As String)
                _tRS_DESC = Value
            End Set
        End Property

        Public Property TRS_REF() As String

            Get
                Return _tRS_REF
            End Get
            Set(ByVal Value As String)
                _tRS_REF = Value
            End Set
        End Property

        Public Property TRS_DATE() As Date

            Get
                Return _tRS_DATE
            End Get
            Set(ByVal Value As Date)
                _tRS_DATE = Value
            End Set
        End Property
        Public Property PERIOD() As String

            Get
                Return _pERIOD
            End Get
            Set(ByVal Value As String)
                _pERIOD = Value
            End Set
        End Property
        Public Property PREV_PERIOD() As String

            Get
                Return _pREV_PERIOD
            End Get
            Set(ByVal Value As String)
                _pREV_PERIOD = Value
            End Set
        End Property
        Public Property BATCH_NO() As String

            Get
                Return _bATCH_NO
            End Get
            Set(ByVal Value As String)
                _bATCH_NO = Value
            End Set
        End Property
        Public Property TRS_SYSTEM() As String

            Get
                Return _tRS_SYSTEM
            End Get
            Set(ByVal Value As String)
                _tRS_SYSTEM = Value
            End Set
        End Property
        Public Property TRS_PRT() As String

            Get
                Return _tRS_PRT
            End Get
            Set(ByVal Value As String)
                _tRS_PRT = Value
            End Set
        End Property
        Public Property TRS_ID() As String

            Get
                Return _tRS_ID
            End Get
            Set(ByVal Value As String)
                _tRS_ID = Value
            End Set
        End Property
        Public Property LOCKED() As String

            Get
                Return _lOCKED
            End Get
            Set(ByVal Value As String)
                _lOCKED = Value
            End Set
        End Property
        Public Property TRS_AMT() As Double

            Get
                Return _tRS_AMT
            End Get
            Set(ByVal Value As Double)
                _tRS_AMT = Value
            End Set
        End Property
        Public Property SEQ() As Double

            Get
                Return _sEQ
            End Get
            Set(ByVal Value As Double)
                _sEQ = Value
            End Set
        End Property
        Public Property UNIT_CODE() As String

            Get
                Return _uNIT_CODE
            End Get
            Set(ByVal Value As String)
                _uNIT_CODE = Value
            End Set
        End Property
        Public Property EMPLOYEE_CODE() As String

            Get
                Return _eMPLOYEE_CODE
            End Get
            Set(ByVal Value As String)
                _eMPLOYEE_CODE = Value
            End Set
        End Property
        Public Property COMPANY_ID() As String

            Get
                Return _cOMPANY_ID
            End Get
            Set(ByVal Value As String)
                _cOMPANY_ID = Value
            End Set
        End Property
        Public Property CURRENCY_CODE() As String

            Get
                Return _cURRENCY_CODE
            End Get
            Set(ByVal Value As String)
                _cURRENCY_CODE = Value
            End Set
        End Property
        Public Property EXCHANGE_RATE() As Double

            Get
                Return _eXCHANGE_RATE
            End Get
            Set(ByVal Value As Double)
                _eXCHANGE_RATE = Value
            End Set
        End Property
        Public Property PROCESSED() As String

            Get
                Return _pROCESSED
            End Get
            Set(ByVal Value As String)
                _pROCESSED = Value
            End Set
        End Property
        Public Property ENTRY_DATE() As Date

            Get
                Return _eNTRY_DATE
            End Get
            Set(ByVal Value As Date)
                _eNTRY_DATE = Value
            End Set
        End Property
        Public Property UseFlag() As Integer

            Get
                Return _useFlag
            End Get
            Set(ByVal Value As Integer)
                _useFlag = Value
            End Set
        End Property
        Public Property DateOfExtract() As Date

            Get
                Return _dateOfExtract
            End Get
            Set(ByVal Value As Date)
                _dateOfExtract = Value
            End Set
        End Property
        Public Property ExtractReference() As String

            Get
                Return _extractReference
            End Get
            Set(ByVal Value As String)
                _extractReference = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "USER_MANAGER"
    '/// <summary>
    '/// Domain entity for USER_MANAGER.
    '/// </summary>
    Public Class USER_MANAGER

        Private _id As Integer
        Private _sURNAME As String = String.Empty
        Private _fIRSTNAME As String = String.Empty
        Private _oTHERNAMES As String = String.Empty
        Private _eMAIL As String = String.Empty
        Private _pHONENUMBER As String = String.Empty
        Private _uSERNAME As String = String.Empty
        Private _pASSWORD As String = String.Empty
        Private _iS_ACTIVE As Integer
        Private _rEFNO As String = String.Empty
        Private _cREATIONDATE As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property SURNAME As String

            Get
                Return _sURNAME
            End Get
            Set(ByVal Value As String)
                _sURNAME = Value
            End Set
        End Property

        Public Property FIRSTNAME As String

            Get
                Return _fIRSTNAME
            End Get
            Set(ByVal Value As String)
                _fIRSTNAME = Value
            End Set
        End Property

        Public Property OTHERNAMES As String

            Get
                Return _oTHERNAMES
            End Get
            Set(ByVal Value As String)
                _oTHERNAMES = Value
            End Set
        End Property

        Public Property EMAIL As String

            Get
                Return _eMAIL
            End Get
            Set(ByVal Value As String)
                _eMAIL = Value
            End Set
        End Property

        Public Property PHONENUMBER As String

            Get
                Return _pHONENUMBER
            End Get
            Set(ByVal Value As String)
                _pHONENUMBER = Value
            End Set
        End Property

        Public Property USERNAME As String

            Get
                Return _uSERNAME
            End Get
            Set(ByVal Value As String)
                _uSERNAME = Value
            End Set
        End Property

        Public Property PASSWORD As String

            Get
                Return _pASSWORD
            End Get
            Set(ByVal Value As String)
                _pASSWORD = Value
            End Set
        End Property

        Public Property IS_ACTIVE As Integer

            Get
                Return _iS_ACTIVE
            End Get
            Set(ByVal Value As Integer)
                _iS_ACTIVE = Value
            End Set
        End Property

        Public Property REFNO As String

            Get
                Return _rEFNO
            End Get
            Set(ByVal Value As String)
                _rEFNO = Value
            End Set
        End Property

        Public Property CREATIONDATE As String

            Get
                Return _cREATIONDATE
            End Get
            Set(ByVal Value As String)
                _cREATIONDATE = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "UNICO_GL_MAPPING"
    '/// <summary>
    '/// Domain entity for UNICO_GL_MAPPING.
    '/// </summary>
    Public Class UNICO_GL_MAPPING

        Private _id As Integer
        Private _legacyDescription As String = String.Empty
        Private _legCode As String = String.Empty
        Private _sAPCode As String = String.Empty
        Private _sAPDesc As String = String.Empty
        Private _dATE_CREATED As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
          
        End Property

        Public Property LegacyDescription As String

            Get
                Return _legacyDescription
            End Get
            Set(ByVal Value As String)
                _legacyDescription = Value
            End Set
        End Property

        Public Property LegCode As String

            Get
                Return _legCode
            End Get
            Set(ByVal Value As String)
                _legCode = Value
            End Set
        End Property

        Public Property SAPCode As String

            Get
                Return _sAPCode
            End Get
            Set(ByVal Value As String)
                _sAPCode = Value
            End Set
        End Property

        Public Property SAPDesc As String

            Get
                Return _sAPDesc
            End Get
            Set(ByVal Value As String)
                _sAPDesc = Value
            End Set
        End Property

        Public Property DATE_CREATED As String

            Get
                Return _dATE_CREATED
            End Get
            Set(ByVal Value As String)
                _dATE_CREATED = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region


#Region "UNICO_DOC_HEADER"
    '/// <summary>
    '/// Domain entity for UNICO_DOC_HEADER.
    '/// </summary>
    Public Class UNICO_DOC_HEADER

        Private _id As Integer
        Private _uSERNAME As String = String.Empty
        Private _tRS_REF As String = String.Empty
        Private _documentNumber As Integer
        Private _eNTRY_DATE As String = String.Empty
        Private _pERIOD As String = String.Empty
        Private _pOSTINGDATE As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property USERNAME As String

            Get
                Return _uSERNAME
            End Get
            Set(ByVal Value As String)
                _uSERNAME = Value
            End Set
        End Property

        Public Property PERIOD As String

            Get
                Return _pERIOD
            End Get
            Set(ByVal Value As String)
                _pERIOD = Value
            End Set
        End Property

        Public Property TRS_REF As String

            Get
                Return _tRS_REF
            End Get
            Set(ByVal Value As String)
                _tRS_REF = Value
            End Set
        End Property

        Public Property DocumentNumber As Integer

            Get
                Return _documentNumber
            End Get
            Set(ByVal Value As Integer)
                _documentNumber = Value
            End Set
        End Property

        Public Property ENTRY_DATE As String

            Get
                Return _eNTRY_DATE
            End Get
            Set(ByVal Value As String)
                _eNTRY_DATE = Value
            End Set
        End Property
        Public Property POSTINGDATE As String

            Get
                Return _pOSTINGDATE
            End Get
            Set(ByVal Value As String)
                _pOSTINGDATE = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region


#Region "DOCUMENT_TYPE"
    '/// <summary>
    '/// Domain entity for DOCUMENT_TYPE.
    '/// </summary>
    Public Class DOCUMENT_TYPE

        Private _id As Integer
        Private _dOCUMENT_TYPE As String = String.Empty
        Private _dATE_ADDED As String = String.Empty
        Private _iS_ACTIVE As Integer

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
          
        End Property

        Public Property DOCUMENT_TYPE As String

            Get
                Return _dOCUMENT_TYPE
            End Get
            Set(ByVal Value As String)
                _dOCUMENT_TYPE = Value
            End Set
        End Property

        Public Property DATE_ADDED As String

            Get
                Return _dATE_ADDED
            End Get
            Set(ByVal Value As String)
                _dATE_ADDED = Value
            End Set
        End Property

        Public Property IS_ACTIVE As Integer

            Get
                Return _iS_ACTIVE
            End Get
            Set(ByVal Value As Integer)
                _iS_ACTIVE = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region


#Region "SAPCLIENT"
    '/// <summary>
    '/// Domain entity for SAPCLIENT.
    '/// </summary>
    Public Class SAPCLIENT

        Private _id As Integer
        Private _cLIENT As String = String.Empty
        Private _dATE_ADDED As String = String.Empty
        Private _iS_ACTIVE As Integer

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
          
        End Property

        Public Property CLIENT As String

            Get
                Return _cLIENT
            End Get
            Set(ByVal Value As String)
                _cLIENT = Value
            End Set
        End Property

        Public Property DATE_ADDED As String

            Get
                Return _dATE_ADDED
            End Get
            Set(ByVal Value As String)
                _dATE_ADDED = Value
            End Set
        End Property

        Public Property IS_ACTIVE As Integer

            Get
                Return _iS_ACTIVE
            End Get
            Set(ByVal Value As Integer)
                _iS_ACTIVE = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region










End Class

