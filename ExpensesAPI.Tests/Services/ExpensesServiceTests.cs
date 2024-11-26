using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpensesAPI.Contracts.Requests;
using ExpensesAPI.Models;
using ExpensesAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpensesAPI.Tests.Services
{
    public class ExpensesServiceTests
    {
        private ExpensesService _expensesService;
        private ExpensesDbContext _expensesDbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ExpensesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _expensesDbContext = new ExpensesDbContext(options);

            _expensesDbContext.Expenses.AddRange(new List<Expense>
            {
                new Expense("Lunch", 15, new DateTime(2024,11,25)),
                new Expense("Train", 25, new DateTime(2024,11,25)),
                new Expense("Hotel", 30, new DateTime(2024,11,25))
            });
            _expensesDbContext.SaveChanges();

            _expensesService = new ExpensesService(_expensesDbContext);
        }

        [Test]
        public async Task GetAllExpenses_Test()
        {                   
            List<Expense> expenses = await _expensesService.GetAllExpenses();
            List<Expense> expectedExpenses = new List<Expense>()
            {
                new Expense() { Id = 1, Description = "Lunch", Amount = 15, Date = new DateTime(2024,11,25) },
                new Expense() { Id = 2, Description = "Train", Amount = 25, Date = new DateTime(2024,11,25) },
                new Expense() { Id = 3, Description = "Hotel", Amount = 30, Date = new DateTime(2024,11,25) }
            };
            
            Assert.That(expenses, Is.EqualTo(expectedExpenses));
        }

        [Test]
        public async Task CreateExpense_ValidExpense()
        {
            CreateExpenseRequest expenseReq = new CreateExpenseRequest("Dinner", 27, new DateTime(2024, 12, 5));
            Expense expectedExpense = new Expense() { Id = 4, Description = "Dinner", Amount = 27, Date = new DateTime(2024, 12, 5) };

            Expense createdExpense = await _expensesService.CreateExpense(expenseReq);

            Assert.That(createdExpense, Is.EqualTo(expectedExpense));

            List<Expense> expectedExpenses = new List<Expense>()
            {
                new Expense() { Id = 1, Description = "Lunch", Amount = 15, Date = new DateTime(2024,11,25) },
                new Expense() { Id = 2, Description = "Train", Amount = 25, Date = new DateTime(2024,11,25) },
                new Expense() { Id = 3, Description = "Hotel", Amount = 30, Date = new DateTime(2024,11,25) },
                new Expense() { Id = 4, Description = "Dinner", Amount = 27, Date = new DateTime(2024, 12, 5) }
            };
            List<Expense> resultingExpenses = await _expensesService.GetAllExpenses();

            Assert.That(resultingExpenses, Is.EqualTo(expectedExpenses));
        }

        [Test]
        public async Task DeleteExpense_Test()
        {
            int index = 2;
            List<Expense> expectedExpenses = new List<Expense>()
            {
                new Expense() { Id = 1, Description = "Lunch", Amount = 15, Date = new DateTime(2024,11,25) },                
                new Expense() { Id = 3, Description = "Hotel", Amount = 30, Date = new DateTime(2024,11,25) },                
            };

            await _expensesService.DeleteExpense(index);
            List<Expense> resultingExpenses = await _expensesService.GetAllExpenses();

            Assert.That(resultingExpenses, Is.EqualTo(expectedExpenses));
        }

        [TearDown]
        public void TearDown()
        {
            _expensesDbContext.Dispose();
        }
    }
}
