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
            // Отримуємо email користувача з сесії
            var email = HttpContext.Session.GetString("UserEmail");

            // Перевіряємо, чи є email у сесії
            if (string.IsNullOrEmpty(email))
            {
                TempData["ErrorMessage"] = "Будь ласка, увійдіть у систему, щоб переглянути обліковий запис.";
                return RedirectToAction("LogIn", "Contacts"); // Редирект на сторінку входу
            }

            // Шукаємо контакт за email
            var contact = _context.Contacts
                                  .FirstOrDefault(c => c.Email.ToLower() == email.ToLower());

            // Якщо контакт не знайдено
            if (contact == null)
            {
                TempData["ErrorMessage"] = "Контакт не знайдено.";
                return RedirectToAction("LogIn", "Contacts");
            }


            // Повертаємо контакт у вигляді моделі
            return View(contact);
        }
    }
}
