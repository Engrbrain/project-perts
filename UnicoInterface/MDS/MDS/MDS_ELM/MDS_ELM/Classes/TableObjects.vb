Imports Microsoft.ApplicationBlocks.Data

Public Class TableObjects



#Region "STAFFMAPPING"
    '/// <summary>
    '/// Domain entity for STAFFMAPPING.
    '/// </summary>
    Public Class STAFFMAPPING

        Private _id As Integer
        Private _sTAFFNAME_ELM As String = String.Empty
        Private _sTAFFNAME_SAP As String = String.Empty
        Private _sAP_PERSONNEL_NUMBER As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property STAFFNAME_ELM As String

            Get
                Return _sTAFFNAME_ELM
            End Get
            Set(ByVal Value As String)
                _sTAFFNAME_ELM = Value
            End Set
        End Property

        Public Property STAFFNAME_SAP As String

            Get
                Return _sTAFFNAME_SAP
            End Get
            Set(ByVal Value As String)
                _sTAFFNAME_SAP = Value
            End Set
        End Property

        Public Property SAP_PERSONNEL_NUMBER As String

            Get
                Return _sAP_PERSONNEL_NUMBER
            End Get
            Set(ByVal Value As String)
                _sAP_PERSONNEL_NUMBER = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "USER_REGISTER"
    '/// <summary>
    '/// Domain entity for USER_REGISTER.
    '/// </summary>
    Public Class USER_REGISTER

        Private _id As Integer
        Private _fIRST_NAME As String = String.Empty
        Private _lAST_NAME As String = String.Empty
        Private _eMAIL As String = String.Empty
        Private _pERSONNEL_NUMBER As String = String.Empty
        Private _uSER_PASSWORD As String = String.Empty
        Private _tIMESTAMP As String = String.Empty
        Private _rOLEFK As Integer
        Private _uNIQUEREF As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
            
        End Property

        Public Property FIRST_NAME As String

            Get
                Return _fIRST_NAME
            End Get
            Set(ByVal Value As String)
                _fIRST_NAME = Value
            End Set
        End Property

        Public Property LAST_NAME As String

            Get
                Return _lAST_NAME
            End Get
            Set(ByVal Value As String)
                _lAST_NAME = Value
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

        Public Property PERSONNEL_NUMBER As String

            Get
                Return _pERSONNEL_NUMBER
            End Get
            Set(ByVal Value As String)
                _pERSONNEL_NUMBER = Value
            End Set
        End Property

        Public Property USER_PASSWORD As String

            Get
                Return _uSER_PASSWORD
            End Get
            Set(ByVal Value As String)
                _uSER_PASSWORD = Value
            End Set
        End Property

        Public Property TIMESTAMP As String

            Get
                Return _tIMESTAMP
            End Get
            Set(ByVal Value As String)
                _tIMESTAMP = Value
            End Set
        End Property

        Public Property ROLEFK As Integer

            Get
                Return _rOLEFK
            End Get
            Set(ByVal Value As Integer)
                _rOLEFK = Value
            End Set
        End Property

        Public Property UNIQUEREF As String

            Get
                Return _uNIQUEREF
            End Get
            Set(ByVal Value As String)
                _uNIQUEREF = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "TEMP_ORDER_SPLITING"
    '/// <summary>
    '/// Domain entity for TEMP_ORDER_SPLITING.
    '/// </summary>
    Public Class TEMP_ORDER_SPLITING

        Private _id As Integer
        Private _nEW_DOCUMENT_NUMBER As String = String.Empty
        Private _oLD_DOCUMENT_NUMBER As String = String.Empty
        Private _dEPOTID As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property NEW_DOCUMENT_NUMBER As String

            Get
                Return _nEW_DOCUMENT_NUMBER
            End Get
            Set(ByVal Value As String)
                _nEW_DOCUMENT_NUMBER = Value
            End Set
        End Property

        Public Property OLD_DOCUMENT_NUMBER As String

            Get
                Return _oLD_DOCUMENT_NUMBER
            End Get
            Set(ByVal Value As String)
                _oLD_DOCUMENT_NUMBER = Value
            End Set
        End Property

        Public Property DEPOTID As String

            Get
                Return _dEPOTID
            End Get
            Set(ByVal Value As String)
                _dEPOTID = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "DEPORTMANAGERS"
    '/// <summary>
    '/// Domain entity for DEPORTMANAGERS.
    '/// </summary>
    Public Class DEPORTMANAGERS

        Private _id As Integer
        Private _dEPORTID As String = String.Empty
        Private _dEPORTNAME As String = String.Empty
        Private _mANAGERS_FIRST_NAME As String = String.Empty
        Private _mANAGERS_LAST_NAME As String = String.Empty
        Private _sAP_NAME As String = String.Empty
        Private _pERSONNELID As String = String.Empty
        Private _iS_ACTIVE As Integer

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property DEPORTID As String

            Get
                Return _dEPORTID
            End Get
            Set(ByVal Value As String)
                _dEPORTID = Value
            End Set
        End Property

        Public Property DEPORTNAME As String

            Get
                Return _dEPORTNAME
            End Get
            Set(ByVal Value As String)
                _dEPORTNAME = Value
            End Set
        End Property

        Public Property MANAGERS_FIRST_NAME As String

            Get
                Return _mANAGERS_FIRST_NAME
            End Get
            Set(ByVal Value As String)
                _mANAGERS_FIRST_NAME = Value
            End Set
        End Property

        Public Property MANAGERS_LAST_NAME As String

            Get
                Return _mANAGERS_LAST_NAME
            End Get
            Set(ByVal Value As String)
                _mANAGERS_LAST_NAME = Value
            End Set
        End Property

        Public Property SAP_NAME As String

            Get
                Return _sAP_NAME
            End Get
            Set(ByVal Value As String)
                _sAP_NAME = Value
            End Set
        End Property

        Public Property PERSONNELID As String

            Get
                Return _pERSONNELID
            End Get
            Set(ByVal Value As String)
                _pERSONNELID = Value
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


