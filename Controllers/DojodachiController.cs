using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dojodachi.Models;
using System;
namespace dojodachi.Controllers
{
    public class DojodachiController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            Dojodachi pet = new Dojodachi();
            HttpContext.Session.SetObjectAsJson("pet", pet);
            return View("Index", pet);
        }

        [HttpPost("Feed")]
        public IActionResult Feed()
        {
            Dojodachi pet = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            Random randInt = new Random();
            int randChance = randInt.Next(1, 5);

            if ((pet.Meal - 1) < 0)
            {
            HttpContext.Session.SetString("Message", "Your pet is out of meals!");
            HttpContext.Session.SetObjectAsJson("pet", pet);
            HttpContext.Session.SetString("Image", "~/images/sad-cat.png");
            }
            else{
            pet.Meal -= 1;
                if (pet.Energy > 0)
                {
                    pet.Energy -= 5;
                    if (randChance == 1)
                    {
                        HttpContext.Session.SetObjectAsJson("pet", pet);
                        HttpContext.Session.SetString("Message", "You fed your dojodachi but he/she didn't like it. Fullness: +0. Energy: - 5");
                        HttpContext.Session.SetString("Image", "~/images/neutral-cat.png");
                        return RedirectToAction("Playing");
                    }
                    else{
                        Random rand = new Random();
                        int randFullness = rand.Next(5, 11);
                        pet.Fullness += randFullness;
                        string randFullnessString = randFullness.ToString();
                        string message = "You fed your dojodachi. Fullness: +" + randFullnessString + ". Energy: - 5";
                        HttpContext.Session.SetString("Message", message);
                        HttpContext.Session.SetObjectAsJson("pet", pet);
                        HttpContext.Session.SetString("Image", "~/images/happy-cat.png");
                        return RedirectToAction("Playing");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Message", "Your dojodachi is out of energy!");
                    HttpContext.Session.SetObjectAsJson("pet", pet);
                    HttpContext.Session.SetString("Image", "~/images/sad-cat.png");
                    return RedirectToAction("Playing");
                }

            }
            return RedirectToAction("Playing");
        }
        [HttpPost("Play")]
        public IActionResult Play()
        {
            Dojodachi pet = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            Random randInt = new Random();
            int randChance = randInt.Next(1, 5);
            if (pet.Energy > 0)
            {
                pet.Energy -= 5;
                if (randChance == 1)
                {
                    HttpContext.Session.SetString("Message", "You played with your dojodachi but he/she didn't like it. Happiness: +0. Energy: - 5");
                    HttpContext.Session.SetObjectAsJson("pet", pet);
                    HttpContext.Session.SetString("Image", "~/images/neutral-cat.png");
                }
                else{
                    Random rand = new Random();
                    int randHappiness = rand.Next(5, 11);
                    pet.Happiness += randHappiness;
                    string randHappinessString = randHappiness.ToString();
                    string message = "You played your dojodachi. Happiness: +" + randHappinessString + ". Energy: - 5";
                    HttpContext.Session.SetString("Message", message);
                    HttpContext.Session.SetObjectAsJson("pet", pet);
                    HttpContext.Session.SetString("Image", "~/images/happy-cat.png");
                }
                return RedirectToAction("Playing");
            }
            else
            {
                HttpContext.Session.SetString("Message", "Your dojodachi is out of energy!");
                HttpContext.Session.SetObjectAsJson("pet", pet);
                HttpContext.Session.SetString("Image", "~/images/sad-cat.png");
                return RedirectToAction("Playing");
            }
        }
        [HttpPost("Work")]
        public IActionResult Work()
        {
            Dojodachi pet = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            if (pet.Energy > 0)
            {
                Random rand = new Random();
                int randMeal = rand.Next(1,4);
                pet.Meal += randMeal;
                pet.Energy -= 5;
                string randMealString = randMeal.ToString();
                string message = "Your dojodachi worked hard! Meal: +" + randMealString + ". Energy: -5";
                HttpContext.Session.SetString("Message", message);
                HttpContext.Session.SetObjectAsJson("pet", pet);
                HttpContext.Session.SetString("Image", "~/images/neutral-cat.png");
                return RedirectToAction("Playing");
            }
            else
            {
                HttpContext.Session.SetString("Message", "Your dojodachi is out of energy!");
                HttpContext.Session.SetObjectAsJson("pet", pet);
                HttpContext.Session.SetString("Image", "~/images/sad-cat.png");
                return RedirectToAction("Playing");
            }
        }
        [HttpPost("Sleep")]
        public IActionResult Sleep()
        {
            Dojodachi pet = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            pet.Energy += 15;
            pet.Fullness -= 5;
            pet.Happiness -= 5;
            HttpContext.Session.SetString("Message", "Your Dojodachi slept. Fullness: -5. Happiness: -5. Energy: +15.");
            HttpContext.Session.SetObjectAsJson("pet", pet);
            HttpContext.Session.SetString("Image", "~/images/happy-cat.png");
            return RedirectToAction("Playing");
        }
        [HttpGet("dojodachi")]
        public IActionResult Playing()
        {
            Dojodachi pet = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            if (pet.Energy >= 100 && pet.Happiness >= 100 && pet.Happiness >= 100)
            {
                return RedirectToAction("Win");
            }
            else if (pet.Fullness <= 0 || pet.Happiness <= 0)
            {
                return RedirectToAction("Lose");
            }
            else
            {
                ViewBag.Message = HttpContext.Session.GetString("Message");
                string image = HttpContext.Session.GetString("Image");
                ViewBag.Image = Url.Content(image);
                return View("Playing", pet);
            }
        }
        [HttpGet("win")]
        public IActionResult Win()
        {
            Dojodachi pet = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            ViewBag.Message = "Congratulations! You Won!";
            return View("Win", pet);
        }

        [HttpGet("lose")]
        public IActionResult Lose()
        {
            Dojodachi pet = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            ViewBag.Message = "Your Dojodachi has passed away...";
            return View("Lose", pet);
        }
    }
}