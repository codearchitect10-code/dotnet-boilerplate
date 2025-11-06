using Framework.Core.Domain;

namespace Framework.Core.Masters;
public class BusinessSector : AuditableEntity<int>
{
    public string Value { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<BusinessSubSector> BusinessSubSectors { get; } = [];

}
