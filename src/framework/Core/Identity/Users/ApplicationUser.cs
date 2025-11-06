using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Core.Domain.Contracts;
using Framework.Core.Domain.Events;
using Microsoft.AspNetCore.Identity;

namespace Framework.Core.Identity.Users;
public class ApplicationUser : IdentityUser, IAuditable, ISoftDeletable
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Uri? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public int UserRoleId { get; set; }
    public int BusinessSectorId { get; set; }
    public int BusinessSubSectorId { get; set; }
    public int BusinessActivityId { get; set; }
    public string? EmiratesId { get; set; }
    public DateTime? DOB { get; set; }
    public string Gender { get; set; } = default!;
    public string? Nationality { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public string? ObjectId { get; set; }

    public DateTimeOffset Created { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTimeOffset? Deleted { get; set; }
    public Guid? DeletedBy { get; set; }

    [NotMapped]
    public Collection<DomainEvent> DomainEvents { get; } = [];
    public void QueueDomainEvent(DomainEvent @event)
    {
        if (!DomainEvents.Contains(@event))
        {
            DomainEvents.Add(@event);
        }
    }
}
