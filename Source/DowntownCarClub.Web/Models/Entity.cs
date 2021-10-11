using System;

namespace DowntownCarClub.Web.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
