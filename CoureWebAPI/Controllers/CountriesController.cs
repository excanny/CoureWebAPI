using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoureWebAPI.Models;
using Newtonsoft.Json;
using System.Dynamic;

namespace CoureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CountriesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [Route("/api/GetRelatedData")]
        [HttpGet]
        public async Task<ActionResult> GetRelatedData(string phoneNumber)
        {
            var str = phoneNumber.Substring(0, 3);

            var country = await _context.Countries.Where(s => s.CountryCode == str).FirstOrDefaultAsync();

            if (country != null)
            {
                var countrydetails = _context.CountryDetails.Where(c => c.CountryId == country.Id).Select(s => new
                {
                    s.Operator,
                    s.OperatorCode

                }).ToList();

                dynamic expando = new ExpandoObject();
                expando.number = phoneNumber;

                expando.country = new dynamic[1];

                expando.country[0] = new ExpandoObject();
                expando.country[0].name = country.Name;
                expando.country[0].countryCode = country.CountryCode;
                expando.country[0].countryDetails = countrydetails;

                return Ok(expando);
            }

            return new BadRequestResult();

            
        }
    }
}
