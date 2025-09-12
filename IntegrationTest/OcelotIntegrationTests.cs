using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTest;

public class OcelotIntegrationTests
{
    private string _carId;
    private const string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIxNWQyMTcxMi1lMWFiLTRlNGEtYmVlMS0xMzRhZTY3NzczN2EiLCJleHAiOjE3MzQ0NjU1NDl9._XOCZ6nz-pDjKKL2wzuQBPisAgbKj4lrQRRm4fgG-5w";
    private const string _baseUrl = "https://localhost:9093";
        
    [Test]
    public async Task GetAllCars_ShouldReturnOk()
    {
        WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        HttpClient client = webHost.CreateClient();
        // Act
        var response = await client.GetAsync("/Cars");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var cars = await response.Content.ReadFromJsonAsync<IEnumerable<Car>>();
        _carId = cars!.First().Id;
        Assert.NotNull(cars);
    }

    [Test]
    public async Task GetCarById_ShouldReturnOk()
    {
        var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var client = webHost.CreateClient();
        
        // Arrange
        var carId = this._carId;

        // Act
        var response = await client.GetAsync($"/Cars/{carId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var car = await response.Content.ReadFromJsonAsync<Car>();
        Assert.NotNull(car);
        Assert.That(car.Id, Is.EqualTo(carId));
    }

    [Test]
    public async Task CreateCar_ShouldRequireAuthorization()
    {
        var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var client = webHost.CreateClient();
        
        // Arrange
        var newCar = new CarDto ("TestModel","TestMake",2023);
        var content = new StringContent(JsonSerializer.Serialize(newCar), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/Cars", content);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task DeleteCar_ShouldReturnNotFound_WhenIdInvalid()
    {
        var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var client = webHost.CreateClient();
        
        // Arrange
        const string invalidCarId = "999";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        // Act
        var response = await client.DeleteAsync($"/Cars/{invalidCarId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task DeleteCar_ShouldReturnUnauthorized()
    {
        var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var client = webHost.CreateClient();
        
        // Arrange
        const string invalidCarId = "999";
        const string jwt = "";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await client.DeleteAsync($"/Cars/{invalidCarId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }


    private record Car(string Id, string Model, string Make, int Year);
    private record CarDto(string Model, string Make, int Year);
}