#Region "SALES_ORDER_HEADER"
    '/// <summary>
    '/// Domain entity for SALES_ORDER_HEADER.
    '/// </summary>
    Public Class SALES_ORDER_HEADER

        Private _id As Integer
        Private _sALES_DOCUMENT_NUMBER As String = String.Empty
        Private _dOCUMENT_TYPE As String = String.Empty
        Private _sALES_ORGANIZATION As String = String.Empty
        Private _dIST_CHANNEL As String = String.Empty
        Private _dIVISION As String = String.Empty
        Private _rEQ_DATE_H As String = String.Empty
        Private _pURCHASE_DATE As String = String.Empty
        Private _iNCOTERMS1 As String = String.Empty
        Private _iNCOTERMS2 As String = String.Empty
        Private _iS_LOADED As Integer
        Private _tIMESTAMP As String = String.Empty
        Private _uNIQUEREF As String = String.Empty
        Private _rATEDAMOUNT As Double
        Private _mINIMUMAMOUNT As Double
        Private _oFFSET As Double
        Private _sALES_OFFICE As String = String.Empty




#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
          
        End Property

        

        Public Property SALES_DOCUMENT_NUMBER As String

            Get
                Return _sALES_DOCUMENT_NUMBER
            End Get
            Set(ByVal Value As String)
                _sALES_DOCUMENT_NUMBER = Value
            End Set
        End Property
        Public Property SALES_OFFICE As String

            Get
                Return _sALES_OFFICE
            End Get
            Set(ByVal Value As String)
                _sALES_OFFICE = Value
            End Set
        End Property

        Public Property DOCUMENT_TYPE As String

            Get
                Return _dOCUMENT_TYPE
            End Get
            Set(ByVal Value As String)
                _dOCUMENT_TYPE = Value
            End Set
        End Property

        Public Property SALES_ORGANIZATION As String

            Get
                Return _sALES_ORGANIZATION
            End Get
            Set(ByVal Value As String)
                _sALES_ORGANIZATION = Value
            End Set
        End Property

        Public Property DIST_CHANNEL As String

            Get
                Return _dIST_CHANNEL
            End Get
            Set(ByVal Value As String)
                _dIST_CHANNEL = Value
            End Set
        End Property

        Public Property DIVISION As String

            Get
                Return _dIVISION
            End Get
            Set(ByVal Value As String)
                _dIVISION = Value
            End Set
        End Property

        Public Property REQ_DATE_H As String

            Get
                Return _rEQ_DATE_H
            End Get
            Set(ByVal Value As String)
                _rEQ_DATE_H = Value
            End Set
        End Property

        Public Property PURCHASE_DATE As String

            Get
                Return _pURCHASE_DATE
            End Get
            Set(ByVal Value As String)
                _pURCHASE_DATE = Value
            End Set
        End Property

        Public Property INCOTERMS1 As String

            Get
                Return _iNCOTERMS1
            End Get
            Set(ByVal Value As String)
                _iNCOTERMS1 = Value
            End Set
        End Property

        Public Property INCOTERMS2 As String

            Get
                Return _iNCOTERMS2
            End Get
            Set(ByVal Value As String)
                _iNCOTERMS2 = Value
            End Set
        End Property

        Public Property IS_LOADED As Integer

            Get
                Return _iS_LOADED
            End Get
            Set(ByVal Value As Integer)
                _iS_LOADED = Value
            End Set
        End Property

        Public Property TIMESTAMP As String

            Get
                Return _tIMESTAMP
            End Get
            Set(ByVal Value As String)
                _tIMESTAMP = Value
            End Set
        End Property

        Public Property UNIQUEREF As String

            Get
                Return _uNIQUEREF
            End Get
            Set(ByVal Value As String)
                _uNIQUEREF = Value
            End Set
        End Property

        Public Property RATEDAMOUNT As Double

            Get
                Return _rATEDAMOUNT
            End Get
            Set(ByVal Value As Double)
                _rATEDAMOUNT = Value
            End Set
        End Property

        Public Property MINIMUMAMOUNT As Double

            Get
                Return _mINIMUMAMOUNT
            End Get
            Set(ByVal Value As Double)
                _mINIMUMAMOUNT = Value
            End Set
        End Property

        Public Property OFFSET As Double

            Get
                Return _oFFSET
            End Get
            Set(ByVal Value As Double)
                _oFFSET = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region


