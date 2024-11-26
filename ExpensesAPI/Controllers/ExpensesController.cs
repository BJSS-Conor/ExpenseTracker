using ExpensesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesAPI.Tests.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpensesService _expensesService;

        public ExpensesController(ExpensesService expensesService)
        {
            _expensesService = expensesService;
        }
    }
}
