using CompanyList.Data;
using CompanyList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CompanyList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyDbContext context;

        public CompanyController(CompanyDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            var com = context.Companies.ToList();
            return Ok(com);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCompanyById(int id)
        {
            var com = context.Companies.FirstOrDefault(c=> c.CompanyId == id);
            return Ok(com);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany(Company company)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");
            context.Companies.Add(new Company()
            {
                CompanyName = company.CompanyName,
                CompanyId = company.CompanyId,
            });
            context.SaveChanges();
            return Ok(company);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invaild Data");
            }


            var existingCompany = context.Companies.Where(c => c.CompanyId == company.CompanyId).FirstOrDefault<Company>();
            if (existingCompany != null)
            {
                existingCompany.CompanyName = company.CompanyName;
                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok(existingCompany);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid student id");
            }
            var company = context.Companies.Where(c => c.CompanyId == id).FirstOrDefault();
            context.Entry(company).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();

            return Ok();
        }

    }
}
