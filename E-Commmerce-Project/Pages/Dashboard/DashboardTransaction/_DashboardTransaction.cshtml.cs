using DWYT.MOE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DWYT.MOE.Pages
{
    public class DashboardListModel : BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public DashboardListModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public List<orderstatus> lstOrderStatus;
        public List<productordertransactionhd> lstProductOrderTransactionHd;
        public List<productordertransactiondt> lstProductOrderTransactionDt;
        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            lstOrderStatus = context.orderstatus.Where(x => x.IsDeleted == false).ToList();
            lstProductOrderTransactionHd = context.productordertransactionhd.Where(x => x.IsDeleted == false).ToList();
            lstProductOrderTransactionHd.GroupBy(s => s.OrderStatusID).ToList();

        }

    }
}
