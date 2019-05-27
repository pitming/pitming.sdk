namespace WebApplication1.Controllers
{
  public interface ITypeRepository
  {
    string[] GetStorageTypes();
    string[] GetAccessType(string destinationType);
  }
}