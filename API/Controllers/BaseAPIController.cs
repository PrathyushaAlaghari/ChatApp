using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")] // /api/user [controller] means the name of the controller
public class BaseAPIController : ControllerBase
{

}
