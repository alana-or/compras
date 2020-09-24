using Microsoft.AspNetCore.Mvc.Testing;

namespace SimpressUX.API.Teste.Integracao
{
    public static class WebApplicationFactoryExtensions
    {
        public static WebApplicationFactory<TStartup> EnsureServerStarted<TStartup>(this WebApplicationFactory<TStartup> webApplicationFactory) where TStartup : class
        {
            webApplicationFactory.CreateClient();
            return webApplicationFactory;
        }
    }
}
