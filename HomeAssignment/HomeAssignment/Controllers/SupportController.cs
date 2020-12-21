using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignment.Controllers
{
    public class SupportController : Controller
    {
        [HttpGet] 
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string email, string message) // parameters need to match the name in input/textarea in Contact.cshtml
        {
            if(string.IsNullOrEmpty(email)) { 
                ViewData["danger"] = "Email was left empty";
            }
            else if(string.IsNullOrEmpty(message)) {
                ViewData["danger"] = "Message was left empty";
            } 
            else
            {
                ViewData["feedback"] = "Thank you for your query, we will get back to your shortly";
            }
            return View();
        }
    }
}
