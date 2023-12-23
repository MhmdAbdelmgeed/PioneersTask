namespace Business
{
    public class Summary
    {
        public int NumberOfTransactions { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemainingAmount { get; set; }

        public Summary()
        {
        }

        public Summary(int numberOfTransactions, decimal totalAmount, decimal remainingAmount)
        {
            NumberOfTransactions = numberOfTransactions;
            TotalAmount = totalAmount;
            RemainingAmount = remainingAmount;
        }
    }
}
