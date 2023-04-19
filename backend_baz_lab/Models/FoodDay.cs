using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace backend_baz_lab.Models
{
    public class FoodDay
    {
        public int Id { get; set; }
        public DateTime DateOfMeal { get; set; }
        public IList<Meal>? Meals { get; set; }
        public FoodDay()
        {
            Meals = new List<Meal>();
        }

        public void AddMeal(Meal meal)
        {
            Meals.Add(meal);
        }

        public void DeleteMeal(Meal meal)
        {
            Meals.Remove(meal);
        }
        public double SumKkal()
        {
            var sum = Meals.Sum(meal => meal.Kkal);
            return sum;
        }
        public string PercentFatProteinAndCarbon()
        {
            var total = Meals.Sum(meal => meal.Fats + meal.Proteins + meal.Carbohydrates);
            var percentFats = Meals.Sum(meal => meal.Fats) / total;
            var percentCarbon = Meals.Sum(meal => meal.Carbohydrates) / total;
            var percentProtein = Meals.Sum(meal => meal.Proteins) / total;
            return "Fats persent: " + percentFats + "\n" +
                   "Carbohydrates persent: " + percentCarbon + "\n" +
                   "Protein persent: " + percentProtein + "\n";
        }

        public object WhenIEatThis(Meal meal)
        {
            if (Meals.Contains(meal))
            {
                return DateOfMeal.Date.ToShortDateString();
            }
            else
            {
                return -1;
            }
            
            
        }
    }
}
