using BalanceApp.Data;
using BalanceApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace BalanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly BalanceAppContext _context;

        public AppController(BalanceAppContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<UserData>> GetUserData(string userId)
        {
            var userData = _context.UserData.FirstOrDefault(x => x.UserId == userId);

            if(userData == null)
            {
                return NotFound();
            }

            var userTaks = _context.UserTasks.Where(x => x.UserDataId == userData.Id);
            userData.userTasks = userTaks.ToList();

            return Ok(userData);
        }

        [HttpPost]
        public async Task<ActionResult> PostUserData(string userId)
        {
            var user = _context.UserData.FirstOrDefault(x => x.UserId == userId);

            if(user == null)
            {
                _context.Add(new UserData() { 
                    UserId = userId
                });

                await _context.SaveChangesAsync();

                return Created();
            }

            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult> PostTask(string userId, UserTask task)
        {
            var user = _context.UserData.FirstOrDefault(x => x.UserId == userId);

            if(user == null)
            {
                return NotFound();
            }

            user.userTasks.Add(task);

            await _context.SaveChangesAsync();

            return Created();
        }
    }
}
