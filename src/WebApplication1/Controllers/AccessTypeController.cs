using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
  public class AccessTypeController : Controller
  {
    private readonly ITypeRepository _typeRepository;

    public AccessTypeController(ITypeRepository typeRepository)
    {
      _typeRepository = typeRepository;
    }

    public IActionResult Get(string destinationType)
    {
      var accessType = _typeRepository.GetAccessType(destinationType);
      if (accessType == null)
        return NotFound();
      return Ok(accessType);
    }
  }
}
