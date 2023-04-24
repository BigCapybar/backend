using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_baz_lab.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Text.Json.Nodes;
using NuGet.Protocol;

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
            return await _context.FoodDay.Include(m => m.Meals).ToListAsync();
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


        //мои функции
        //GET: api/FoodDays // вывод количества каллорий за день
        [HttpGet]
        [Route("SumKKalofDay")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<double>> SumKKalofDay(int idDay)
        {
            if (_context.FoodDay == null)
            {
                return NotFound();
            }
            var foodDay = _context.FoodDay.Include(m => m.Meals).FirstOrDefault(fd => fd.Id == idDay);

            if (foodDay == null)
            {
                return BadRequest();
            }
            return foodDay.SumKkal();

        }

        [HttpGet]
        [Route("PercentFPC")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<BSHUPercent>> PercentFatProteinAndCarbon(int idDay)
        {
            if (_context.FoodDay == null)
            {
                return NotFound();
            }
            var foodDay = _context.FoodDay.Include(m => m.Meals).FirstOrDefault(fd => fd.Id == idDay);

            if (foodDay == null)
            {
                return BadRequest();
            }
            
            var result = foodDay.PercentFatProteinAndCarbon();

            return result;
        }

        [HttpGet]
        [Route("LastTimeIEatThis")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<DateTime>> lastTimeIEatThis(int idMeal)
        {
            if (_context.FoodDay == null)
            {
                return NotFound();
            }
            var foodDay = _context.FoodDay.Include(m => m.Meals).OrderByDescending(data => data.DateOfMeal).FirstOrDefault(fd => fd.Meals.Any(m => m.Id == idMeal));

            var meal = _context.Meal.Where(o => o.Id == idMeal).FirstOrDefault();
            if (foodDay == null)
                return BadRequest();

            return foodDay.WhenIEatThis(meal);
        }

        [HttpPut()]
        [Route("AddMeal")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddMeal(int dayId, int MealId)
        {
            var meal = _context.Meal.Where(o => o.Id == MealId).FirstOrDefault();

            var foodDay = _context.FoodDay.Where(p => p.Id == dayId).FirstOrDefault();

            foodDay.AddMeal(meal);
            _context.Entry(foodDay).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete()]
        [Route("DeleteMeal")]
        [Authorize(Roles = "user")]
        
        public async Task<IActionResult> DeleteMeal(int dayId, int MealId)
        {
            var meal = _context.Meal.Where(o => o.Id == MealId).FirstOrDefault();

            var foodDay = _context.FoodDay.Where(p => p.Id == dayId).FirstOrDefault();

            foodDay.DeleteMeal(meal);
            _context.Entry(foodDay).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

    }

}
