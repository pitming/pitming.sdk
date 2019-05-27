using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
  public interface IStorageRepository
  {
    Task<Access> GetAccessAsync(string id);
    Task DeleteAsync(string accessId);
    Task CreateAsync(Access access);
  }

  public class StorageIn
  {
    public string Id { get; set; }
    public string[] AccessIds { get; set; }
    public Access[] Accesses { get; set; }

  }
  public class Access
  {
    public string Id { get; set; }
    public Samba Samba { get; set; }
  }

  public class Samba
  { }
}