#Region "SALES_ORDER_ITEM"
    '/// <summary>
    '/// Domain entity for SALES_ORDER_ITEM.
    '/// </summary>
    Public Class SALES_ORDER_ITEM

        Private _id As Integer
        Private _sALES_DOCUMENT_NUMBER As String = String.Empty
        Private _pO_ITM_NO As String = String.Empty
        Private _mATERIAL As String = String.Empty
        Private _bILLDATE As String = String.Empty
        Private _pLANT As String = String.Empty
        Private _sTORAGE_LOCATION As String = String.Empty
        Private _iNCOTERM1 As String = String.Empty
        Private _iNCOTERM2 As String = String.Empty
        Private _iS_LOADED As Integer
        Private _tIMESTAMP As String = String.Empty
        Private _uNIQUEREF As String = String.Empty
        Private _sALES_ORDER_LINE_ITEMS As String = String.Empty
        Private _pROFIT_CENTER As String = String.Empty



#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
            
        End Property

        Public Property PROFIT_CENTER As String

            Get
                Return _pROFIT_CENTER
            End Get
            Set(ByVal Value As String)
                _pROFIT_CENTER = Value
            End Set
        End Property

        Public Property SALES_ORDER_LINE_ITEMS As String

            Get
                Return _sALES_ORDER_LINE_ITEMS
            End Get
            Set(ByVal Value As String)
                _sALES_ORDER_LINE_ITEMS = Value
            End Set
        End Property

        Public Property SALES_DOCUMENT_NUMBER As String

            Get
                Return _sALES_DOCUMENT_NUMBER
            End Get
            Set(ByVal Value As String)
                _sALES_DOCUMENT_NUMBER = Value
            End Set
        End Property

        Public Property PO_ITM_NO As String

            Get
                Return _pO_ITM_NO
            End Get
            Set(ByVal Value As String)
                _pO_ITM_NO = Value
            End Set
        End Property

        Public Property MATERIAL As String

            Get
                Return _mATERIAL
            End Get
            Set(ByVal Value As String)
                _mATERIAL = Value
            End Set
        End Property

        Public Property BILLDATE As String

            Get
                Return _bILLDATE
            End Get
            Set(ByVal Value As String)
                _bILLDATE = Value
            End Set
        End Property

        Public Property PLANT As String

            Get
                Return _pLANT
            End Get
            Set(ByVal Value As String)
                _pLANT = Value
            End Set
        End Property

        Public Property STORAGE_LOCATION As String

            Get
                Return _sTORAGE_LOCATION
            End Get
            Set(ByVal Value As String)
                _sTORAGE_LOCATION = Value
            End Set
        End Property

        Public Property INCOTERM1 As String

            Get
                Return _iNCOTERM1
            End Get
            Set(ByVal Value As String)
                _iNCOTERM1 = Value
            End Set
        End Property

        Public Property INCOTERM2 As String

            Get
                Return _iNCOTERM2
            End Get
            Set(ByVal Value As String)
                _iNCOTERM2 = Value
            End Set
        End Property

        Public Property IS_LOADED As Integer

            Get
                Return _iS_LOADED
            End Get
            Set(ByVal Value As Integer)
                _iS_LOADED = Value
            End Set
        End Property

        Public Property TIMESTAMP As String

            Get
                Return _tIMESTAMP
            End Get
            Set(ByVal Value As String)
                _tIMESTAMP = Value
            End Set
        End Property

        Public Property UNIQUEREF As String

            Get
                Return _uNIQUEREF
            End Get
            Set(ByVal Value As String)
                _uNIQUEREF = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "SALES_ORDER_CONDITION_ITEM"
    '/// <summary>
    '/// Domain entity for SALES_ORDER_CONDITION_ITEM.
    '/// </summary>
    Public Class SALES_ORDER_CONDITION_ITEM

        Private _id As Integer
        Private _sALES_DOCUMENT_NUMBER As String = String.Empty
        Private _sALES_DOCUMENT_ITEM As String = String.Empty
        Private _cONDITION_ITEM_NUMBER As String = String.Empty
        Private _sTEP_NUMBER As String = String.Empty
        Private _cONDITION_COUNTER As String = String.Empty
        Private _cONDITION_TYPE As String = String.Empty
        Private _cONDITION_RATE As Double
        Private _cURRENCY_KEY As String = String.Empty
        Private _cONDITION_UNIT As String = String.Empty
        Private _cONDITION_PRICING_UNIT As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property SALES_DOCUMENT_NUMBER As String

            Get
                Return _sALES_DOCUMENT_NUMBER
            End Get
            Set(ByVal Value As String)
                _sALES_DOCUMENT_NUMBER = Value
            End Set
        End Property

        Public Property SALES_DOCUMENT_ITEM As String

            Get
                Return _sALES_DOCUMENT_ITEM
            End Get
            Set(ByVal Value As String)
                _sALES_DOCUMENT_ITEM = Value
            End Set
        End Property

        Public Property CONDITION_ITEM_NUMBER As String

            Get
                Return _cONDITION_ITEM_NUMBER
            End Get
            Set(ByVal Value As String)
                _cONDITION_ITEM_NUMBER = Value
            End Set
        End Property

        Public Property STEP_NUMBER As String

            Get
                Return _sTEP_NUMBER
            End Get
            Set(ByVal Value As String)
                _sTEP_NUMBER = Value
            End Set
        End Property

        Public Property CONDITION_COUNTER As String

            Get
                Return _cONDITION_COUNTER
            End Get
            Set(ByVal Value As String)
                _cONDITION_COUNTER = Value
            End Set
        End Property

        Public Property CONDITION_TYPE As String

            Get
                Return _cONDITION_TYPE
            End Get
            Set(ByVal Value As String)
                _cONDITION_TYPE = Value
            End Set
        End Property

        Public Property CONDITION_RATE As Double

            Get
                Return _cONDITION_RATE
            End Get
            Set(ByVal Value As Double)
                _cONDITION_RATE = Value
            End Set
        End Property

        Public Property CURRENCY_KEY As String

            Get
                Return _cURRENCY_KEY
            End Get
            Set(ByVal Value As String)
                _cURRENCY_KEY = Value
            End Set
        End Property

        Public Property CONDITION_UNIT As String

            Get
                Return _cONDITION_UNIT
            End Get
            Set(ByVal Value As String)
                _cONDITION_UNIT = Value
            End Set
        End Property

        Public Property CONDITION_PRICING_UNIT As String

            Get
                Return _cONDITION_PRICING_UNIT
            End Get
            Set(ByVal Value As String)
                _cONDITION_PRICING_UNIT = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region


