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
    public class ProductListModel : BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public ProductListModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public List<producthd> lstProduct;
        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            lstProduct = context.producthd.Where(x => x.IsDeleted == false).ToList();
        }

    }
}
