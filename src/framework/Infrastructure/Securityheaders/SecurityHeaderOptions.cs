namespace Framework.Infrastructure.Securityheaders;
public class SecurityHeaderOptions
{
    public bool Enable { get; set; }
    public SecurityHeaders Headers { get; set; } = default!;
}