#Region "SALES_ORDER_PARTNER"
    '/// <summary>
    '/// Domain entity for SALES_ORDER_PARTNER.
    '/// </summary>
    Public Class SALES_ORDER_PARTNER

        Private _id As Integer
        Private _sALES_DOCUMENT_NUMBER As String = String.Empty
        Private _pARTNER_ROLES As String = String.Empty
        Private _pARTNER_NUMBER As String = String.Empty
        Private _cOUNTRY As String = String.Empty
        Private _iS_LOADED As Integer
        Private _tIMESTAMP As String = String.Empty
        Private _uNIQUEREF As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property SALES_DOCUMENT_NUMBER As String

            Get
                Return _sALES_DOCUMENT_NUMBER
            End Get
            Set(ByVal Value As String)
                _sALES_DOCUMENT_NUMBER = Value
            End Set
        End Property

        Public Property PARTNER_ROLES As String

            Get
                Return _pARTNER_ROLES
            End Get
            Set(ByVal Value As String)
                _pARTNER_ROLES = Value
            End Set
        End Property

        Public Property PARTNER_NUMBER As String

            Get
                Return _pARTNER_NUMBER
            End Get
            Set(ByVal Value As String)
                _pARTNER_NUMBER = Value
            End Set
        End Property

        Public Property COUNTRY As String

            Get
                Return _cOUNTRY
            End Get
            Set(ByVal Value As String)
                _cOUNTRY = Value
            End Set
        End Property

        Public Property IS_LOADED As Integer

            Get
                Return _iS_LOADED
            End Get
            Set(ByVal Value As Integer)
                _iS_LOADED = Value
            End Set
        End Property

        Public Property TIMESTAMP As String

            Get
                Return _tIMESTAMP
            End Get
            Set(ByVal Value As String)
                _tIMESTAMP = Value
            End Set
        End Property

        Public Property UNIQUEREF As String

            Get
                Return _uNIQUEREF
            End Get
            Set(ByVal Value As String)
                _uNIQUEREF = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "SALES_ORDER_SCHEDULELINES"
    '/// <summary>
    '/// Domain entity for SALES_ORDER_SCHEDULELINES.
    '/// </summary>
    Public Class SALES_ORDER_SCHEDULELINES

        Private _id As Integer
        Private _sALES_DOCUMENT_NUMBER As String = String.Empty
        Private _sALES_DOCUMENT_ITEM As String = String.Empty
        Private _sCHEDULE_LINES As String = String.Empty
        Private _rEQ_DATE As String = String.Empty
        Private _rEQ_QUANTITY As String = String.Empty
        Private _iS_LOADED As Integer
        Private _tIMESTAMP As String = String.Empty
        Private _uNIQUEREF As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property SALES_DOCUMENT_NUMBER As String

            Get
                Return _sALES_DOCUMENT_NUMBER
            End Get
            Set(ByVal Value As String)
                _sALES_DOCUMENT_NUMBER = Value
            End Set
        End Property

        Public Property SALES_DOCUMENT_ITEM As String

            Get
                Return _sALES_DOCUMENT_ITEM
            End Get
            Set(ByVal Value As String)
                _sALES_DOCUMENT_ITEM = Value
            End Set
        End Property

        Public Property SCHEDULE_LINES As String

            Get
                Return _sCHEDULE_LINES
            End Get
            Set(ByVal Value As String)
                _sCHEDULE_LINES = Value
            End Set
        End Property

        Public Property REQ_DATE As String

            Get
                Return _rEQ_DATE
            End Get
            Set(ByVal Value As String)
                _rEQ_DATE = Value
            End Set
        End Property

        Public Property REQ_QUANTITY As String

            Get
                Return _rEQ_QUANTITY
            End Get
            Set(ByVal Value As String)
                _rEQ_QUANTITY = Value
            End Set
        End Property

        Public Property IS_LOADED As Integer

            Get
                Return _iS_LOADED
            End Get
            Set(ByVal Value As Integer)
                _iS_LOADED = Value
            End Set
        End Property

        Public Property TIMESTAMP As String

            Get
                Return _tIMESTAMP
            End Get
            Set(ByVal Value As String)
                _tIMESTAMP = Value
            End Set
        End Property

        Public Property UNIQUEREF As String

            Get
                Return _uNIQUEREF
            End Get
            Set(ByVal Value As String)
                _uNIQUEREF = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "MATERIAL_MASTER"
    '/// <summary>
    '/// Domain entity for MATERIAL_MASTER.
    '/// </summary>
    Public Class MATERIAL_MASTER

        Private _id As Integer
        Private _mATERIAL_CODE As String = String.Empty
        Private _mATERIAL_DESC As String = String.Empty
        Private _dIVISION As String = String.Empty
        Private _uNIT_OF_MEASURE As String = String.Empty
        Private _lEG_CODE As String = String.Empty
        Private _lEG_DESC As String = String.Empty
        Private _lEG_UOM As String = String.Empty
        Private _uNIQUEREF As String = String.Empty
        Private _tIMESTAMP As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
          
        End Property

        Public Property MATERIAL_CODE As String

            Get
                Return _mATERIAL_CODE
            End Get
            Set(ByVal Value As String)
                _mATERIAL_CODE = Value
            End Set
        End Property

        Public Property MATERIAL_DESC As String

            Get
                Return _mATERIAL_DESC
            End Get
            Set(ByVal Value As String)
                _mATERIAL_DESC = Value
            End Set
        End Property

        Public Property DIVISION As String

            Get
                Return _dIVISION
            End Get
            Set(ByVal Value As String)
                _dIVISION = Value
            End Set
        End Property

        Public Property UNIT_OF_MEASURE As String

            Get
                Return _uNIT_OF_MEASURE
            End Get
            Set(ByVal Value As String)
                _uNIT_OF_MEASURE = Value
            End Set
        End Property

        Public Property LEG_CODE As String

            Get
                Return _lEG_CODE
            End Get
            Set(ByVal Value As String)
                _lEG_CODE = Value
            End Set
        End Property

        Public Property LEG_DESC As String

            Get
                Return _lEG_DESC
            End Get
            Set(ByVal Value As String)
                _lEG_DESC = Value
            End Set
        End Property

        Public Property LEG_UOM As String

            Get
                Return _lEG_UOM
            End Get
            Set(ByVal Value As String)
                _lEG_UOM = Value
            End Set
        End Property

        Public Property UNIQUEREF As String

            Get
                Return _uNIQUEREF
            End Get
            Set(ByVal Value As String)
                _uNIQUEREF = Value
            End Set
        End Property

        Public Property TIMESTAMP As String

            Get
                Return _tIMESTAMP
            End Get
            Set(ByVal Value As String)
                _tIMESTAMP = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "CUSTOMER_MAPPING"
    '/// <summary>
    '/// Domain entity for CUSTOMER_MAPPING.
    '/// </summary>
    Public Class CUSTOMER_MAPPING

        Private _id As Integer
        Private _eLM_CLIENT_ID As String = String.Empty
        Private _eLM_CLIENT_NAME As String = String.Empty
        Private _sAP_CLIENT_ID As String = String.Empty
        Private _sAP_CLIENT_NAME As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property ELM_CLIENT_ID As String

            Get
                Return _eLM_CLIENT_ID
            End Get
            Set(ByVal Value As String)
                _eLM_CLIENT_ID = Value
            End Set
        End Property

        Public Property ELM_CLIENT_NAME As String

            Get
                Return _eLM_CLIENT_NAME
            End Get
            Set(ByVal Value As String)
                _eLM_CLIENT_NAME = Value
            End Set
        End Property

        Public Property SAP_CLIENT_ID As String

            Get
                Return _sAP_CLIENT_ID
            End Get
            Set(ByVal Value As String)
                _sAP_CLIENT_ID = Value
            End Set
        End Property

        Public Property SAP_CLIENT_NAME As String

            Get
                Return _sAP_CLIENT_NAME
            End Get
            Set(ByVal Value As String)
                _sAP_CLIENT_NAME = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region

