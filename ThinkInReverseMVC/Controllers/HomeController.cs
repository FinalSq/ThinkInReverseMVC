using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThinkInReverseMVC.Models;

namespace ThinkInReverseMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ThinkInReverse()
        {
            Palindrome model = new();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThinkInReverse(Palindrome palindrome)
        {
            string inputWord = palindrome.InputWord;
            string revWord = "";

            for (int i = inputWord.Length - 1; i >= 0; i--)
            {
                revWord += inputWord[i];
            }

            palindrome.RevWord = revWord;

            revWord = Regex.Replace(revWord.ToLower(), @"\s+", "");
            inputWord = Regex.Replace(inputWord.ToLower(), @"\s+", "");


            if (revWord == inputWord)
            {
                palindrome.IsPalindrome = true;
                palindrome.Message = $"Success! {palindrome.InputWord} is a Palindrome";
            }
            else
            {
                palindrome.IsPalindrome = false;
                palindrome.Message = $"Failure! {palindrome.InputWord} is not a Palindrome";
            }


            return View(palindrome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
