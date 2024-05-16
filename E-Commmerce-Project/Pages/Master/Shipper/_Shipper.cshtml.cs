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
    public class ShiperListModel : BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public ShiperListModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public List<shipper> lstShipper;
        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            lstShipper = context.shipper.Where(x => x.IsDeleted == false).ToList();
        }

    }
}
