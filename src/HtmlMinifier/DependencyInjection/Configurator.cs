using Bx.HtmlMinifier.Interfaces;
using Bx.HtmlMinifier.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Bx.HtmlMinifier.DependencyInjection
{
    public class Configurator : IServicesConfigurator {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHtmlMinifierService, HtmlMinifierService>();
        }
    }
}