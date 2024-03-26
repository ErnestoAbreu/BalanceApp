using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BalanceApp.Models;

namespace BalanceApp.Data
{
    public class BalanceAppContext : DbContext
    {
        public BalanceAppContext (DbContextOptions<BalanceAppContext> options)
            : base(options)
        {
        }

        public DbSet<UserData> UserData { get; set; } = default!;
        public DbSet<UserTask> UserTasks { get; set; } = default!;
    }
}
