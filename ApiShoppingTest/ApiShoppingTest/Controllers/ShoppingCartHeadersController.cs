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
    public class ShoppingCartHeadersController : ControllerBase
    {
        private readonly ShoppingContext _context;

        public ShoppingCartHeadersController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingCartHeaders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartHeader>>> GetShoppingCartHeader()
        {
            return await _context.ShoppingCartHeader.ToListAsync();
        }

        // GET: api/ShoppingCartHeaders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartHeader>> GetShoppingCartHeader(int id)
        {
            var shoppingCartHeader = await _context.ShoppingCartHeader.FindAsync(id);

            if (shoppingCartHeader == null)
            {
                return NotFound();
            }

            return shoppingCartHeader;
        }

        // PUT: api/ShoppingCartHeaders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartHeader(int id, ShoppingCartHeader shoppingCartHeader)
        {
            if (id != shoppingCartHeader.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCartHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartHeaderExists(id))
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

        // POST: api/ShoppingCartHeaders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ShoppingCartHeader>> PostShoppingCartHeader(ShoppingCartHeader shoppingCartHeader)
        {
            _context.ShoppingCartHeader.Add(shoppingCartHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCartHeader", new { id = shoppingCartHeader.Id }, shoppingCartHeader);
        }

        // DELETE: api/ShoppingCartHeaders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShoppingCartHeader>> DeleteShoppingCartHeader(int id)
        {
            var shoppingCartHeader = await _context.ShoppingCartHeader.FindAsync(id);
            if (shoppingCartHeader == null)
            {
                return NotFound();
            }

            _context.ShoppingCartHeader.Remove(shoppingCartHeader);
            await _context.SaveChangesAsync();

            return shoppingCartHeader;
        }

        private bool ShoppingCartHeaderExists(int id)
        {
            return _context.ShoppingCartHeader.Any(e => e.Id == id);
        }
    }
}
