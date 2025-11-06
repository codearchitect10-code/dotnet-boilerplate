using Framework.Core.Domain;

namespace Framework.Core.Masters;
public class BusinessSubSector : AuditableEntity<int>
{
    public string Value { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Foreign Key
    public int BusinessSectorId { get; set; }

    // Navigation
    public BusinessSector BusinessSector { get; set; }
    public ICollection<BusinessActivity> BusinessActivities { get; } = [];
}
