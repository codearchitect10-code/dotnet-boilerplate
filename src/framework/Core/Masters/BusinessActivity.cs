using Framework.Core.Domain;

namespace Framework.Core.Masters;
public class BusinessActivity : AuditableEntity<int>
{
    public string ActivityCode { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Foreign Key
    public int BusinessSubSectorId { get; set; }

    // Navigation
    public BusinessSubSector BusinessSubSector { get; }
}
