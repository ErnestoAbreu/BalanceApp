using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BalanceApp.Data;
using BalanceApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BalanceApp.Pages.UserTasks
{
    public class IndexModel : PageModel
    {
        public UserData UserData { get; set; } = default!;
        public IList<UserTask> UserTask { get; set; } = default!;

        public async Task OnGetAsync()
        {
            UserData userData = null;

            if(User.Identity.IsAuthenticated)
                userData = await ApiRequests.GetUserDataAsync(User.getUserId());

            if (userData == null)
            {
                return;
            }

            UserData = userData;
            UserTask = userData.userTasks;
        }
    }
}
