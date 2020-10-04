using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiShoppingTest.Models;

namespace ApiShoppingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceItemsController : ControllerBase
    {
        private readonly ShoppingContext _context;

        public InvoiceItemsController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceItems>>> GetInvoiceItems()
        {
            return await _context.InvoiceItems.ToListAsync();
        }

        // GET: api/InvoiceItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceItems>> GetInvoiceItems(int id)
        {
            var invoiceItems = await _context.InvoiceItems.FindAsync(id);

            if (invoiceItems == null)
            {
                return NotFound();
            }

            return invoiceItems;
        }

        // PUT: api/InvoiceItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceItems(int id, InvoiceItems invoiceItems)
        {
            if (id != invoiceItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceItemsExists(id))
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

        // POST: api/InvoiceItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<InvoiceItems>> PostInvoiceItems(InvoiceItems invoiceItems)
        {
            _context.InvoiceItems.Add(invoiceItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceItems", new { id = invoiceItems.Id }, invoiceItems);
        }

        // DELETE: api/InvoiceItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvoiceItems>> DeleteInvoiceItems(int id)
        {
            var invoiceItems = await _context.InvoiceItems.FindAsync(id);
            if (invoiceItems == null)
            {
                return NotFound();
            }

            _context.InvoiceItems.Remove(invoiceItems);
            await _context.SaveChangesAsync();

            return invoiceItems;
        }

        private bool InvoiceItemsExists(int id)
        {
            return _context.InvoiceItems.Any(e => e.Id == id);
        }
    }
}
