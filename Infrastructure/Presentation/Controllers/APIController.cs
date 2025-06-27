global using Microsoft.AspNetCore.Mvc;
global using ServicesAbstractions;
global using Shared.DataTransferObjects;
global using Shared.DataTransferObjects.Products;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class APIController :ControllerBase
    {

    }
}
