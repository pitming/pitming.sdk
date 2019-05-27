using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
  public class StorageController : Controller
  {
    private readonly IStorageRepository _storageRepository;

    public StorageController(IStorageRepository storageRepository)
    {
      _storageRepository = storageRepository;
    }
  }

  public class AccessController : Controller
  {
    private readonly IStorageRepository _storageRepository;

    public AccessController(IStorageRepository storageRepository)
    {
      _storageRepository = storageRepository;
    }

    public async Task<IActionResult> Get(string id)
    {
      var storage = await _storageRepository.GetAccessAsync(id);
      if (storage == null)
        return NotFound();
      return Ok(storage);
    }

    public async Task<IActionResult> Create(Access access)
    {
      await _storageRepository.CreateAsync(access);
      return Created(string.Empty, access.Id);
    }

    public async Task<IActionResult> Delete(string accessId)
    {
      await _storageRepository.DeleteAsync(accessId);
      return NoContent();
    }
  }
}
