using Microsoft.AspNetCore.Mvc;

namespace OptionsBinder
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly SomeConfiguration _someConfiguration;

        public ValuesController(SomeConfiguration someConfiguration)
        {
            _someConfiguration = someConfiguration;
        }

        [HttpGet]
        public string Get()
        {
            return $"Value read from configuration: {_someConfiguration.SomeProp}";
        }
    }
}
