using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace S5A0504.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secoundNumber}")]
        public IActionResult Get(string firstNumber, string secoundNumber)
        {
            var errors = new List<string>();
            if (!decimal.TryParse(firstNumber, out decimal first))
                errors.Add("First number must be number");
            if (!decimal.TryParse(secoundNumber, out decimal secound))
                errors.Add("Secound number must be number");
            if (errors.Count == 0)
                return Ok(first + secound);
            else
            {
                return BadRequest(new
                {
                    title = "Invalid Input",
                    errors
                });
            }
        }
    }
}
