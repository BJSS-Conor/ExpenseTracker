namespace ExpensesAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public Expense() { }

        public Expense(string description, int amount, DateTime date) 
        {
            Description = description;
            Amount = amount;
            Date = date;
        }

        public override bool Equals(object obj)
        {            
            if (ReferenceEquals(this, obj))
                return true;
            
            if (obj == null || GetType() != obj.GetType())
                return false;
            
            var other = (Expense)obj;
            return Id == other.Id &&
                   Description == other.Description &&
                   Amount == other.Amount &&
                   Date == other.Date;
        }
    }
}
