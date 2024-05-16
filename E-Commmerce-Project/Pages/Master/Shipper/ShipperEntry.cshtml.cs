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
    public class ShipperEntryModel: BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public ShipperEntryModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public shipper shipper;
        public bool IsAdd;
        public string ID = "";
        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            if (string.IsNullOrEmpty(id))
            {
                IsAdd = true;
                shipper = new shipper();
            }
            else
            {
                IsAdd = false;
                shipper = context.shipper.FirstOrDefault(p => p.ShipperID== Convert.ToInt32(id));
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
                        shipper pc = new shipper();

                        pc.ShipperName = FormData.ShipperName;
                        pc.CreatedBy = UserName;
                        pc.CreatedDate = DateTime.Now;
                        context.shipper.Add(pc);
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

                        shipper pc = context.shipper.FirstOrDefault(p => p.ShipperID == ID);
                        pc.ShipperName = FormData.ShipperName;
                        pc.LastUpdatedBy = UserName;
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
