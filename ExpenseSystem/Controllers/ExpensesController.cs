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

        public ExpensesController(AppDbContext context)
        {
            _context = context;
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
        [HttpPut("approve")]
        public async Task<IActionResult> ApproveExpense(Expense expense) {
            expense.Status = approve;
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
            return await PutExpense(expense.Id, expense);
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
