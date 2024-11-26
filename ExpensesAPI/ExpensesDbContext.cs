using ExpensesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesAPI
{
    public class ExpensesDbContext : DbContext
    {
        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
    }
}
