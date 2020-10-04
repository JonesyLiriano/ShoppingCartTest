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
    public class ProductsActivitiesController : ControllerBase
    {
        private readonly ShoppingContext _context;

        public ProductsActivitiesController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: api/ProductsActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsActivity>>> GetProductsActivity()
        {
            return await _context.ProductsActivity.ToListAsync();
        }

        // GET: api/ProductsActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsActivity>> GetProductsActivity(int id)
        {
            var productsActivity = await _context.ProductsActivity.FindAsync(id);

            if (productsActivity == null)
            {
                return NotFound();
            }

            return productsActivity;
        }

        // PUT: api/ProductsActivities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductsActivity(int id, ProductsActivity productsActivity)
        {
            if (id != productsActivity.IdProduct)
            {
                return BadRequest();
            }

            _context.Entry(productsActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsActivityExists(id))
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

        // POST: api/ProductsActivities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductsActivity>> PostProductsActivity(ProductsActivity productsActivity)
        {
            _context.ProductsActivity.Add(productsActivity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductsActivityExists(productsActivity.IdProduct))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductsActivity", new { id = productsActivity.IdProduct }, productsActivity);
        }

        // DELETE: api/ProductsActivities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductsActivity>> DeleteProductsActivity(int id)
        {
            var productsActivity = await _context.ProductsActivity.FindAsync(id);
            if (productsActivity == null)
            {
                return NotFound();
            }

            _context.ProductsActivity.Remove(productsActivity);
            await _context.SaveChangesAsync();

            return productsActivity;
        }

        private bool ProductsActivityExists(int id)
        {
            return _context.ProductsActivity.Any(e => e.IdProduct == id);
        }
    }
}
