using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomersController : ControllerBase
  {
    [HttpGet, Authorize]
    public IEnumerable<string> GetCustomers()
    {
      return new string[] { "John Doe", "Chandrashekhar Singh" };
    }
  }
}
