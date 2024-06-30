using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers;
using PhotosoilAPI;
using System.Text;
using Photosoil.Core.Models;
using System.Reflection;

namespace TestProject1
{

    class MyWebApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // shared extra set up goes here
            return base.CreateHost(builder);
        }
    }
    [TestClass]
    public class UnitTest1
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UnitTest1()
        {
            var application = new MyWebApplication();
            _client = application.CreateClient();
        }
        [TestMethod]
        public async Task TestYourController()
        {
            // Выполняем запрос к вашему контроллеру
            var response = await _client.GetAsync("/api/SoilObject/GetForUpdate?Id=58");


            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ServiceResponse<SoilObjectVM>>(responseString);

            Assert.IsNotNull(responseData);

            responseData.Response.Authors = new int[] { 2, 4 };
            responseData.Response.SoilTerms = new int[] { 2, 4, 22 };
            responseData.Response.EcoSystems = new int[] { 2, 5 };
            responseData.Response.ObjectPhoto = new int[] { 488, 480 };


            //responseData.Response.Authors = new int[] { 2, 5 };
            //responseData.Response.SoilTerms = new int[] { 2, 55, 100 };
            //responseData.Response.EcoSystems = new int[] { 2, 6 };
            //responseData.Response.ObjectPhoto = new int[] { 488, 481,482 };


            var formData = new MultipartFormDataContent();

            // Используем рефлексию для перебора свойств объекта SoilObjectVM и добавления их в FormDataContent
            PropertyInfo[] properties = typeof(SoilObjectVM).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(responseData.Response);
                    formData.Add(new StringContent(value.ToString()), property.Name);
            }


            var responseUpdate = await _client.PutAsync("/api/SoilObject/Put/58", formData);
            responseUpdate.EnsureSuccessStatusCode();

            var responseStringUp = await responseUpdate.Content.ReadAsStringAsync();
            var responseDataUp = JsonConvert.DeserializeObject<ServiceResponse<SoilObject>>(responseStringUp);


            Assert.IsNotNull(responseDataUp);

            Assert.IsTrue(responseDataUp.Response.Authors.Select(x => x.Id) == new int[] { 2, 4 });
            Assert.IsTrue(responseDataUp.Response.Terms.Select(x => x.Id) == new int[] { 2, 4,22 });
            Assert.IsTrue(responseDataUp.Response.EcoSystems.Select(x => x.Id) == new int[] { 2, 5 });
            Assert.IsTrue(responseDataUp.Response.ObjectPhoto.Select(x => x.Id) == new int[] { 488, 480 });

        }
    }


}