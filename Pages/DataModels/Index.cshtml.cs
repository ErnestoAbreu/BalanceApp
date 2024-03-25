using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BalanceApp.Data;
using BalanceApp.Models;

namespace BalanceApp.Pages.DataModels
{
    public class IndexModel : PageModel
    {
        private readonly BalanceApp.Data.BalanceAppContext _context;

        public IndexModel(BalanceApp.Data.BalanceAppContext context)
        {
            _context = context;
        }

        public IList<UserData> UserData { get;set; } = default!;

        public async Task OnGetAsync()
        {
            UserData = await _context.UserData.ToListAsync();
        }
    }
}
