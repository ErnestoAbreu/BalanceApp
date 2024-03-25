namespace BalanceApp.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public int Value { get; set; } = 0;
        public string? Description { get; set; }
        public bool IsConsolidated { get; set; }
    }
}
