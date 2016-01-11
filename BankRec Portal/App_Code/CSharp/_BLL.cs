using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Collections;
using System.Data.Objects;
using System.Transactions;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Security.Roles;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using EMPERMOSLibrary;
using EMPERMOSLibrary.Dal;
using Empermos._Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;


/// <summary>
/// Summary description for _BLL
/// </summary>
/// 
namespace Empermos._BLL
{
    public class VnxProjectBLL
    {
        public static List<GetProjectByUserID_Result1> GetProjectByUserID(int? UserID, int? Status)
        {
            try
            {
                var result = VnxProjectDB.GetProjectByUserID(UserID, Status);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
        //public static GetProjectByProjectID_Result GetProjectByProjectID(int ProjectID)
        //{
        //    try
        //    {
        //        var result = VnxProjectDB.GetProjectByProjectID(ProjectID);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}

        public static VnxProject GetProjectByProjectID(int ProjectID)
        {
            try
            {
                var result = VnxProjectDB.GetProjectByProjectID(ProjectID);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }

        public static DataSet FetchProjectDetails(int ProjectID)
        {
            try
            {
                DataSet result = VnxProjectDB.FetchProjectDetails(ProjectID);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }

        public static int InsertAllProjectDetails(VnxProject Project, FLTInventoryHistory InventHist, List<GetItemDetails> inventHistDetailList,
           List<VnxACtivity> activityList, List<GetEngineers_Result> EngineerList)
        {
            int retVal = 0;
            DbTransaction tran = null;
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection con = db.CreateConnection();
            con.Open();
            tran = con.BeginTransaction();
            try
            {
                int projectID = VnxProjectDB.InsertProject(Project, tran);

                InventHist.ProjectID = projectID;
                int histID = VnxProjectDB.InsertFLTInventoryHistory(InventHist, tran);

                foreach (GetItemDetails item in inventHistDetailList)
                {
                    item.HistoryID = histID;
                    VnxProjectDB.InsertFLTInventoryHistoryDetails(item, tran);
                }

                VnxProjectActivity proActivity;
                foreach (VnxACtivity item in activityList)
                {
                    proActivity = new VnxProjectActivity();
                    proActivity.ProjectID = projectID;
                    proActivity.ActivityID = item.ID;
                    VnxProjectDB.InsertVnxProjectActivity(proActivity, tran);
                }

                VnxProjectEngineer proEngineer;
                foreach (GetEngineers_Result item in EngineerList)
                {
                    proEngineer = new VnxProjectEngineer();
                    proEngineer.ProjectID = projectID;
                    proEngineer.EngineerID = item.UserID;
                    VnxProjectDB.InsertVnxProjectEngineer(proEngineer, tran);
                }
                tran.Commit();
                retVal = 1;
               
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ErrorMgt.WriteLog(ex);
                retVal = -99;
            }
            finally
            {
                con.Close();
            }
            return retVal;
        }
        public static int UpdateAllProjectDetails(VnxProject Project, string InventHist, List<GetItemDetails> inventHistDetailList,
            List<VnxACtivity> activityList, List<GetEngineers_Result> EngineerList)
        {
            int retVal = 0;
            DbTransaction tran = null;
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection con = db.CreateConnection();
            con.Open();
            tran = con.BeginTransaction();
            try
            {
                VnxProjectDB.UpdateProject(Project, tran);


                VnxProjectDB.DeleteFLTInventoryHistoryDetail(int.Parse(InventHist), tran);
                foreach (GetItemDetails item in inventHistDetailList)
                {
                    item.HistoryID = int.Parse(InventHist);
                    VnxProjectDB.InsertFLTInventoryHistoryDetails(item, tran);
                }
                VnxProjectDB.DeleteVnxProjectActivity(Project.ID, tran);
                VnxProjectActivity proActivity;
                foreach (VnxACtivity item in activityList)
                {
                    proActivity = new VnxProjectActivity();
                    proActivity.ProjectID = Project.ID;
                    proActivity.ActivityID = item.ID;
                    VnxProjectDB.InsertVnxProjectActivity(proActivity, tran);
                }
                VnxProjectDB.DeleteVnxProjectEngineer(Project.ID, tran);
                VnxProjectEngineer proEngineer;
                foreach (GetEngineers_Result item in EngineerList)
                {
                    proEngineer = new VnxProjectEngineer();
                    proEngineer.ProjectID = Project.ID;
                    proEngineer.EngineerID = item.UserID;
                    VnxProjectDB.InsertVnxProjectEngineer(proEngineer, tran);
                }
                tran.Commit();
                retVal = 1;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                ErrorMgt.WriteLog(ex);
                retVal = -99;
            }
            finally
            {
                con.Close();
            }
            return retVal;
        }
        public static int UpdateElapseProjectDetails(VnxProject Project, VnxProjectHistory ProjectHist, string InventHist, List<GetItemDetails> inventHistDetailList,
            List<VnxACtivity> activityList, List<GetEngineers_Result> EngineerList)
        {
            int retVal = 0;
            DbTransaction tran = null;
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection con = db.CreateConnection();
            con.Open();
            tran = con.BeginTransaction();
            try
            {
                VnxProjectDB.UpdateProject(Project, tran);

                VnxProjectDB.InsertProjectHistory(ProjectHist, tran);
                VnxProjectDB.DeleteFLTInventoryHistoryDetail(int.Parse(InventHist), tran);
                foreach (GetItemDetails item in inventHistDetailList)
                {
                    item.HistoryID = int.Parse(InventHist);
                    VnxProjectDB.InsertFLTInventoryHistoryDetails(item, tran);
                }
                VnxProjectDB.DeleteVnxProjectActivity(Project.ID, tran);
                VnxProjectActivity proActivity;
                foreach (VnxACtivity item in activityList)
                {
                    proActivity = new VnxProjectActivity();
                    proActivity.ProjectID = Project.ID;
                    proActivity.ActivityID = item.ID;
                    VnxProjectDB.InsertVnxProjectActivity(proActivity, tran);
                }
                VnxProjectDB.DeleteVnxProjectEngineer(Project.ID, tran);
                VnxProjectEngineer proEngineer;
                foreach (GetEngineers_Result item in EngineerList)
                {
                    proEngineer = new VnxProjectEngineer();
                    proEngineer.ProjectID = Project.ID;
                    proEngineer.EngineerID = item.UserID;
                    VnxProjectDB.InsertVnxProjectEngineer(proEngineer, tran);
                }
                tran.Commit();
                retVal = 1;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                ErrorMgt.WriteLog(ex);
                retVal = -99;
            }
            finally
            {
                con.Close();
            }
            return retVal;
        }
    }

    public class FLTItemBLL
    {
        public static List<FLTItem> FetchAllItem()
        {
            try
            {
                var result = FLTItemDB.FetchAllItem();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
    }
    public class VnxACtivityBLL
    {
        public static List<VnxACtivity> FetchAllACtivities()
        {
            try
            {
                var result = VnxACtivityDB.FetchAllACtivities();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
    }
    public class VnxEngineerBLL
    {
        public static List<GetEngineers_Result> GetEngineers()
        {
            try
            {
                var result = VnxEngineerDB.GetEngineers();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
    }

    public class VnxClientBLL
    {
        public static List<VnxClient> FetchAllClient()
        {
            try
            {
                var result = VnxClientDB.FetchAllClient();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
    }

    public class FLTItemsBLL
    {
        public static List<GetAllItems_Result> FetchAllItems()
        {
            try
            {
                var result = FLTItemsDB.FetchAllItems();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
        public static FLTItem GetSingleItem(int ID)
        {
            try
            {
                var result = FLTItemsDB.GetSingleItem(ID);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
        public static int CheckInsertItem(string Name)
        {
            try
            {
                var result = FLTItemsDB.CheckInsertItem(Name);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }
     
        public static int InsertItem(FLTItem GetItem)
        {
            try
            {
                var result = FLTItemsDB.InsertItem(GetItem);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }
        public static int CheckUpdateItem(string Name, int ID)
        {
            try
            {
                var result = FLTItemsDB.CheckUpdateItem(Name, ID);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }
        public static int UpdateItem(FLTItem GetItem)
        {
            try
            {
                var result = FLTItemsDB.UpdateItem(GetItem);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }
    }
    public class FLTSupplierBLL
    {
        public static List<GetAllSuplier_Result> FetchAllSupplier()
        {
            try
            {
                var result = FLTSupplierDB.FetchAllSupplier();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
        public static FLTSupplier FetchSingleSupplier(int ID)
        {
            try
            {
                var result = FLTSupplierDB.FetchSingleSupplier(ID);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
        public static int CheckInsertSupplier(string Name)
        {
            try
            {
                var result = FLTSupplierDB.CheckInsertSupplier(Name);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }
        public static int InsertSupplier(FLTSupplier nuSupplierValue)
        {
            try
            {
                var result = FLTSupplierDB.InsertSupplier(nuSupplierValue);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }
        public static int CheckUpdateSupplier(string Name, int ID)
        {
            try
            {
                var result = FLTSupplierDB.CheckUpdateSupplier(Name, ID);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }
        public static int UpdateSupplier(FLTSupplier nuSupplierValue)
        {
            try
            {
                var result = FLTSupplierDB.UpdateSupplier(nuSupplierValue);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }
    }
    public class FLTInventoryTxnsBLL
    {
        public static List<GetItemSupply_Result> GetItemSupply()
        {
            try
            {
                var result = FLTInventoryTxnsDB.GetItemSupply();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
        public static FLTInventoryTxn FetchSingleItemSupply(int ID)
        {
            try
            {
                var result = FLTInventoryTxnsDB.FetchSingleItemSupply(ID);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
        public static int InsertItemSupply(FLTInventoryTxn nuItemSupplyValue)
        {
            try
            {
                var result = FLTInventoryTxnsDB.InsertItemSupply(nuItemSupplyValue);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }

        public static int UpdateItemSupply(FLTInventoryTxn nuItemSupplyValue)
        {
            try
            {
                var result = FLTInventoryTxnsDB.UpdateItemSupply(nuItemSupplyValue);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return -99;
            }
        }

        public static List<GetItemForDisburse_Result> GetItemForDisburse()
        {
            try
            {
                var result = FLTInventoryTxnsDB.GetItemForDisburse();
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }
        public static List<GetItemDisburseByHistID_Result> GetItemDisburseByHistID(int HistID)
        {
            try
            {
                var result = FLTInventoryTxnsDB.GetItemDisburseByHistID(HistID);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }

        public static List<FLTItem> CheckDisbursedQty(List<FLTInventoryHistoryDetail> nuInvenHistDetail)
        {
            try
            {
                var result = FLTInventoryTxnsDB.CheckDisbursedQty(nuInvenHistDetail);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
                return null;
            }
        }


        public static int ProcessItemDisbursement(FLTInventoryHistory InventHist, List<FLTInventoryHistoryDetail> inventHistDetailList)
        {

            int retVal = 0;
            DbTransaction tran = null;
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection con = db.CreateConnection();
            con.Open();
            tran = con.BeginTransaction();
            try
            {
                VnxProjectDB.UpdateFLTInventoryHistory(InventHist, tran);
                foreach (FLTInventoryHistoryDetail item in inventHistDetailList)
                {
                    VnxProjectDB.UpdateFLTInventoryHistoryDetails(item, tran);
                    VnxProjectDB.UpdateItemQty(Convert.ToInt32(item.ItemID), Convert.ToInt32(item.Quantity), tran);
                }
                
                tran.Commit();
                retVal = 1;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                ErrorMgt.WriteLog(ex);
                retVal = -99;
            }
            finally
            {
                con.Close();
            }
            return retVal;
        }

        //public static List<FLTItem> CheckRequestQty(List<FLTInventoryHistoryDetail> nuInvenHistDetail)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.CheckRequestQty(nuInvenHistDetail);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}

        

        //public static int InsertInventoryHistory(List<FLTInventoryHistoryDetail> nuInvenHistDetail, FLTInventoryHistory nuInvenHist)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.InsertInventoryHistory(nuInvenHistDetail, nuInvenHist);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}
        //public static int UpdateInventoryHistory(List<FLTInventoryHistoryDetail> nuInvenHistDetail, int HistID)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.UpdateInventoryHistory(nuInvenHistDetail, HistID);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}
        //public static List<GetInventoryHistByRequestID_Result> FetchAllGetInventoryHistByRequestID(int RequesterID, DateTime? StartDate, DateTime? EndDate)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.FetchAllGetInventoryHistByRequestID(RequesterID, StartDate, EndDate);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static List<FLTInventoryHistoryDetail> GetInventoryByHistID(int HistID)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetInventoryByHistID(HistID);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static List<GetAllPendingItemRequests_Result> GetAllPendingItemRequests()
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetAllPendingItemRequests();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static int UpdateRequestApproval(List<FLTInventoryHistoryDetail> nuInvenHistDetail, FLTInventoryHistory nuInvenHist)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.UpdateRequestApproval(nuInvenHistDetail, nuInvenHist);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}
        //public static int DeleteItemRequestByID(int ID)
        //{
        //    try
        //    {
        //        FLTInventoryTxnsDB.DeleteItemRequestByID(ID);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}
        //public static int DeleteItemHistoryAndDetailsByHistID(int HistID)
        //{
        //    try
        //    {
        //        FLTInventoryTxnsDB.DeleteItemHistoryAndDetailsByHistID(HistID);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}
        //public static int DeleteInvenHistDetailByHistIDAndItemID(int HistID, int ItemID)
        //{
        //    try
        //    {
        //        FLTInventoryTxnsDB.DeleteInvenHistDetailByHistIDAndItemID(HistID, ItemID);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}



        //public static List<GetAllPendingDisbursement_Result> GetAllPendingDisbursement()
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetAllPendingDisbursement();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static int UpdateDisbursementApproval(List<FLTInventoryHistoryDetail> nuInvenHistDetail, FLTInventoryHistory nuInvenHist)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.UpdateDisbursementApproval(nuInvenHistDetail, nuInvenHist);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}
        //public static List<GetItemListReport_Result> GetItemListReport(int CollegeID, int? Type, int? ItemID, int? Status)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetItemListReport(CollegeID, Type, ItemID, Status);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static List<GetRequisitionHistory_Result> GetRequisitionHistory(int CollegeID, string StaffID, int? DeptID, DateTime? StartDate, DateTime? EndDate, int? Status)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetRequisitionHistory(CollegeID, StaffID, DeptID, StartDate, EndDate, Status);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static List<GetInvenHistDetailReport_Result> GetInvenHistDetailReport(int HistoryID)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetInvenHistDetailReport(HistoryID);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static List<GetSupplierReport_Result> GetSupplierReport(int CollegeID, int? SupplierID, DateTime? StartDate, DateTime? EndDate, int? ItemID)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetSupplierReport(CollegeID, SupplierID, StartDate, EndDate, ItemID);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static List<GetItemSuppliedReport_Result> GetItemSuppliedReport(int CollegeID, int SupplierID, DateTime SuppliedDate)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetItemSuppliedReport(CollegeID, SupplierID, SuppliedDate);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}

        //public static List<GetAssetItems_Result> GetAssetItems(int StaffID, int? ItemID, string AssetNo, int? CollegeID)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetAssetItems(StaffID, ItemID, AssetNo, CollegeID);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}

        //public static int InsertAssetTrack(FLTAssetTracking nuTrack)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.InsertAssetTrack(nuTrack);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}
        //public static int UpdateAssetTrack(FLTAssetTracking nuTrack)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.UpdateAssetTrack(nuTrack);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return -99;
        //    }
        //}

        //public static List<GetAssetTrackByHistDetailID_Result> GetAssetTrackByHistDetailID(int HistDetailID)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetAssetTrackByHistDetailID(HistDetailID);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
        //public static List<GetQuantityDisburseByHistID_Result> GetQuantityDisburseByHistID(int HistDetailID)
        //{
        //    try
        //    {
        //        var result = FLTInventoryTxnsDB.GetQuantityDisburseByHistID(HistDetailID);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMgt.WriteLog(ex);
        //        return null;
        //    }
        //}
    }
}
