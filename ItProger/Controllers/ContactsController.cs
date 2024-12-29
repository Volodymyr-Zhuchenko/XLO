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


        [HttpPost]
        public IActionResult LogIn(Autorizate autorizate)
        {
            if (ModelState.IsValid)
            {
                // Перевіряємо, чи є користувач в базі даних
                var user = _context.Contacts.FirstOrDefault(c => c.Email == autorizate.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "Користувач з таким Email не знайдений.");
                    return View(autorizate);
                }

                // Перевіряємо пароль
                if (user.Password != HashPassword(autorizate.Password))
                {
                    ModelState.AddModelError("Password", "Невірний пароль.");
                    return View(autorizate);
                }

                // Якщо логін успішний, зберігаємо дані користувача в сесії
                HttpContext.Session.SetString("UserEmail", user.Email);

                // Перенаправляємо на UserAccountController
                return RedirectToAction("ViewAccount", "UserAccount");
            }

            return View(autorizate);
        }

        public IActionResult LogOut()
        {
            // Виходимо з сесії
            HttpContext.Session.Remove("UserEmail");

            return RedirectToAction("LogIn");
        }

       

    }
}
