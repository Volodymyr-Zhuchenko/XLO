using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ItProger.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ItProger.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly AppDbContext _context;

        public UserAccountController(AppDbContext context)
        {
            _context = context;
        }

        // Дія для перегляду інформації користувача
        public IActionResult ViewAccount()
        {
            var email = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("LogIn", "Contacts"); // Якщо email відсутній, редиректимо на сторінку входу
            }

            // Шукаємо контакт за email
            var contact = _context.Contacts.FirstOrDefault(c => c.Email == email);

            if (contact == null)
            {
                return NotFound(); // Якщо контакт не знайдений
            }

            return View(contact); // Передаємо контакт в представлення
        }
    }
}
