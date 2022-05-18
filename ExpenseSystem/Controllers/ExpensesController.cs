using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseSystem.Models;

namespace ExpenseSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {   
        private readonly AppDbContext _context;
        private const string approve = "Approved";
        private const string reject = "Rejected";
        private const string review = "Under Review";
        private const string paid = "Paid";

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }
       
        private async Task<IActionResult> UpdateExpDue(Expense expense) {
            var employee = _context.Employees.SingleOrDefault(x => x.Id == expense.EmployeeId);
            if (employee == null) { throw new Exception("No employee found"); }
            employee.ExpensesDue = (expense.Status == approve ? (employee.ExpensesDue + expense.Total) : employee.ExpensesDue);
            await _context.SaveChangesAsync();
            return Ok();
        }
        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
          if (_context.Expenses == null)
          {
              return NotFound();
          }
            return await _context.Expenses.Include(x => x.Employee).ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
          if (_context.Expenses == null)
          {
              return NotFound();
          }
            var expense = await _context.Expenses.Include(x => x.Employee)
                                            .Include(x => x.ExpenseLines)
                                            .ThenInclude(x => x.Item)
                                            .SingleOrDefaultAsync(x => x.Id == id );

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }
        //return approved
        [HttpGet("approved")] 
        public async Task<ActionResult<IEnumerable<Expense>>> GetApproved() {

            return await _context.Expenses.Where(x => x.Status == approve).ToListAsync();
        }
        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //Approve unconditionally
        //modify to update employee>expense due with expense total
        [HttpPut("approve")]
        public async Task<IActionResult> ApproveExpense(Expense expense) {
            expense.Status = approve;
            await UpdateExpDue(expense);
            return await PutExpense(expense.Id, expense);
        }

        //Reject unconditionally
        [HttpPut("reject")]
        public async Task<IActionResult> RejectExpense(Expense expense) {
            expense.Status = reject;
            return await PutExpense(expense.Id, expense);
        }

        //approve <=75, reject >75
        [HttpPut("review")]
        public async Task<IActionResult> ReviewExpense(Expense expense) {
            expense.Status = (expense.Total <= 75 ? approve : review);
            await UpdateExpDue(expense);
            return await PutExpense(expense.Id, expense);
        }
        [HttpPut("payexpense/{id}")]
        public async Task<IActionResult> PayExpense(int id) {
            var exp = _context.Expenses.SingleOrDefault(x => x.Id == id);
            if (exp == null) { throw new Exception("No expense found"); }
            var employee = _context.Employees.SingleOrDefault(x => x.Id == exp.EmployeeId);
            if (employee == null) { throw new Exception("No employee found"); }
            if (exp.Status != approve) { throw new Exception("Expense not approved"); }
            employee.ExpensesPaid += exp.Total;
            employee.ExpensesDue -= exp.Total;
            exp.Status = paid;
            await _context.SaveChangesAsync();
            return Ok();
        }
        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
          if (_context.Expenses == null)
          {
              return Problem("Entity set 'AppDbContext.Expenses'  is null.");
          }
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            var employee = _context.Employees.SingleOrDefault(x => x.Id == expense.EmployeeId);
            if (employee == null) { throw new Exception("No employee found"); }
            employee.ExpensesDue = (expense.Status == approve ? (employee.ExpensesDue + expense.Total) : employee.ExpensesDue);
            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if (_context.Expenses == null)
            {
                return NotFound();
            }
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return (_context.Expenses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
