namespace BalanceApp.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public List<UserTask> userTasks { get; set; } = new List<UserTask>();
        public int TotalBalance { get; set; } = 0;
        public int PositiveBalance { get; set; } = 0;
        public int NegativeBalance { get; set; } = 0;
        public int ConsolidatedBalance { get; set; } = 0;
    }
}
