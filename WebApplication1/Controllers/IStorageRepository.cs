using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
  public interface IStorageRepository
  {
    Task<Access> GetAccessAsync(string id);
  }

  public class Storage
  {
    public string Id { get; set; }
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