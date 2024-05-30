using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SophaTemp.Controllers
{

    public class ContactController : Controller
    {
        private static List<ContactMessage> messages = new List<ContactMessage>();

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactMessage message)
        {
            if (ModelState.IsValid)
            {
                message.Date = DateTime.Now;
                messages.Add(message);
                return RedirectToAction("Contact");
            }
            return View(message);
        }

        [HttpGet]
        public IActionResult AdminMessages()
        {
            return View(messages);
        }
    }

    public class ContactMessage
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
