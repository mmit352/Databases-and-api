using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSIMS.Data;
using StudentSIMS.Models;

namespace StudentSIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AddressContext _context;

        public AddressesController(AddressContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.Address.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.studentId)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.studentId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.studentId == id);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<Address>> PostStudentAddress(Address address, int id)
        {
            var student = await _context.student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            Student newStudent = _context.student.Single(c => c.studentId == id);
            Address newAddress = new Address
            {
                streetNumber = address.streetNumber,
                street = address.street,
                suburb = address.suburb,
                city = address.city,
                postCode = address.postCode,
                country = address.country,
                studentId = id,


            };
            _context.Address.Add(newAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = newAddress.addressId }, newAddress);



        }
        [HttpPut("/api/StudentAddress/{id}")]
        public async Task<IActionResult> PutStudentAddress(int id, Address address)
        {
            if (_context.Address.Any(c => c.studentId == id)) {
                Address existingAddress = _context.Address.Single(c => c.studentId == id && c.addressId == address.addressId);

        

            if (existingAddress != null)
            {
                existingAddress.streetNumber = address.streetNumber;
                existingAddress.street = address.street;
                existingAddress.suburb = address.suburb;
                existingAddress.city = address.city;
                existingAddress.postCode = address.postCode;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                        if (AddressExists(id))
                        {
                            throw;
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
            };

        } else 
        {
            return BadRequest();
        }
        return NoContent();
}

        /*private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.addressId == id);


        }*/








    }
}