using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using possession_backend.Models;

namespace possession_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly PossessionContext _context;

        public ItemsController(PossessionContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [Route("/InLocation/{locationId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsInLocation(int locationId)
        {
            return await _context.Items.Where(x => x.LocationId == locationId).ToListAsync();
        }

        [Route("/InContainer/{containerId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsInContainer(int containerId)
        {
            return await _context.Items.Where(x => x.ContainerId == containerId).ToListAsync();
        }

        [Route("/InCategory/{categoryId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsInCategory(int categoryId)
        {
            return await _context.Items.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        [Route("Filter")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Item>>> GetFilteredItems([FromBody]ItemsFilter filterCriteria)
        {
            if(filterCriteria.GetType()
                 .GetProperties() //get all properties on object
                 .Select(pi => pi.GetValue(filterCriteria)) //get value for the property
                 .Any(value => value != null)) // Check if one of the values is not null, if so it returns true. 
            {
                var query = _context.Items.AsQueryable();

                query = query.Where(i =>
                    (!filterCriteria.ItemId.HasValue || filterCriteria.ItemId == i.ItemId)
                    && (filterCriteria.Name.IsNullOrEmpty() || i.Name.Contains(filterCriteria.Name))
                    && (filterCriteria.Description.IsNullOrEmpty() || i.Description.Contains(filterCriteria.Description))
                    && (!filterCriteria.MaxDate.HasValue || i.PurchaseDate.Ticks <= filterCriteria.MaxDate.Value.Ticks)
                    && (!filterCriteria.MinDate.HasValue || i.PurchaseDate.Ticks >= filterCriteria.MinDate.Value.Ticks)
                    && (!filterCriteria.MinPrice.HasValue || i.OriginalPrice >= filterCriteria.MinPrice)
                    && (!filterCriteria.MaxPrice.HasValue || i.OriginalPrice <= filterCriteria.MaxPrice)
                    && (!filterCriteria.MaxPrice.HasValue || i.OriginalPrice <= filterCriteria.MaxPrice)
                    && (!filterCriteria.CategoryId.HasValue || i.CategoryId == filterCriteria.CategoryId)
                    && (!filterCriteria.ContainerId.HasValue || i.CategoryId == filterCriteria.ContainerId)
                    && (!filterCriteria.LocationId.HasValue || i.CategoryId == filterCriteria.LocationId)
                );

                return await query.ToListAsync();
            }
            else
            {
                return await _context.Items.ToListAsync();
            }

        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
