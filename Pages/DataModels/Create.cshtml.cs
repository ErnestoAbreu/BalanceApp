using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BalanceApp.Data;
using BalanceApp.Models;

namespace BalanceApp.Pages.DataModels
{
    public class CreateModel : PageModel
    {
        private readonly BalanceApp.Data.BalanceAppContext _context;

        public CreateModel(BalanceApp.Data.BalanceAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserData UserData { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UserData.Add(UserData);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
