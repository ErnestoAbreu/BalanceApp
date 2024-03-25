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
    public class DetailsModel : PageModel
    {
        private readonly BalanceApp.Data.BalanceAppContext _context;

        public DetailsModel(BalanceApp.Data.BalanceAppContext context)
        {
            _context = context;
        }

        public UserData UserData { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userdata = await _context.UserData.FirstOrDefaultAsync(m => m.Id == id);
            if (userdata == null)
            {
                return NotFound();
            }
            else
            {
                UserData = userdata;
            }
            return Page();
        }
    }
}
