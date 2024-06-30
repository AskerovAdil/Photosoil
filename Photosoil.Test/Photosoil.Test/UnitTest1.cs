using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Photosoil.Test
{
    class MyWebApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // shared extra set up goes here
            return base.CreateHost(builder);
        }
    }
    public class UnitTest1
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UnitTest1()
        {
            var application = new MyWebApplication();
            _client = application.CreateClient();
        }
        [Fact]
        public async Task TestYourController()
        {
            // Выполняем запрос к вашему контроллеру
            var response = await _client.GetAsync("/api/SoilObject/GetAll");

            // Проверяем успешность запроса и ожидаемый результат
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Добавьте здесь проверки на ожидаемый результат
            // Например, можно проверить содержимое ответа или статус код

            // Assert...
        }
    }
}