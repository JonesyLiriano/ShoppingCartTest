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
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly ShoppingContext _context;

        public ShoppingCartItemsController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingCartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartItems>>> GetShoppingCartItems()
        {
            return await _context.ShoppingCartItems.ToListAsync();
        }

        // GET: api/ShoppingCartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartItems>> GetShoppingCartItems(int id)
        {
            var shoppingCartItems = await _context.ShoppingCartItems.FindAsync(id);

            if (shoppingCartItems == null)
            {
                return NotFound();
            }

            return shoppingCartItems;
        }

        // PUT: api/ShoppingCartItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartItems(int id, ShoppingCartItems shoppingCartItems)
        {
            if (id != shoppingCartItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCartItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartItemsExists(id))
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

        // POST: api/ShoppingCartItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ShoppingCartItems>> PostShoppingCartItems(ShoppingCartItems shoppingCartItems)
        {
            _context.ShoppingCartItems.Add(shoppingCartItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCartItems", new { id = shoppingCartItems.Id }, shoppingCartItems);
        }

        // DELETE: api/ShoppingCartItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShoppingCartItems>> DeleteShoppingCartItems(int id)
        {
            var shoppingCartItems = await _context.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItems == null)
            {
                return NotFound();
            }

            _context.ShoppingCartItems.Remove(shoppingCartItems);
            await _context.SaveChangesAsync();

            return shoppingCartItems;
        }

        private bool ShoppingCartItemsExists(int id)
        {
            return _context.ShoppingCartItems.Any(e => e.Id == id);
        }
    }
}
