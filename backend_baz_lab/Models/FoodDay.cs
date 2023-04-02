using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace backend_baz_lab.Models
{
    public class FoodDay
    {
        public int Id { get; set; }
        public DateTime DateOfMeal { get; set; }
        public List<Meal> Meals { get; set; }

        public void AddMealinList(Meal meal)
        {
            Meals.Add(meal);
        }

        [HttpGet]
        public string SumKkalofDay()
        {
            double sum = 0;
            foreach (var meal in Meals)
            {
                sum = meal.Kkal;
            }
            return "Sum Kkal: " + sum;
        }





    }
}
