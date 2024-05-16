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
    public class CategoryEntryModel : BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public CategoryEntryModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public productcategory ProductCategory;
        public bool IsAdd;
        public string ID = "";
        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            if (string.IsNullOrEmpty(id))
            {
                IsAdd = true;
                ProductCategory = new productcategory();
            }
            else
            {
                IsAdd = false;
                ProductCategory = context.productcategory.FirstOrDefault(p => p.CategoryID == Convert.ToInt32(id));
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
                        productcategory pc = new productcategory();
                        pc.CategoryName = FormData.CategoryName;
                        pc.CategoryCode = FormData.CategoryCode;
                        pc.CreatedBy = UserName;
                        pc.CreatedDate = DateTime.Now;
                        context.productcategory.Add(pc);
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
                        productcategory pc = context.productcategory.FirstOrDefault(p => p.CategoryID == ID);
                        pc.CategoryName = FormData.CategoryName;
                        pc.CategoryCode = FormData.CategoryCode;
                        pc.IsDeleted = FormData.IsDeleted;
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
