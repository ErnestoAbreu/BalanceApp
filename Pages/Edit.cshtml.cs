using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BalanceApp.Data;
using BalanceApp.Models;

namespace BalanceApp.Pages
{
    public class EditModel : PageModel
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if( await ApiRequests.EditTask(UserTask.Id, User.getUserId(), UserTask) )
                return RedirectToPage("./Index");
            else
                return Page();
        }
    }
}
