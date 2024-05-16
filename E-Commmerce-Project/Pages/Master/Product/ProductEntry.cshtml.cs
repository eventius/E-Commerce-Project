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
    public class ProductEntryModel : BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public ProductEntryModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public producthd Product;
        public List<productcategory> LstCategory;
        public bool IsAdd;
        public string ID = "";
        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            if (id == "")
            {
                IsAdd = true;
                LstCategory = context.productcategory.Where(x => x.IsDeleted == false).ToList();

                Product = new producthd();
            }
            else
            {
                IsAdd = false;
                LstCategory = context.productcategory.Where(x => x.IsDeleted == false).ToList();
                Product = context.producthd.FirstOrDefault(p => p.ProductID == Convert.ToInt32(id));
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
                        producthd pc = new producthd();
                        pc.ProductCode = FormData.ProductCode;
                        pc.ProductName = FormData.ProductName;
                        pc.ProductQuantity = Convert.ToInt32(FormData.Quantity);
                        pc.CategoryID = Convert.ToInt32(FormData.Category);
                        pc.Price = Convert.ToDecimal(FormData.Price); 
                        pc.AlertAt = Convert.ToInt32(FormData.Alert);

                        pc.CreatedBy = Fullname;
                        pc.CreatedDate = DateTime.Now;
                        context.producthd.Add(pc);
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
                        producthd pc = context.producthd.FirstOrDefault(p => p.ProductID == ID);
                        pc.ProductCode = FormData.ProductCode;
                        pc.ProductName = FormData.ProductName;
                        pc.ProductQuantity = Convert.ToInt32(FormData.Quantity);
                        pc.CategoryID = Convert.ToInt32(FormData.Category);
                        pc.Price = Convert.ToDecimal(FormData.Price);
                        pc.AlertAt = Convert.ToInt32(FormData.Alert);
                        pc.IsDeleted = FormData.IsDeleted;
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
