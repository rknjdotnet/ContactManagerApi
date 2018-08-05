using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerApi.Models;
using ContactManger.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Contact")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _contactService.GetContacts());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _contactService.GetContactById(id));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(ContactRequest contact)
        {
            try
            {
                return Ok(await _contactService.AddContact(contact));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id,ContactRequest contact)
        {
            try
            {
                return Ok(await _contactService.UpdateContact(id, contact));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _contactService.DeleteContact(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}