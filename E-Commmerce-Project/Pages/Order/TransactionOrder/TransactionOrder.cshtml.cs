using DWYT.MOE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace DWYT.MOE.Pages
{
    public class TransactionOrderEntryModel : BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public TransactionOrderEntryModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public productordertransactionhd transactionhd;
        public List<shipper> lstShipper;
        public List<orderstatus> lstOrderStatus;
        public bool IsAdd;
        public string ID = "";
        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            if (id == "")
            {
                IsAdd = true;
                lstShipper = context.shipper.Where(x => x.IsDeleted == false).ToList();
                lstOrderStatus = context.orderstatus.Where(x => x.IsDeleted == false).ToList();
                transactionhd = new productordertransactionhd();
            }
            else
            {
                IsAdd = false;
                lstShipper = context.shipper.Where(x => x.IsDeleted == false).ToList();
                lstOrderStatus = context.orderstatus.Where(x => x.IsDeleted == false).ToList();
                transactionhd = context.productordertransactionhd.FirstOrDefault(p => p.TransactionID == Convert.ToInt32(id));
                ID = id;
            }
        }
        public IActionResult OnPostSaveData([FromBody] dynamic FormData)
        {
            bool result = true;
            var context = new DWYT_MOEContext();
            if (FormData.hdnIsAdd == 1)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        productordertransactionhd pc = new productordertransactionhd();
                        pc.TransactionNo = FormData.TransactionNo;
                        pc.TransactionDate = DateTime.Today;
                        pc.RecevierName = FormData.Name;
                        pc.ShipperID = Convert.ToInt32(FormData.Shipper);
                        pc.ShippingAddress = FormData.Address; 
                        pc.ShippingNo = FormData.ShippingNo;
                        pc.OrderStatusID = Convert.ToInt32(FormData.OrderStatus);

                        pc.CreatedBy = Fullname;
                        pc.CreatedDate = DateTime.Now;
                        context.productordertransactionhd.Add(pc);
                        context.SaveChanges();

                        transaction.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        string message = ex.Message;
                        result = false;

                    }
                }

            }
            else
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int ID = Convert.ToInt32(FormData.hdnID);
                        productordertransactionhd pc = context.productordertransactionhd.FirstOrDefault(p => p.TransactionID == ID);
                        pc.TransactionNo = FormData.TransactionNo;
                        pc.TransactionDate = DateTime.Today;
                        pc.RecevierName = FormData.Name;
                        pc.ShipperID = Convert.ToInt32(FormData.Shipper);
                        pc.ShippingAddress = FormData.Address;
                        pc.ShippingNo = FormData.ShippingNo;
                        pc.OrderStatusID = Convert.ToInt32(FormData.OrderStatus);
                        pc.LastUpdatedBy = Fullname;
                        pc.LastUpdatedDate = DateTime.Now;
                        context.SaveChanges();

                        transaction.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        string message = ex.Message;
                        result = false;

                    }
                }
            }
            return new JsonResult(new { result });
        }

    }
}
