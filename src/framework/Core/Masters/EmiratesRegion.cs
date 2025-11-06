using Framework.Core.Domain;

namespace Framework.Core.Masters;
public class EmiratesRegion : AuditableEntity<int>
{
    public string Value { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Foreign Key
    public int EmiratesId { get; set; }

    // Navigation
    public Emirates Emirates { get; set; }
}
