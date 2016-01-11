using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankStatementProcessor.ObjectInfo;
using BankStatementProcessor.Utility;
using BankStatementProcessor.DAL;
using System.Data;
using System.IO;

namespace BankStatementProcessor.BLL
{
    public class StatementUploads
    {
        public static OperationResultInfo ProcesUpload(HttpPostedFile Data, List<string> ColumnsNames)
        {
            OperationResultInfo retVal = new OperationResultInfo();
            int sucess = 0;
            
            try
            {
                //check file type
                if ((Data.ContentType == "application/vnd.ms-excel" || Data.ContentType == "application/vnd.xls" || Data.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") && (!Data.FileName.EndsWith(".csv")))
                {
                    //save Data
                    string filename = string.Format("{0}-{1}", Guid.NewGuid(), Path.GetFileName(Data.FileName));
                    string folder = HttpContext.Current.Server.MapPath("~/BankStatementUpload");
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    string fullPath = string.Format("{0}/{1}", folder, filename);
                    Data.SaveAs(fullPath);

                    //load data
                    OperationResultInfo retData = new OperationResultInfo();
                   retData.ReturnObject = DataTransformation.ExcelToDataSet(fullPath);
                   // retData.ReturnObject = DataTransformation.Parse(fullPath);
                    DataTable dt = (DataTable)retData.ReturnObject;
                    if (dt.Columns.Count == ColumnsNames.Count)
                    {

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            string a = dt.Columns[i].ColumnName.Replace('#', '.').Trim();

                            if (a == ColumnsNames[i])
                            {
                                sucess = sucess + 1;

                            }
                        }
                        if (sucess == ColumnsNames.Count)
                        {
                            retVal.Status = true;
                            retVal.ErrorMessage = string.Format("{0} Acccount Statement(s) was successfully updated.", 1);
                            retVal.ReturnObject = dt;
                        }
                        else
                        {
                            retVal.Status = false;
                            retVal.ErrorMessage = "The supplied file does not have matching ColumnNames as Shown in the sample!";
                        }
                    }
                    else
                    {
                        retVal.Status = false;
                        retVal.ErrorMessage = " The supplied file does not have the same Numbers of column as Shown in the sample!";

                    }

                }
                else
                {
                    retVal.Status = false;
                    retVal.ErrorMessage = "Wrong file type, only microsoft excel file is allowed!";
                }
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                retVal.Status = false;
                retVal.ErrorMessage = "Error processing data!";
            }

            return retVal;
        }
    }

    public class Bank
    {
        public static OperationResultInfo InsertBank(string BakNAme)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GBanks.InsertBank(BakNAme);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo UpdateBank(int ID, string BakNAme, int Status)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GBanks.UpdateBank(ID, BakNAme, Status);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllBank(int ID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GBanks.FetchAllBank(ID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllActiveBank()
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GBanks.FetchAllActiveBank();

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }

    public class HouseBank
    {
        public static OperationResultInfo InsertHouseBank(int BankID, string HouseBankID, string BankName)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GHouseBanks.InsertHouseBank(BankID, HouseBankID, BankName);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo UpdateHouseBank(int ID, int BankID, string HouseBankID, int Status, string BankName)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GHouseBanks.UpdateHouseBank(ID, BankID, HouseBankID, Status, BankName);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllHouseBank(int ID, int BankID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GHouseBanks.FetchAllHouseBank(ID, BankID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllActiveHouseBank(int BankID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GHouseBanks.FetchAllActiveHouseBank(BankID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
    public class Company
    {
        public static OperationResultInfo InsertCompany(string CompanyName, string companycode)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GCompany.InsertCompany(CompanyName, companycode);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo UpdateCompany(int ID, string CompanyName, string companycode, int Status)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GCompany.UpdateCompany(ID, CompanyName, companycode, Status);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllCompany(int ID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GCompany.FetchAllCompany(ID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllActiveCompany()
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GCompany.FetchAllActiveCompany();

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }

    public class Account
    {
        public static OperationResultInfo InsertAccount(int Companyid, string BankId, string AccountId, string Currency, string Account)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GAccount.InsertAccount(Companyid, BankId, AccountId, Currency, Account);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo UpdateAccount(int ID, int Companyid, string BankId, string AccountId, string Currency, int status, string Account)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GAccount.UpdateAccount(ID, Companyid, BankId, AccountId, Currency, status, Account);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllAccount(int ID, int BankID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GAccount.FetchAllAccount(ID, BankID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllActiveAccount()
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GAccount.FetchAllActiveAccount();

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public static OperationResultInfo FetchBankByAccountID(int ID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GAccount.FetchBAnkByAccountID(ID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }

    public class SystemSetting
    {
        public static OperationResultInfo InsertSystemSetting(int BankID, string Transaction, string ExternalCode, string BankTransactionCategory, string OperationType, string BankName)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GSystemSettings.InsertSystemSettings(BankID, Transaction, ExternalCode, BankTransactionCategory, OperationType, BankName);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo UpdateSystemSettings(int ID, int BankID, string Transaction, string ExternalCode, string BankTransactionCategory, string OperationType, int Status, string BankName)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GSystemSettings.UpdateSystemSettings(ID, BankID, Transaction, ExternalCode, BankTransactionCategory, OperationType, Status, BankName);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllSystemsettings(int ID, int BankID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GSystemSettings.FetchAllSystemSettings(ID, BankID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllSystemsettingsBYBank(int ID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GSystemSettings.FetchAllSystemSettingsBYBank(ID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllActiveSystemsettings()
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GSystemSettings.FetchAllActiveSystemSettings();

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }



    public class Format
    {
        public static OperationResultInfo InsertFormat(ref int id, int BakNAme, string ColumnName, string ColumnText, string BankName)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GFormats.InsertFormats(ref id, BakNAme, ColumnName, ColumnText, BankName);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo UpdateFormat(int ID, int BakNAme, string ColumnName, string ColumnText, int Statuss, string BankName)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GFormats.UpdateFormats(ID, BakNAme, ColumnName, ColumnText, Statuss, BankName);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo UpdateFormatIndex(int ID, int Index)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GFormats.UpdateFormatIndex(ID, Index);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public static OperationResultInfo DeleteFormatByID(int ID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GFormats.DeleteFormatByID(ID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllFormat(int ID, int BankID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GFormats.FetchAllFormat(ID, BankID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo FetchAllActiveFormatByBankID(int ID)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GFormats.FetchAllActiveFormatByBankID(ID);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }


    }

    public class C2GDateGetters
    {
        public static OperationResultInfo GetDate(string MainDate)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GDateGetter.GetDate(MainDate);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo GetValueDate(string MainDate)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GDateGetter.GetValueDate(MainDate);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
    public class C2GREport
    {


        public static OperationResultInfo GetMAinAccountReport(string StartDate, string AccountNo, string Company, string Client)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GReportGetter.GetMAinReport(StartDate, AccountNo, Company, Client);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static OperationResultInfo GetUserBU(string UserId)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GReportGetter.GetUserBU(UserId);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static OperationResultInfo GetBUAccount(string BUId)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GReportGetter.GetBuAccount(BUId);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public static OperationResultInfo GetExistence(string AccountNo)
        {
            OperationResultInfo retVal;
            try
            {
                retVal = C2GReportGetter.ExistenceTest(AccountNo);

                return retVal;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}