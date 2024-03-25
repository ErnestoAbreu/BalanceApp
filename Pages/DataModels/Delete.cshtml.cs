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
    public class DeleteModel : PageModel
    {
        private readonly BalanceApp.Data.BalanceAppContext _context;

        public DeleteModel(BalanceApp.Data.BalanceAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userdata = await _context.UserData.FindAsync(id);
            if (userdata != null)
            {
                UserData = userdata;
                _context.UserData.Remove(UserData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
