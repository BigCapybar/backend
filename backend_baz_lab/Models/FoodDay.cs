using backend_baz_lab.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text.Json;

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
        public BSHUPercent PercentFatProteinAndCarbon()
        {
            var kkal = Meals.Sum(meal => meal.Kkal);
            var protein = Meals.Sum(meal => meal.Proteins);
            var fats = Meals.Sum(meal => meal.Fats);
            var carbons = Meals.Sum(meal => meal.Carbohydrates);

            //KBSHU kBSHU= new KBSHU(kkal, protein, fats, carbons);
            //var BSHUPercent = (BSHUPercent)kBSHU;

            var bSHUPercent = new BSHUPercent(kkal, protein, fats, carbons);
            return bSHUPercent;
        }

        public DateTime WhenIEatThis(Meal meal)
        {
            if (Meals.Contains(meal))
            {
                return DateOfMeal.Date;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
    
}
