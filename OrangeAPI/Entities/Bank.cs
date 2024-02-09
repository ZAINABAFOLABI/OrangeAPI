namespace OrangeAPI.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string SortCode { get; set; }
        public required string AccountNumber { get; set; }
        public required string BankName { get; set; }

        public string BankAddress { get; set; } = string.Empty;
    }
}
