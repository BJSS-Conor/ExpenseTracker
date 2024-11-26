using ExpensesAPI.Contracts.Requests;
using ExpensesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesAPI.Services
{
    public class ExpensesService
    {
        private readonly ExpensesDbContext _context;

        public ExpensesService(ExpensesDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllExpenses() 
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<List<Expense>> GetExpensesByMonth(int month)
        {
            List<Expense> expenses = await _context.Expenses.Where(e => e.Date.Month == month).ToListAsync();
            return expenses;
        }

        public async Task<Expense> CreateExpense(CreateExpenseRequest expenseRequest) 
        { 
            Expense expense = new Expense(expenseRequest.Description, expenseRequest.Amount, expenseRequest.Date);

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return expense;
        }
        public async Task DeleteExpense(int id) 
        {            
            var expense = await _context.Expenses.SingleOrDefaultAsync(e => e.Id == id);

            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }
    }
}
