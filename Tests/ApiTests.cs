using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Api.Messaging;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class ApiTests
    {
        public const string baseUrl = "https://localhost:7117/";
        public const string insecureBaseUrl = "http://localhost:5007/";

        private async Task<HttpResponseMessage?> GetVehiclesResponse() {
            var client = new HttpClient();
            var vehiclesUrl = insecureBaseUrl + "vehicles";
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, vehiclesUrl));
            
            return response;
        }

        private async Task<Vehicle[]?> GetVehiclesData(HttpResponseMessage response) {
            var data = await response.Content.ReadFromJsonAsync<Api.Messaging.Vehicle[]>();

            return data;
        }

        private async Task<HttpResponseMessage?> GetFindResponse(Guid[] ids) {
            var client = new HttpClient();
            var findUrl = insecureBaseUrl + "coordinates/find";

            var response = await client.PostAsJsonAsync<Guid[]>(findUrl, ids);

            return response;
        }

        private async Task<Coordinate[]?> GetFindData(HttpResponseMessage findResponse)
        {
            var coordinates = await findResponse.Content.ReadFromJsonAsync<Api.Messaging.Coordinate[]>();
            return coordinates;
        }

        private async Task<HttpResponseMessage?> GetCalcResponse(Coordinate[] coordinates) {
            var client = new HttpClient();
            var canculateUrl = insecureBaseUrl + "coordinates/calculate-path";

            var calcResponse = await client.PostAsJsonAsync(canculateUrl, coordinates);

            return calcResponse;
        }

        private async Task<Dictionary<string, VehicleStats>?> GetCalcData(HttpResponseMessage calcResponse) {
            var vehicleMap = await calcResponse.Content.ReadFromJsonAsync<Dictionary<string, VehicleStats>>();
            
            return vehicleMap;
        }

        [Test]
        public async Task TestVehicles()
        {
            var response = await GetVehiclesResponse();

            Assert.That(response, Is.Not.Null);
            Assert.That(response.IsSuccessStatusCode, Is.True);

            var vehicles = await GetVehiclesData(response);

            Assert.That(vehicles, Is.Not.Null);
            Assert.That(vehicles, Is.Not.Empty);
        }

        [Test]
        public async Task TestCoordinatesFind() {
            
            var response = await GetVehiclesResponse();;

            Assert.That(response, Is.Not.Null);
            Assert.That(response.IsSuccessStatusCode, Is.True);

            var vehicles = await GetVehiclesData(response);
            
            Assert.That(vehicles, Is.Not.Null);
            Assert.That(vehicles, Is.Not.Empty);

            var vehicleIds = vehicles.Select(v => v.Id).ToArray();

            var findResponse = await GetFindResponse(vehicleIds);

            Assert.That(findResponse, Is.Not.Null);
            Assert.That(findResponse.IsSuccessStatusCode, Is.True);

            var coordinates = await GetFindData(findResponse);

            Assert.That(coordinates, Is.Not.Null);
            Assert.That(coordinates, Is.Not.Empty);
        }

        [Test]
        public async Task TestCoordinatesCalculatePath() {
            var response = await GetVehiclesResponse();;

            Assert.That(response, Is.Not.Null);
            Assert.That(response.IsSuccessStatusCode, Is.True);

            var vehicles = await GetVehiclesData(response);
            
            Assert.That(vehicles, Is.Not.Null);
            Assert.That(vehicles, Is.Not.Empty);

            var vehicleIds = vehicles.Select(v => v.Id).ToArray();

            var findResponse = await GetFindResponse(vehicleIds);

            Assert.That(findResponse, Is.Not.Null);
            Assert.That(findResponse.IsSuccessStatusCode, Is.True);

            var coordinates = await GetFindData(findResponse);

            Assert.That(coordinates, Is.Not.Null);
            Assert.That(coordinates, Is.Not.Empty);

            var calcResponse = await GetCalcResponse(coordinates);

            Assert.That(calcResponse, Is.Not.Null);
            Assert.That(calcResponse.IsSuccessStatusCode, Is.True);

            var vehicleMap = await GetCalcData(calcResponse);
            
            Assert.That(vehicleMap, Is.Not.Null);
            Assert.That(vehicleMap, Is.Not.Empty);

            //TestContext.Progress.WriteLine(JsonSerializer.Serialize(vehicleMap));
        }
    }
}
