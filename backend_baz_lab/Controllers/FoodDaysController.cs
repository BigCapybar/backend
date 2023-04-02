using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_baz_lab.Models;

namespace backend_baz_lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodDaysController : ControllerBase
    {
        private readonly FoodDayContext _context;

        public FoodDaysController(FoodDayContext context)
        {
            _context = context;
        }

        // GET: api/FoodDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodDay>>> GetFoodDay()
        {
          if (_context.FoodDay == null)
          {
              return NotFound();
          }
            return await _context.FoodDay.ToListAsync();
        }

        // GET: api/FoodDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodDay>> GetFoodDay(int id)
        {
          if (_context.FoodDay == null)
          {
              return NotFound();
          }
            var foodDay = await _context.FoodDay.FindAsync(id);

            if (foodDay == null)
            {
                return NotFound();
            }

            return foodDay;
        }

        // PUT: api/FoodDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodDay(int id, FoodDay foodDay)
        {
            if (id != foodDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodDayExists(id))
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

        // POST: api/FoodDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodDay>> PostFoodDay(FoodDay foodDay)
        {
          if (_context.FoodDay == null)
          {
              return Problem("Entity set 'FoodDayContext.FoodDay'  is null.");
          }
            _context.FoodDay.Add(foodDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodDay", new { id = foodDay.Id }, foodDay);
        }

        // DELETE: api/FoodDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodDay(int id)
        {
            if (_context.FoodDay == null)
            {
                return NotFound();
            }
            var foodDay = await _context.FoodDay.FindAsync(id);
            if (foodDay == null)
            {
                return NotFound();
            }

            _context.FoodDay.Remove(foodDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodDayExists(int id)
        {
            return (_context.FoodDay?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        ////мои функции
        //// GET: api/FoodDays // вывод количества каллорий за день
        //[Route("SumKKaloDay")]
        //[HttpGet]

        //public ActionResult<string> SumKKaloDay(int idDay)
        //{
        //    if (_context.FoodDay == null)
        //    {
        //        return NotFound();
        //    }
        //    var foodDay = _context.FoodDay.FindAsync(idDay);

        //    if (foodDay == null)
        //    {
        //        return NotFound();
        //    }
        //    return "12241";
        //}

        //[Route("AddMeal")]
        //[HttpPost] //закидываес еду в список
        //public async Task<ActionResult<FoodDay>> AddMealinFoodDay(int idDay, int idMeal)
        //{
        //    if (_context.FoodDay == null)
        //    {
        //        return NotFound();
        //    }
        //    var foodDay = await _context.FoodDay.FindAsync(idDay);
        //    var meal = await _context.Meal.FindAsync(idMeal);
        //    foodDay.AddMealinList(meal);

        //    if (foodDay == null)
        //    {
        //        return NotFound();
        //    }

        //    return foodDay;
        //}
    }

    


}
