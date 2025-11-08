/*using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;

public class OcelotIntegrationTests : IClassFixture<WebApplicationFactory<>>
{
    private readonly HttpClient _client;
    private string _carId;
    private const string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIxNWQyMTcxMi1lMWFiLTRlNGEtYmVlMS0xMzRhZTY3NzczN2EiLCJleHAiOjE3MzQ0NjU1NDl9._XOCZ6nz-pDjKKL2wzuQBPisAgbKj4lrQRRm4fgG-5w";
    private const string _baseUrl = "https://localhost:8080";
    
    public OcelotIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri(_baseUrl);
    }
        
    [Fact]
    public async Task GetAllCars_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/Cars");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var cars = await response.Content.ReadFromJsonAsync<IEnumerable<Car>>();
        _carId = cars!.First().Id;
        Assert.NotNull(cars);
    }

    [Fact]
    public async Task GetCarById_ShouldReturnOk()
    {
        // Arrange
        var carId = this._carId;

        // Act
        var response = await _client.GetAsync($"/Cars/{carId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var car = await response.Content.ReadFromJsonAsync<Car>();
        Assert.NotNull(car);
        Assert.Equal(carId, car.Id);
    }

    [Fact]
    public async Task CreateCar_ShouldRequireAuthorization()
    {
        // Arrange
        var newCar = new CarDto("TestModel", "TestMake", 2023);
        var content = new StringContent(JsonSerializer.Serialize(newCar), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/Cars", content);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCar_ShouldReturnNotFound_WhenIdInvalid()
    {
        // Arrange
        const string invalidCarId = "999";
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        // Act
        var response = await _client.DeleteAsync($"/Cars/{invalidCarId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task DeleteCar_ShouldReturnUnauthorized()
    {
        // Arrange
        const string invalidCarId = "999";
        const string jwt = "";
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await _client.DeleteAsync($"/Cars/{invalidCarId}");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private record Car(string Id, string Model, string Make, int Year);
    private record CarDto(string Model, string Make, int Year);
}*/