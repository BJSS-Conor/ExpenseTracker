namespace ExpensesAPI.Contracts.Requests
{
    public class CreateExpenseRequest
    {     
        public string Description { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public CreateExpenseRequest(string description, int amount, DateTime date) 
        {            
            Description = description;
            Amount = amount;
            Date = date;
        }
    }
}