#Region "STORAGE_LOC_MAPPING"
    '/// <summary>
    '/// Domain entity for STORAGE_LOC_MAPPING.
    '/// </summary>
    Public Class STORAGE_LOC_MAPPING

        Private _id As Integer
        Private _dEPORT_ID As String = String.Empty
        Private _dEPORT_NAME As String = String.Empty
        Private _sAP_STORAGE_LOC_ID As String = String.Empty
        Private _sAP_STORAGE_LOC_NAME As String = String.Empty
        Private _iS_ACTIVE As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
           
        End Property

        Public Property DEPORT_ID As String

            Get
                Return _dEPORT_ID
            End Get
            Set(ByVal Value As String)
                _dEPORT_ID = Value
            End Set
        End Property

        Public Property DEPORT_NAME As String

            Get
                Return _dEPORT_NAME
            End Get
            Set(ByVal Value As String)
                _dEPORT_NAME = Value
            End Set
        End Property

        Public Property SAP_STORAGE_LOC_ID As String

            Get
                Return _sAP_STORAGE_LOC_ID
            End Get
            Set(ByVal Value As String)
                _sAP_STORAGE_LOC_ID = Value
            End Set
        End Property

        Public Property SAP_STORAGE_LOC_NAME As String

            Get
                Return _sAP_STORAGE_LOC_NAME
            End Get
            Set(ByVal Value As String)
                _sAP_STORAGE_LOC_NAME = Value
            End Set
        End Property

        Public Property IS_ACTIVE As String

            Get
                Return _iS_ACTIVE
            End Get
            Set(ByVal Value As String)
                _iS_ACTIVE = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region


