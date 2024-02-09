using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrangeAPI.Data;
using OrangeAPI.Entities;

namespace OrangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrangeController : ControllerBase
    {
        private readonly DataContext context;
        private readonly DataContext _context;

        public OrangeController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Bank>>> GetAllBanks()
        {
            var banks = await _context.Banks.ToListAsync();
            
            return Ok(banks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Bank>>> GetBank(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank is null)
                return NotFound("Bank does not exist");

            return Ok(bank);
        }

        [HttpPost]
        public async Task<ActionResult<List<Bank>>> AddBank([FromBody]Bank bank)
        {
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();
          
            //IDEALLY JUST RETURN NOT ALL THE DATA
            return Ok(await _context.Banks.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Bank>>> UpdateBank(Bank updatedBank)
        {
            var dbBank = await _context.Banks.FindAsync(updatedBank.Id);
            if (dbBank is null)
                return NotFound("Bank does not exist");

            dbBank.Name = updatedBank.Name;
            dbBank.SortCode = updatedBank.SortCode;
            dbBank.AccountNumber = updatedBank.AccountNumber;
            dbBank.BankName = updatedBank.BankName;
            dbBank.BankAddress = updatedBank.BankAddress;

            await _context.SaveChangesAsync();


            return Ok(await _context.Banks.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Bank>>> DeleteBank(int id)
        {
            var dbBank = await _context.Banks.FindAsync(id);
            if (dbBank is null)
                return NotFound("Bank does not exist");

            _context.Banks.Remove(dbBank);
            await _context.SaveChangesAsync();

            //ideally you don't need to return the whole list again as in line 78
            //return Ok(await _context.Banks.ToListAsync());
            return Ok($"{dbBank.Name} bank details has been deleted successfully ");
        }
    }
}
