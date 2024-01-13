using MinimalArchitecture.Entities.Authorization.Enums;
using MinimalArchitecture.Entities.Repository;

namespace MinimalArchitecture.Entities.Authorization.Models;
public class Rol:BaseEntity
{
    public Rol()
    {
        Users = default!;
    }
    public int Id { get; set; }
    public string? Description { get; set; }

    public RolType RolType { get; set; }
    public ICollection<User> Users { get; set; }
}
