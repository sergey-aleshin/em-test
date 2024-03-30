using System;
using System.Net;
using System.Net.Http.Json;
using Api.Messaging;
using NUnit.Framework;

namespace Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class ApiTests
    {
        public const string baseUrl = "https://localhost:7117/";

        [Test]
        public async Task TestVehicles()
        {
            var client = new HttpClient();
            var url = baseUrl + "vehicles";
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));

            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.InstanceOf(typeof(HttpResponseMessage)));
            Assert.That(response.IsSuccessStatusCode, Is.True);

            var data = await response.Content.ReadFromJsonAsync<Api.Messaging.Vehicle[]>();
            Assert.That(data, Is.Not.Null);
            Assert.That(data, Is.Not.Empty);
        }
    }
}
