using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BalanceApp.Data;
using BalanceApp.Models;

namespace BalanceApp.Pages
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public UserTask UserTask { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            UserTask userTask = null;

            userTask = await ApiRequests.GetTaskAsync(id, User.getUserId());

            if (userTask == null)
            {
                return NotFound();
            }

            UserTask = userTask;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (await ApiRequests.DeleteTaskAsync(id, User.getUserId()))
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
