using DWYT.MOE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DWYT.MOE.Pages
{
    public class TransactionListModel : BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public TransactionListModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public List<orderstatus> lstOrderStatus;

        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            lstOrderStatus = context.orderstatus.Where(x => x.IsDeleted == false).ToList();
            
         }

        public ActionResult OnGetGridView(int OrderStatus)
        {

            var context = new DWYT_MOEContext();
            List<productordertransactionhd> lstTransaction = context.productordertransactionhd.Where(x => x.OrderStatusID == OrderStatus && x.IsDeleted!= true).ToList();
            return Partial("_TransactionPartial", lstTransaction);
        }
    }
}
