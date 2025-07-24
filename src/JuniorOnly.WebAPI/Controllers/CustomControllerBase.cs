using Microsoft.AspNetCore.Mvc;

namespace JuniorOnly.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomControllerBase : ControllerBase
    {
    }
}
