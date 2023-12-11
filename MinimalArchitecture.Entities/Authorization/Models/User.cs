using MinimalArchitecture.Entities.Authorization.Enums;
using MinimalArchitecture.Entities.Repository;

namespace MinimalArchitecture.Entities.Authorization.Models;
public class User:BaseEntity
{
    public User()
    {
        Roles = new List<Rol>();
    }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Hash { get; set; }
    public bool Active { get; set; }
    

    public ICollection<Rol> Roles { get; set; }


}
