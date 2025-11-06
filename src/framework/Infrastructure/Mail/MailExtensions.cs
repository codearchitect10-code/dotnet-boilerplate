using Framework.Core.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Infrastructure.Mail;
internal static class MailExtensions
{
    internal static IServiceCollection ConfigureMailing(this IServiceCollection services)
    {
        services.AddTransient<IMailService, SmtpMailService>();
        services.AddOptions<MailOptions>().BindConfiguration(nameof(MailOptions));
        return services;
    }
}