#Region "SALES_OFFICE_MAPPING"
    '/// <summary>
    '/// Domain entity for SALES_OFFICE_MAPPING.
    '/// </summary>
    Public Class SALES_OFFICE_MAPPING

        Private _id As Integer
        Private _dEPORT_ID As String = String.Empty
        Private _dEPORT_NAME As String = String.Empty
        Private _sALES_OFFICE As String = String.Empty
        Private _iS_ACTIVE As String = String.Empty
        Private _pROFIT_CENTER As String = String.Empty

#Region "Public Properties"
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
          
        End Property

        Public Property DEPORT_ID As String

            Get
                Return _dEPORT_ID
            End Get
            Set(ByVal Value As String)
                _dEPORT_ID = Value
            End Set
        End Property

        Public Property DEPORT_NAME As String

            Get
                Return _dEPORT_NAME
            End Get
            Set(ByVal Value As String)
                _dEPORT_NAME = Value
            End Set
        End Property

        Public Property SALES_OFFICE As String

            Get
                Return _sALES_OFFICE
            End Get
            Set(ByVal Value As String)
                _sALES_OFFICE = Value
            End Set
        End Property

        Public Property IS_ACTIVE As String

            Get
                Return _iS_ACTIVE
            End Get
            Set(ByVal Value As String)
                _iS_ACTIVE = Value
            End Set
        End Property

        Public Property PROFIT_CENTER As String

            Get
                Return _pROFIT_CENTER
            End Get
            Set(ByVal Value As String)
                _pROFIT_CENTER = Value
            End Set
        End Property
#End Region

        Public Sub New()
        End Sub

    End Class
#End Region




































End Class

