using Microsoft.AspNetCore.Mvc;
using ItProger.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;


namespace ItProger.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index() //contact
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registry(Contact contact) //contact
        {
            if (ModelState.IsValid) 
            {
                return Redirect("/");
            }
            return View("Index");
        }

        public IActionResult LogIn() //contact
        {
  
            return View("LogIn");
        }
        private readonly AppDbContext _context;
        public ContactsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {

            // Перевіряємо, чи є email у базі даних
            bool emailExists = _context.Contacts.Any(c => c.Email == contact.Email);

           

            if (emailExists)
            {
                ModelState.AddModelError("Email", "Цей Email вже використовується.");
            }
            if (ModelState.IsValid)
            {
                // Хешуємо пароль перед збереженням
                contact.Password = HashPassword(contact.Password);

                _context.Contacts.Add(contact);   // Додаємо запис
                _context.SaveChanges();    // Зберігаємо в базу даних
                return RedirectToAction("LogIn");
            }
            return View("Index");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Перетворюємо пароль у байти
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                // Генеруємо хеш
                byte[] hash = sha256.ComputeHash(bytes);
                // Перетворюємо хеш у рядок
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }


    }
}
