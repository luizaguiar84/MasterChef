using Microsoft.EntityFrameworkCore;

namespace MasterChef.Domain.Entities
{
    [Index(nameof(ExternalId), IsUnique = true)]
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ExternalId { get; set; }
    }
}