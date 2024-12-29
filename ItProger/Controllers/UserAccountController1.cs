using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ItProger.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

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

        //public IActionResult ViewAccount()
        //{
        //    // Отримуємо email користувача з сесії
        //    var email = HttpContext.Session.GetString("UserEmail");

        //    // Перевіряємо, чи є email у сесії
        //    if (string.IsNullOrEmpty(email))
        //    {
        //        TempData["ErrorMessage"] = "Будь ласка, увійдіть у систему, щоб переглянути обліковий запис.";
        //        return RedirectToAction("LogIn", "Contacts"); // Редирект на сторінку входу
        //    }

        //    // Шукаємо контакт за email
        //    var contact = _context.Contacts
        //                          .FirstOrDefault(c => c.Email.ToLower() == email.ToLower());

        //    // Якщо контакт не знайдено
        //    if (contact == null)
        //    {
        //        TempData["ErrorMessage"] = "Контакт не знайдено.";
        //        return RedirectToAction("LogIn", "Contacts");
        //    }


        //    // Повертаємо контакт у вигляді моделі
        //    return View(contact);
        //}

        public IActionResult ViewAccount(string searchTerm)
        {
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
            // Отримуємо всі моделі в просторі імені ItProger.Models.Category
            var categoryNamespace = "ItProger.Models.Category";
            var assembly = Assembly.GetExecutingAssembly();

            var categories = assembly.GetTypes()
                .Where(t => t.Namespace == categoryNamespace && t.IsClass && !t.IsAbstract)
                .Select(t => t.Name) // Назви моделей (категорій)
                .ToList();

            // Фільтрація категорій за пошуком
            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories
                    .Where(c => c.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(categories); // Передаємо список категорій у View
        }
    }
}
