using Framework.Core.Domain;

namespace Framework.Core.Masters;
public class Emirates : AuditableEntity<int>
{
    public string EmiratesCode { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<EmiratesRegion> EmiratesRegions { get; } = [];
}
