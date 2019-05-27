using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
  public class DestinationTypeController : Controller
  {
    private readonly ITypeRepository _typeRepository;

    public DestinationTypeController(ITypeRepository typeRepository)
    {
      _typeRepository = typeRepository;
    }
    public IActionResult Get()
    {

      return base.Ok(_typeRepository.GetStorageTypes());
    }
  }
}
