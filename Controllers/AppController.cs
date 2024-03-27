using BalanceApp.Data;
using BalanceApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<ActionResult<UserData>> GetUserData(string userId)
        {
            var userData = await _context.UserData.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userData == null)
            {
                return NotFound();
            }

            var userTaks = _context.UserTasks.Where(x => x.UserDataId == userData.Id);
            userData.userTasks = userTaks.ToList();

            return Ok(userData);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<UserTask>> GetTask(int id, string userId)
        {
            var user = await _context.UserData.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                return BadRequest();
            }

            var task = await _context.UserTasks.Where(x => x.UserDataId == user.Id).FirstOrDefaultAsync(x => x.Id == id);
            
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTask(string userId, UserTask userTask)
        {
            var user = await _context.UserData.FirstOrDefaultAsync(x => x.UserId == userId);

            if(user == null)
            {
                return BadRequest();
            }

            var task = userTask;

            user.userTasks.Add(new UserTask()
            {
                Value = task.Value,
                Description = task.Description,
                UserDataId = user.Id
            }) ;

            user.TotalBalance += task.Value;

            if (task.Value > 0)
                user.PositiveBalance += task.Value;
            else
                user.NegativeBalance += task.Value;

            
            await _context.SaveChangesAsync();

            return Created();
        }
        
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<UserTask>> EditTask(int id, string userId, UserTask userTask)
        {
            var user = await _context.UserData.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                return BadRequest();
            }

            var task = userTask;

            var newTask = await _context.UserTasks.Where(x => x.UserDataId == user.Id).FirstOrDefaultAsync(x => x.Id == id);

            if (newTask == null || newTask.IsConsolidated)
            {
                return NotFound();
            }

            user.TotalBalance -= newTask.Value;

            if (newTask.Value > 0)
                user.PositiveBalance -= newTask.Value;
            else
                user.NegativeBalance -= newTask.Value;

            newTask.Value = task.Value;
            newTask.Description = task.Description;

            user.TotalBalance += newTask.Value;

            if (newTask.Value > 0)
                user.PositiveBalance += newTask.Value;
            else
                user.NegativeBalance += newTask.Value;

            await _context.SaveChangesAsync();

            return Ok(newTask);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteTask(int id, string userId)
        {
            var user = await _context.UserData.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                return BadRequest();
            }

            var task = await _context.UserTasks.Where(x => x.UserDataId == user.Id).FirstOrDefaultAsync(x => x.Id == id);

            if(task == null || task.IsConsolidated)
            {
                return NotFound();
            }

            user.TotalBalance -= task.Value;

            if (task.Value > 0)
                user.PositiveBalance -= task.Value;
            else
                user.NegativeBalance -= task.Value;

            _context.UserTasks.Remove(task);
            
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
