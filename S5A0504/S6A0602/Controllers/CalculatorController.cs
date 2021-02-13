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

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secoundNumber}")]
        public IActionResult Sum(string firstNumber, string secoundNumber)
        {
            Validate(firstNumber, secoundNumber, out List<string> errors, out decimal first, out decimal secound);
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
        [HttpGet("sub/{firstNumber}/{secoundNumber}")]
        public IActionResult Sub(string firstNumber, string secoundNumber)
        {
            Validate(firstNumber, secoundNumber, out List<string> errors, out decimal first, out decimal secound);
            if (errors.Count == 0)
                return Ok(first - secound);
            else
            {
                return BadRequest(new
                {
                    title = "Invalid Input",
                    errors
                });
            }
        }
        [HttpGet("div/{firstNumber}/{secoundNumber}")]
        public IActionResult Div(string firstNumber, string secoundNumber)
        {
            Validate(firstNumber, secoundNumber, out List<string> errors, out decimal first, out decimal secound);
            if (errors.Count == 0)
                return Ok(first / secound);
            else
            {
                return BadRequest(new
                {
                    title = "Invalid Input",
                    errors
                });
            }
        }
        [HttpGet("ave/{firstNumber}/{secoundNumber}")]
        public IActionResult Average(string firstNumber, string secoundNumber)
        {
            Validate(firstNumber, secoundNumber, out List<string> errors, out decimal first, out decimal secound);
            if (errors.Count == 0)
                return Ok((first + secound) / 2);
            else
            {
                return BadRequest(new
                {
                    title = "Invalid Input",
                    errors
                });
            }
        }

        [HttpGet("sqrt/{Number}")]
        public IActionResult Sqrt(string Number)
        {
            if (!double.TryParse(Number, out double value))
            {
                return BadRequest(new
                {
                    title = "Invalid Input",
                    errors = new string[]
                    {
                        $"Inválid Number '{Number}'"
                    }
                });
            }
            else
                return Ok(Math.Sqrt(value));
        }

        [NonAction]
        private void Validate(string firstNumber, string secoundNumber, out List<string> errors, out decimal first, out decimal secound)
        {
            errors = new List<string>();
            if (!decimal.TryParse(firstNumber, out first))
                errors.Add($"First number '{firstNumber}' must be number");
            if (!decimal.TryParse(secoundNumber, out secound))
                errors.Add($"Secound number '{secoundNumber}' must be number");
        }
    }
}
