namespace Roulette.Models
{
    public class PlayerBetModel
    {
        public int PlayerBetId { get; set; }
        public int PlayerId { get; set; }
        public int BetId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public  decimal PayoutValue { get; set; }
        public int ResultId { get; set; }

    }
}
