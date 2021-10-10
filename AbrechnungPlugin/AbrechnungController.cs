using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AbrechnungsPlugin
{
    [ApiController]
    [Route("[controller]")]
    public class AbrechnungsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<AbrechnungsController> _logger;

        public AbrechnungsController(ILogger<AbrechnungsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Person()
            {
                Name = "Test",
                Id = index
            })
            .ToArray();
        }
    }
}