using BalanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BalanceApp.Pages
{
    public class ConsolidateModel : PageModel
    {
        [BindProperty]
        public UserTask UserTask { get; set; } = default!;
        public async Task<ActionResult> OnGetAsync(int id)
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

        public async Task<ActionResult> OnPostAsync(int id)
        {
            if (await ApiRequests.ConsolidateTaskAsync(id, User.getUserId()))
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
