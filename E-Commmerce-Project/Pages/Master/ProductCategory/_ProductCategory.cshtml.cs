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
    public class CategoryListModel : BasePage
    {
        private readonly ILogger<DashboardModel> _logger;

        public CategoryListModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public List<productcategory> lstCategory;
        protected override void InitializeControl(string id)
        {
            var context = new DWYT_MOEContext();
            lstCategory = context.productcategory.Where(x => x.IsDeleted == false).ToList();
        }

    }
}
