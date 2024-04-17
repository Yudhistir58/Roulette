namespace Roulette.Models
{
    public class PlayerBetModel
    {
        public int PlayerBetId { get; set; }
        public int PlayerId { get; set; }
        public int BetId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public  decimal Payout { get; set; }
        public int ResultId { get; set; }

    }
}
