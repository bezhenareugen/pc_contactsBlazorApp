using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsBlazorApp.Server.Data;
using ContactsBlazorApp.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsBlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public List<Contact> GetContacts()
        {
            var userId = _userManager.GetUserId(User);

            var contact = _context.Users.Include(c => c.Contacts).FirstOrDefault(u => u.Id == userId).Contacts;

            return contact;
        }

        [HttpPost]
        public void Save([FromBody] string ContactPhone)
        {
            var userId = _userManager.GetUserId(User);

            var contact = new Contact
            {
                ContactPhone = ContactPhone
            };

            var user = _context.Users.Include(c => c.Contacts).FirstOrDefault(u => u.Id == userId);

            user.Contacts.Add(contact);
            _context.SaveChanges();

        }

        [HttpPut]
        [Route("{id}")]
        public Contact Update(Guid id, [FromBody] Contact model)
        {
            var userId = _userManager.GetUserId(User);

            var contactUpdate = _context.Users.Include(c => c.Contacts).FirstOrDefault(u => u.Id == userId).Contacts.FirstOrDefault(c => c.Id == id);

         
            contactUpdate.ContactPhone = model.ContactPhone;

            _context.Update(contactUpdate);
            _context.SaveChanges();

            return model;
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(Guid id)
        {
            var userId = _userManager.GetUserId(User);

            var userContact = _context.Users.Include(c => c.Contacts).FirstOrDefault(u => u.Id == userId).Contacts.FirstOrDefault(c => c.Id == id);

            _context.Remove(userContact);
            _context.SaveChanges();
        }
    }
}
