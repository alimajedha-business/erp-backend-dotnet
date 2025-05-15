namespace Accounting.Application.DTOs
{
    public class LedgerDTo
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}